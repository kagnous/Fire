using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MB_UIController : MonoBehaviour
{
    private MB_PlayerController _player;
    private Controls _inputsInstance;

    private GameObject _cameraIcon;
    private int _playerChecks = 0;

    private GameObject _grabIcon;
    private GameObject _interractIcon;

    private void Awake()
    {
        _inputsInstance = new Controls();
    }

    private void Start()
    {
        _player = FindObjectOfType<MB_PlayerController>();
        _player.eventInGrabRange += DisplayGrab;
        _player.eventInInterractRange += DisplayInterract;
        _player.eventGrab += GrabClick;
        _player.eventInterract += InterractClick;

        _cameraIcon = transform.Find("CameraIcon").gameObject;

        _grabIcon = transform.Find("Grab").gameObject;
        _grabIcon.SetActive(false);

        _interractIcon = transform.Find("Interract").gameObject;
        _interractIcon.SetActive(false);
    }

    private void OnEnable()
    {
        _inputsInstance.Camera.Enable();
        _inputsInstance.Camera.Dezoom.performed += CheckZoom;
        _inputsInstance.Camera.Zoom.performed += CheckZoom;
        _inputsInstance.Camera.Rotation.performed += CheckMoveLevel;

        _inputsInstance.Player.Enable();
        _inputsInstance.Player.Move.performed += CheckMovePlayer;
    }

    #region Display Controls
    private void CheckZoom(InputAction.CallbackContext context)
    {
        _inputsInstance.Camera.Dezoom.performed -= CheckZoom;
        _inputsInstance.Camera.Zoom.performed -= CheckZoom;
        TestDisplayControlsUI();
    }

    private void CheckMovePlayer(InputAction.CallbackContext context)
    {
        _inputsInstance.Player.Move.performed -= CheckMovePlayer;
        TestDisplayControlsUI();
    }

    private void CheckMoveLevel(InputAction.CallbackContext context)
    {
        _inputsInstance.Camera.Rotation.performed -= CheckMoveLevel;
        TestDisplayControlsUI();
    }

    private void TestDisplayControlsUI()
    {
        _playerChecks++;
            //Debug.Log(_playerChecks);

        if(_playerChecks >= 3)
        _cameraIcon.SetActive(false);
    }
    #endregion

    public void DisplayGrab(bool state)
    {
        _grabIcon.transform.localScale = Vector3.one;
        _grabIcon.SetActive(state);
    }

    private void DisplayInterract(bool state)
    {
        _interractIcon.transform.localScale = Vector3.one;
        _interractIcon.SetActive(state);
    }

    private void GrabClick(Transform transform)
    {
        _grabIcon.GetComponent<Animator>().SetTrigger("Click");
        //Invoke(nameof(DisplayGrab), 1f);
    }

    private void InterractClick(Transform transform)
    {
        _interractIcon.GetComponent<Animator>().SetTrigger("Click");
    }
}