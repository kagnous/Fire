using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    private Controls _inputsInstance;
    private Vector2 _rotateAxis;
    private Camera _camera;

    private float _zoomValue = 0;
    private float _dezoomValue = 0;

    private void Awake()
    {
        _inputsInstance = new Controls();
    }
    private void Start()
    {
        _camera = FindObjectOfType<Camera>();
    }

    private void OnEnable()
    {
        _inputsInstance.Camera.Enable();
        _inputsInstance.Camera.Rotation.performed += Rotate;
        _inputsInstance.Camera.Zoom.performed += Zoom;
        _inputsInstance.Camera.Dezoom.performed += Dezoom;
    }
    private void OnDisable()
    {
        _inputsInstance.Camera.Rotation.performed -= Rotate;
        _inputsInstance.Camera.Zoom.performed -= Zoom;
        _inputsInstance.Camera.Dezoom.performed -= Dezoom;
    }

    private void FixedUpdate()
    {
        transform.Rotate(_rotateAxis);

        _camera.transform.Translate((transform.position - _camera.transform.position) * Time.deltaTime * (_zoomValue + -_dezoomValue), Space.World);
    }

    private void Rotate(InputAction.CallbackContext context)
    {
        _rotateAxis = new Vector2(0, context.ReadValue<Vector2>().x);
    }

    private void Zoom(InputAction.CallbackContext context)
    {
        //Debug.Log("Zoom");
        _zoomValue = context.ReadValue<float>();
    }

    private void Dezoom(InputAction.CallbackContext context)
    {
        //Debug.Log("Dezoom");
        _dezoomValue = context.ReadValue<float>();
    }
}