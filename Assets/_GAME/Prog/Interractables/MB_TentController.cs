using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controle la tente et le fait de finir le level
/// </summary>
public class MB_TentController : MB_Interractable
{
    private MB_LevelManager _levelManager;
    private GameObject okLight;

    /// <summary>
    /// Si la tente est ok pour y dormir
    /// </summary>
    private bool _isOpen = false; public bool IsOpen { get { return _isOpen; } }  //set { _isOpen = value; }}

    private void Start()
    {
        _levelManager = FindObjectOfType<MB_LevelManager>();
        okLight = GetComponentInChildren<Light>().gameObject;
        okLight.SetActive(false);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<MB_PlayerController>().eventSleep += Interract;
            Debug.Log(42);
        }
    }
    protected override void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<MB_PlayerController>().eventSleep -= Interract;
            other.GetComponent<MB_PlayerController>().eventSleep -= Interract;
            Debug.Log(52);
        }
    }

    protected override void Interract(Transform player)
    {
        if(_isOpen)
        {
            player.GetComponentInChildren<Animator>().SetTrigger("Interract");
            _levelManager.FinishLevel();
        }
        else
        {
            Debug.Log("no");
        }
    }

    public void OpenTent()
    {
        // Si la tente n'est pas déjà ouverte
        if(!_isOpen)
        {
            _isOpen = true;
            okLight.SetActive(true);
            GetComponentInChildren<Animator>().SetTrigger("OpenTent");
            GetComponent<AudioSource>().Play();
        }
    }
}