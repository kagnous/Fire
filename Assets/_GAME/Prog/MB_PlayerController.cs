using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
public class MB_PlayerController : MonoBehaviour
{
    #region Audio

    [Header("Audio")]
    [SerializeField, Tooltip("Bruits de pas d'Ernest")]
    private AudioClip[] _stepsSounds;
    [SerializeField, Tooltip("Bruit de Grab")]
    private AudioClip _grabSound;
    [SerializeField, Tooltip("Bruit de Degrab")]
    private AudioClip _degrabSound;

    private AudioSource _audioSource;

    [Header("\n")]
    #endregion


    [SerializeField, Tooltip("Vitesse maximale de déplacement du personnage")]
    private float _maxSpeed = 5;

    [SerializeField, Tooltip("Vitesse d'acceleration du personnage")]
    private float _acceleration = 2f;

    [SerializeField, Tooltip("Acceleration selon la vitesse de base")]
    private AnimationCurve _accelerationCurve;

    [SerializeField]
    private float _turnSmoothTime = 0.5f;

    private float _currentVelocity;


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
    public event InterractDelegate eventGrab;
    
    public delegate void PlayerInRange(bool state);
    public event PlayerInRange eventInGrabRange;
    public event PlayerInRange eventInInterractRange;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

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
        float speed = _maxSpeed * _directionMovment.magnitude;

        // Déplace le player en fonction de _directionMovment (pas en y) et de sa speed
        _rb.velocity += new Vector3(_directionMovment.x, _rb.velocity.y/_maxSpeed, _directionMovment.y) * _acceleration * _accelerationCurve.Evaluate(_rb.velocity.magnitude/_maxSpeed);

        //Le perso se tourne vers là où il se dirige si la direction est pas (0,0,0)
        if (_directionMovment != Vector2.zero)
        { 
            float targetAngle = Mathf.Atan2(_directionMovment.x, _directionMovment.y) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _currentVelocity, _turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            
            //transform.rotation = Quaternion.LookRotation(new Vector3(_directionMovment.x, 0, _directionMovment.y)); 
        
        } // SmoothDamp
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
            _audioSource.PlayOneShot(_degrabSound);
            _isGrabing = false;
            _itemGrabed = null;
        }
        else
        {
            Collider[] colliders = Physics.OverlapSphere(_grabPoint.position, _grabRange, layerMask);
            if (colliders.Length > 0)
            {
                colliders[0].GetComponent<MB_Grabable>().Grab(transform);
                _audioSource.PlayOneShot(_grabSound);
                _isGrabing = true;
                _itemGrabed = colliders[0].gameObject;

                eventGrab?.Invoke(transform);
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

    private void PlayWalk()
    {
        _audioSource.Stop();
        _audioSource.clip = _stepsSounds[Random.Range(0, _stepsSounds.Length)];
        _audioSource.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Fuel")
        {
            eventInGrabRange?.Invoke(true);
        }
        else if (other.tag == "Interractable")
        {
            if(other.name == "Tent")
            {
                if(other.GetComponent<MB_TentController>().IsOpen)
                    eventInInterractRange?.Invoke(true);
            }
            else
            eventInInterractRange?.Invoke(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Fuel")
        {
            eventInGrabRange?.Invoke(false);
        }
        else if(other.tag == "Interractable")
        {
            eventInInterractRange?.Invoke(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_grabPoint.position, _grabRange);
    }
}