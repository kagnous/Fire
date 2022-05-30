using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
public class MB_PlayerController : MonoBehaviour
{
    [SerializeField, Tooltip("Vitesse de déplacement du personnage")]
    private float _speed = 5;

    private Rigidbody _rb;
    private Controls _inputsInstance;
    private Vector2 _directionMovment;

    [SerializeField]
    private float _grabRange = 1;
    [SerializeField]
    private Transform _grabPoint;
    private LayerMask layerMask = 1<<3;
    private bool _isGrabing; public bool IsGrabing { get { return _isGrabing; } set { _isGrabing = value; } }
    private GameObject _itemGrabed;


    // Event
    public delegate void InterractDelegate(Transform transform);
    public event InterractDelegate eventInterract;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _inputsInstance = new Controls();
        _grabPoint = transform.Find("GrabPoint");
    }

    private void OnEnable()
    {
        // Assignation des fonctions aux Inputs
        _inputsInstance.Player.Enable();
        _inputsInstance.Player.Move.performed += Move;
        _inputsInstance.Player.Grab.performed += Grab;
        _inputsInstance.Player.Interract.performed += Interract;
    }
    private void OnDisable()
    {
        // Désassignation des fonctions aux Inputs
        _inputsInstance.Player.Move.performed -= Move;
        _inputsInstance.Player.Grab.performed -= Grab;
        _inputsInstance.Player.Interract.performed -= Interract;
    }

    void FixedUpdate()
    {
        // Déplace le player en fonction de _directionMovment (pas en y) et de sa speed
        _rb.velocity = new Vector3(_directionMovment.x, _rb.velocity.y/_speed, _directionMovment.y) * _speed;

        //Le perso se tourne vers là où il se dirige si la direction est pas (0,0,0)
        if (_directionMovment != Vector2.zero)
        { transform.rotation = Quaternion.LookRotation(new Vector3(_directionMovment.x, 0, _directionMovment.y)); } // SmoothDamp
    }

    /// <summary>
    /// Récupère et met à jours l'axe de mouvement
    /// </summary>
    /// <param name="context">valeur Vecteur2 de l'input</param>
    public void Move(InputAction.CallbackContext context)
    {
        _directionMovment = context.ReadValue<Vector2>();
            //Debug.Log("Move\n"+ context.ReadValue<Vector2>());
    }

    /// <summary>
    /// Lance l'event de Grab quand l'input est appelé
    /// </summary>
    private void Grab(InputAction.CallbackContext context)
    {
        if(_isGrabing)
        {
            _itemGrabed.GetComponent<MB_Grabable>().Degrab();
            _isGrabing = false;
            _itemGrabed = null;
        }
        else
        {
            Collider[] colliders = Physics.OverlapSphere(_grabPoint.position, _grabRange, layerMask);
            if (colliders.Length > 0)
            {
                colliders[0].GetComponent<MB_Grabable>().Grab(transform);
                _isGrabing = true;
                _itemGrabed = colliders[0].gameObject;
            }
            //Debug.Log(Physics.OverlapSphere(_grabPoint.position, _grabRange, layerMask).Length);
        }
    }


    /// <summary>
    /// Lance l'event d'Interract quand l'input est appelé
    /// </summary>
    private void Interract(InputAction.CallbackContext context)
    {
        eventInterract?.Invoke(transform);
            //Debug.Log("eventInterract");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_grabPoint.position, _grabRange);
    }
}