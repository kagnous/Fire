using UnityEngine;
using UnityEngine.InputSystem;

public class MB_CameraBehavior : MonoBehaviour
{
    [Header("Diorama Rotation")]
    [SerializeField]
    private Transform _diorama;
    
    private float _movementRot;

    [SerializeField]
    private float _rotAcceleration = 0.1f;

    [SerializeField]
    private float _maxRotSpeed = 3f;

    private float _rotInput;

    [Header("Camera Up and Down")]
    [SerializeField]
    private float _maxCameraUp;
    [SerializeField]
    private float _maxCameraDown;

    [SerializeField]
    private float _upAcceleration = 0.1f;

    [SerializeField]
    private float _maxUpSpeed = 3f;

    private float _upMovement;

    private float _upInput;

    private float _baseMove = 0;

    [Header("Zoom/Dezoom")]

    [SerializeField]
    private float _maxZoom;
    [SerializeField]
    private float _maxDezoom;

    [SerializeField]
    private float _zoomAcceleration = 0.1f;

    [SerializeField]
    private float _maxZoomSpeed = 3f;

    private float _zoomMovement;

    private float _zoomInput;



    private void FixedUpdate()
    {
        _movementRot = DioramaRotation(_rotInput, _movementRot, _maxRotSpeed, _rotAcceleration);

        _diorama.Rotate(new Vector2(0, _movementRot));

        _upMovement = DioramaRotation(_upInput,_upMovement, _maxUpSpeed, _upAcceleration);

        if(_baseMove + _upMovement > _maxCameraDown && _baseMove + _upMovement < _maxCameraUp)
        {
            transform.Rotate(new Vector2(_upMovement, 0));
            _baseMove += _upMovement;
        }
        else
        {
            _upMovement = 0;
        }

        _zoomMovement = DioramaRotation(_zoomInput, _zoomMovement, _maxZoomSpeed, _zoomAcceleration);

        Camera camera = Camera.main;

        if(camera.fieldOfView + _zoomMovement < _maxDezoom && camera.fieldOfView + _zoomMovement > _maxZoom)
        {
            camera.fieldOfView += _zoomMovement;
        }
        else
        {
            _zoomMovement = 0;
        }

    }

    private float DioramaRotation(float _input, float _movement, float _maxSpeed, float _acceleration)
    {
        if (_input > 0)
        {
            if (_movement < _maxSpeed)
            {
                _movement += _input * _acceleration;
            }
        }
        else if (_input < 0)
        {
            if (_movement > -_maxSpeed)
            {
                _movement += _input * _acceleration;
            }
        }
        else
        {
            if (_movement > _acceleration)
            {
                _movement -= _acceleration;
            }
            else if (_movement < -_acceleration)
            {
                _movement += _acceleration;
            }
            else if (_movement != 0)
            {
                _movement = 0;
            }
        }

        return _movement;
    }

    public void GetRotationInput(InputAction.CallbackContext callbackContext)
    {
        _rotInput = callbackContext.ReadValue<float>();
    }

    public void GetUpDownInput(InputAction.CallbackContext callbackContext)
    {
        _upInput = callbackContext.ReadValue<float>();
    }

    public void GetZoomInput(InputAction.CallbackContext callbackContext)
    {
        _zoomInput = callbackContext.ReadValue<float>();
    }

    public void GetDezoomInput(InputAction.CallbackContext callbackContext)
    {
        _zoomInput = -callbackContext.ReadValue<float>();
    }

}
