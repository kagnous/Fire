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

    private bool _isGrabed = false;
    private int debugAssign = 0;

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
            debugAssign++;
            // Si debugAssign = 2 ou plus, alors c'est qu'il n'a pas détecté la dernière sortie de collision et on réabonne pas
            if(debugAssign > 1)
                debugAssign = 1;
            else
            // On abonne la fonction Grab à l'event d'Interract du PlayerController
            other.gameObject.GetComponent<MB_PlayerController>().eventGrab += Grab;

            _canvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
                //Debug.Log("No in range");
            debugAssign--;
            // On désabonne la fonction Grab à l'event d'Interract du PlayerController
            collision.gameObject.GetComponent<MB_PlayerController>().eventGrab -= Grab;

            _canvas.SetActive(false);
        }
    }

    private void Grab(Transform parent)
    {
            //Debug.Log("Fonction grab");
        if(!_isGrabed)
        {
                //Debug.Log("Grab");

            // On détruit le rigidbody, positionne l'objet par rapport au player puis on met l'objet en enfant
            Destroy(rb);
            transform.rotation = parent.rotation;
            transform.position = parent.Find("GrabPoint").position;
            transform.SetParent(parent);
            _isGrabed = true;
            _canvas.SetActive(false);
        }
        else
        {
                //Debug.Log("Degrab");

            // On récrée un rigidbody et remet l'objet en en enfant du niveau
            transform.SetParent(level.transform);
            rb = gameObject.AddComponent<Rigidbody>();
            _isGrabed = false;
        }
    }

    /// <summary>
    /// Actions à effectuer avant de détruire l'objet par le feu
    /// </summary>
    public void Burn(Transform fire)
    {
        FindObjectOfType<MB_PlayerController>().eventGrab -= Grab;
        // On active les particules de destructions
        _ps.Play();
        // On efface les trucs chiants de l'objet
        GetComponent<Collider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;

        // On met le combustible sur le feu
        transform.position = fire.position;
        transform.SetParent(fire);

        // On détruit l'obet quand il a finis de faire ses particules
        Destroy(gameObject, _ps.main.duration);
    }
}