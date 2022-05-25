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
        base.OnTriggerEnter(other);

        if(!_isOpen)
            _canvas.SetActive(false);
    }

    protected override void Interract(Transform player)
    {
        if(_isOpen)
        {
            _levelManager.FinishLevel("MainMenu");
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
            //Animation de tente qui s'ouvre ?
        }
    }
}