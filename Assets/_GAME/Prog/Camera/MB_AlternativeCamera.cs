using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MB_AlternativeCamera : MonoBehaviour
{
    private Controls _inputsInstance;
    private Vector2 _rotateAxis;
    private Camera _camera;
    private GameObject _level;

    private float _zoomValue = 0;
    private float _dezoomValue = 0;

    private void Awake()
    {
        _inputsInstance = new Controls();
    }
    private void Start()
    {
        _level = GameObject.FindGameObjectWithTag("Level");
        _camera = FindObjectOfType<Camera>();
    }

    private void OnEnable()
    {
        // Assignation des fonctions aux Inputs
        _inputsInstance.Camera.Enable();
        _inputsInstance.Camera.Rotation.performed += Rotate;
        _inputsInstance.Camera.Zoom.performed += Zoom;
        _inputsInstance.Camera.Dezoom.performed += Dezoom;
    }
    private void OnDisable()
    {
        // D�sassignation des fonctions aux Inputs
        _inputsInstance.Camera.Rotation.performed -= Rotate;
        _inputsInstance.Camera.Zoom.performed -= Zoom;
        _inputsInstance.Camera.Dezoom.performed -= Dezoom;
    }

    private void FixedUpdate()
    {
        // Tourne le diorama selon la valeur de _rotateAxis
        _level.transform.Rotate(_rotateAxis);

        //_camera.transform.LookAt(transform);

        // D�place la cam�ra vers le diorama en fonction des 2 valeurs de zoom (si �gales ou � 0, pas de mouvement car *0)
        _camera.transform.Translate((transform.position - _camera.transform.position) * Time.deltaTime * (_zoomValue + -_dezoomValue), Space.World);
    }

    /// <summary>
    /// R�cup�re et met � jours l'axe de rotation
    /// </summary>
    /// <param name="context">valeur Vecteur2 de l'input</param>
    private void Rotate(InputAction.CallbackContext context)
    {
        _rotateAxis = new Vector2(0, context.ReadValue<Vector2>().x);
    }

    /// <summary>
    /// R�cup�re et met � jours la valeur de zoom
    /// </summary>
    /// <param name="context">valeur float de l'input</param>
    private void Zoom(InputAction.CallbackContext context)
    {
        //Debug.Log("Zoom");
        _zoomValue = context.ReadValue<float>();
    }

    /// <summary>
    /// R�cup�re et met � jours la valeur de d�zoom
    /// </summary>
    /// <param name="context">valeur float de l'input</param>
    private void Dezoom(InputAction.CallbackContext context)
    {
        //Debug.Log("Dezoom");
        _dezoomValue = context.ReadValue<float>();
    }
}