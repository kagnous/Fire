using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Controls _inputsInstance;
    private Vector2 _directionMovment;

    public float speed = 1;

    // Event
    public delegate void InterractDelegate(Transform transform);
    public event InterractDelegate eventInterract;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        _inputsInstance = new Controls();
    }

    private void OnEnable()
    {
        _inputsInstance.Player.Enable();
        _inputsInstance.Player.Move.performed += Move;
        _inputsInstance.Player.Grab.performed += Interract;
    }
    private void OnDisable()
    {
        _inputsInstance.Player.Move.performed -= Move;
        _inputsInstance.Player.Grab.performed -= Interract;
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector3(_directionMovment.x, -0.1f, _directionMovment.y) * speed;

        //Le perso se tourne vers là où il se dirige si la direction est pas (0,0,0)
        if (_directionMovment != Vector2.zero)
        { transform.rotation = Quaternion.LookRotation(new Vector3(_directionMovment.x, 0, _directionMovment.y)); }
    }

    public void Move(InputAction.CallbackContext context)
    {
        _directionMovment = context.ReadValue<Vector2>();
        //Debug.Log("Move\n"+ context.ReadValue<Vector2>());
    }

    private void Interract(InputAction.CallbackContext context)
    {
        //Debug.Log(eventInterract.);

        eventInterract?.Invoke(transform);
    }
}