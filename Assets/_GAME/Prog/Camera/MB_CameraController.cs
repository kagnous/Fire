using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[AddComponentMenu("Fire/Camera/Camera Controller V2")]
public class MB_CameraController : MonoBehaviour
{
    /****************************SERIALIZED PROPERTIES********************************/

    [SerializeField, Tooltip("Camera")]
    private Camera _camera;

    [SerializeField, Tooltip("speed in sec of lerp")]
    private float _lerpSpeed = 1;

    [SerializeField, Tooltip("mode of camera used at runtime")]
    private CameraMode _activeMode;

    [SerializeField, Tooltip("animation curve for speed of lerp")]
    private AnimationCurve _lerpCurve;

    [SerializeField, Tooltip("when player and fire are mixed, higher value means closer to player, 0 is fully fire")]
    [Range(0f, 1f)]
    private float _playerFollowIntensity;

    [SerializeField]
    private bool[] _modeEnabledList = new bool[0];

    [SerializeField]
    private float _scrollLerpSpeed = 0.01f;


    /****************************UNSERIALIZED PROPERTIES********************************/

    private int _cameraStateLength = System.Enum.GetValues(typeof(CameraMode)).Length;

    private Transform _player;
    private Transform _fire;

    private CameraMode _modeMemo;

    private Quaternion _toLerpAngle = Quaternion.identity;
    private Quaternion _fireLocalAngle = Quaternion.identity;

    private float _lerpTimer = 0;

    private bool _isFirstFrame = true;


    #region Public API

    public enum CameraMode
    {
        FireLock,
        PlayerLock,
        PlayerFireMixed,
    }

    #endregion


    /****************************CLASS FUNCTIONS********************************/

    private void Awake()
    {
        Init();

        _player = GameObject.FindWithTag("Player").transform;
        _fire = FindObjectOfType<MB_FireController>().transform;

        _camera.transform.LookAt(_fire);
        _fireLocalAngle = _camera.transform.localRotation;

        if ((int)_activeMode < _cameraStateLength - 1)
            _modeMemo = _activeMode + 1;
        else
            _modeMemo = _activeMode - 1;
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
            Init();

        if (_lerpCurve.keys.Length == 0)
            _lerpCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));

        if (_modeEnabledList.Length != _cameraStateLength)
            _modeEnabledList = new bool[_cameraStateLength];
    }

    private void Update()
    {
        transform.position = _fire.position;

        if (_activeMode == CameraMode.FireLock)
        {
            if (_activeMode != _modeMemo)
            {
                if (!_isFirstFrame)
                    CallExit(_modeMemo);

                OnFireLockEnter();
            }

            OnFireLockStay();
        }
        else if (_activeMode == CameraMode.PlayerLock)
        {
            if (_activeMode != _modeMemo)
            {
                if (!_isFirstFrame)
                    CallExit(_modeMemo);

                OnPlayerLockEnter();
            }

            OnPlayerLockStay();
        }
        else if (_activeMode == CameraMode.PlayerFireMixed)
        {
            if (_activeMode != _modeMemo)
            {
                if (!_isFirstFrame)
                    CallExit(_modeMemo);

                OnPlayerFireMixedEnter();
            }

            OnPlayerFireMixedStay();
        }

        if (_lerpCurve.keys[0].time != 0 || _lerpCurve.keys[0].value != 0)
            _lerpCurve.keys[0] = new Keyframe(0, 0);
        if (_lerpCurve.keys[_lerpCurve.keys.Length - 1].time != 1 || _lerpCurve.keys[_lerpCurve.keys.Length - 1].value != 1)
            _lerpCurve.keys[_lerpCurve.keys.Length - 1] = new Keyframe(1, 1);

        _modeMemo = _activeMode;
        _isFirstFrame = false;
    }

    /// <summary>
    /// called at awake and at editor scene repaint
    /// </summary>
    private void Init()
    {
        if (_camera == null)
            if ((_camera = Camera.main) == null)
                if (!TryGetComponent(out _camera))
                    _camera = gameObject.AddComponent<Camera>();
    }

    private void CallExit(CameraMode memoCam)
    {
        if (_modeMemo == CameraMode.FireLock)
            OnFireLockExit();
        else if (_modeMemo == CameraMode.PlayerLock)
            OnPlayerLockExit();
        else if (_modeMemo == CameraMode.PlayerFireMixed)
            OnPlayerFireMixedExit();
    }

    #region On Fire Lock

    private void OnFireLockEnter()
    {
        Debug.Log("fire enter");

        _toLerpAngle = Camera.main.transform.localRotation;
        _lerpTimer = 0;
    }
    private void OnFireLockStay()
    {
        Debug.Log("fire stay");

        _camera.transform.localRotation = Quaternion.Lerp(_toLerpAngle, _fireLocalAngle, _lerpCurve.Evaluate(_lerpTimer));

        if (_lerpTimer < 1)
            _lerpTimer += Time.deltaTime / _lerpSpeed;
        else
            _lerpTimer = 1;
    }
    private void OnFireLockExit()
    {
        Debug.Log("fire exit");

    }

    #endregion

    #region On Player Lock

    private void OnPlayerLockEnter()
    {
        Debug.Log("player enter");

        _toLerpAngle = Camera.main.transform.rotation;
        _lerpTimer = 0;
    }
    private void OnPlayerLockStay()
    {
        Debug.Log("player stay");

        _camera.transform.LookAt(_player);
        Quaternion playerTarget = _camera.transform.rotation;
        _camera.transform.rotation = _toLerpAngle;

        _camera.transform.rotation = Quaternion.Lerp(_toLerpAngle, playerTarget, _lerpCurve.Evaluate(_lerpTimer));

        if (_lerpTimer < 1)
            _lerpTimer += Time.deltaTime / _lerpSpeed;
        else
            _lerpTimer = 1;
    }
    private void OnPlayerLockExit()
    {
        Debug.Log("player exit");

    }

    #endregion

    #region On Player Fire Mixed Lock

    private void OnPlayerFireMixedEnter()
    {
        Debug.Log("player fire enter");

        _toLerpAngle = Camera.main.transform.rotation;
        _lerpTimer = 0;
    }
    private void OnPlayerFireMixedStay()
    {
        Debug.Log("player fire stay");

        _camera.transform.LookAt(_player);
        Quaternion playerTarget = _camera.transform.rotation;
        _camera.transform.LookAt(_fire);
        Quaternion _fireAngle = _camera.transform.rotation;
        _camera.transform.rotation = _toLerpAngle;


        Quaternion target = Quaternion.Lerp(_fireAngle, playerTarget, _playerFollowIntensity);

        _camera.transform.rotation = Quaternion.Lerp(_toLerpAngle, target, _lerpCurve.Evaluate(_lerpTimer));

        if (_lerpTimer < 1)
            _lerpTimer += Time.deltaTime / _lerpSpeed;
        else
            _lerpTimer = 1;
    }
    private void OnPlayerFireMixedExit()
    {
        Debug.Log("player fire exit");

    }

    #endregion

    #region INPUT CALLBACKS

    public void OnSwitchMode(InputAction.CallbackContext callback)
    {
        if (callback.started)
        {
            int autobreak = 0;

            do
            {
                if ((int)_activeMode < _cameraStateLength - 1)
                    _activeMode++;
                else
                    _activeMode = 0;
                autobreak++;
            }
            while (!_modeEnabledList[(int)_activeMode] && autobreak < 100);
        }
    }

    public void OnLerpScroll(InputAction.CallbackContext callback)
    {
        _playerFollowIntensity += _scrollLerpSpeed * callback.ReadValue<float>();
        _playerFollowIntensity = Mathf.Clamp(_playerFollowIntensity, 0f, 1f);
    }

    #endregion
}
