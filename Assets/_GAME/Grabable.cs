using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabable : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject level;

    [SerializeField, Tooltip("Comportement du feu à combustion")]
    private Fuel fuel;  public Fuel FuelParam => fuel;

    public bool _isGrabed = false;
    private int test = 0;

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
            Debug.Log("In range");
            test++;
            if(test > 1)
                test = 1;
            else
            other.gameObject.GetComponent<PlayerController>().eventInterract += Grab;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("No in range");
            test--;
            collision.gameObject.GetComponent<PlayerController>().eventInterract -= Grab;
        }
    }

    private void Grab(Transform parent)
    {
        //Debug.Log("Fonction grab");
        if(!_isGrabed)
        {
            //Debug.Log("Grab");
            Destroy(rb);
            transform.rotation = parent.rotation;
            transform.position = parent.Find("GrabPoint").position;
            transform.SetParent(parent);
            _isGrabed = true;
        }
        else
        {
            //Debug.Log("Degrab");
            transform.SetParent(level.transform);
            rb = gameObject.AddComponent<Rigidbody>();
            _isGrabed = false;
        }
    }

    public void Burn()
    {
        FindObjectOfType<PlayerController>().eventInterract -= Grab;
        Destroy(gameObject);
    }
}