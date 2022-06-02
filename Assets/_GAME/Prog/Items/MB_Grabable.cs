using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class MB_Grabable : MonoBehaviour
{
    [SerializeField, Tooltip("Comportement du feu à combustion")]
    private SO_FuelParameters fuel;  public SO_FuelParameters FuelParam => fuel;

    private Rigidbody rb;
    private GameObject level;
    private GameObject _canvas;

    private ParticleSystem _ps;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        _ps = GetComponent<ParticleSystem>();
        _canvas = GetComponentInChildren<Canvas>().gameObject;
        _canvas.SetActive(false);
    }
    private void Start()
    {
        level = GameObject.FindGameObjectWithTag("Level");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
                //Debug.Log("In range");
            _canvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
                //Debug.Log("No in range");
            _canvas.SetActive(false);
        }
    }

    public void Grab(Transform parent)
    {
        //Debug.Log("Grab");

        // On détruit le rigidbody, positionne l'objet par rapport au player puis on met l'objet en enfant
        Destroy(rb);
        transform.rotation = parent.rotation;
        transform.position = parent.Find("GrabPoint").position;
        transform.SetParent(parent);
        _canvas.SetActive(false);
    }

    public void Degrab()
    {
        // On récrée un rigidbody et remet l'objet en en enfant du niveau
        transform.SetParent(level.transform);
        rb = gameObject.AddComponent<Rigidbody>();
    }

    /// <summary>
    /// Actions à effectuer avant de détruire l'objet par le feu
    /// </summary>
    public void Burn(Transform fire)
    {
        //FindObjectOfType<MB_PlayerController>().eventGrab -= Grab;
        FindObjectOfType<MB_PlayerController>().IsGrabing = false;

        // On active les particules de destructions
        _ps.Play();
        // On efface les trucs chiants de l'objet
        GetComponent<Collider>().enabled = false;
        GetComponentInChildren<MeshRenderer>().enabled = false;

        // On met le combustible sur le feu
        transform.position = fire.position;
        transform.SetParent(fire);

        // On détruit l'obet quand il a finis de faire ses particules
            //Debug.Log(_ps.main.startLifetimeMultiplier + _ps.main.duration);
        Destroy(gameObject, _ps.main.startLifetimeMultiplier + _ps.main.duration);
    }
}