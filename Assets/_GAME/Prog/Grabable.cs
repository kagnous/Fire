using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Grabable : MonoBehaviour
{
    [SerializeField, Tooltip("Comportement du feu � combustion")]
    private Fuel fuel;  public Fuel FuelParam => fuel;

    private Rigidbody rb;
    private GameObject level;

    private bool _isGrabed = false;
    private int debugAssign = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
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
            // Si debugAssign = 2 ou plus, alors c'est qu'il n'a pas d�tect� la derni�re sortie de collision et on r�abonne pas
            if(debugAssign > 1)
                debugAssign = 1;
            else
            // On abonne la fonction Grab � l'event d'Interract du PlayerController
            other.gameObject.GetComponent<PlayerController>().eventInterract += Grab;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
                //Debug.Log("No in range");
            debugAssign--;
            // On d�sabonne la fonction Grab � l'event d'Interract du PlayerController
            collision.gameObject.GetComponent<PlayerController>().eventInterract -= Grab;
        }
    }

    private void Grab(Transform parent)
    {
            //Debug.Log("Fonction grab");
        if(!_isGrabed)
        {
                //Debug.Log("Grab");

            // On d�truit le rigidbody, positionne l'objet par rapport au player puis on met l'objet en enfant
            Destroy(rb);
            transform.rotation = parent.rotation;
            transform.position = parent.Find("GrabPoint").position;
            transform.SetParent(parent);
            _isGrabed = true;
        }
        else
        {
                //Debug.Log("Degrab");

            // On r�cr�e un rigidbody et remet l'objet en en enfant du niveau
            transform.SetParent(level.transform);
            rb = gameObject.AddComponent<Rigidbody>();
            _isGrabed = false;
        }
    }

    /// <summary>
    /// Actions � effectuer avant de d�truire l'objet par le feu
    /// </summary>
    public void Burn()
    {
        FindObjectOfType<PlayerController>().eventInterract -= Grab;
        Destroy(gameObject);
    }
}