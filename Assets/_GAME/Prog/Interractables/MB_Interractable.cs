using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public abstract class MB_Interractable : MonoBehaviour
{
    protected GameObject _canvas;

    private void Awake()
    {
        _canvas = GetComponentInChildren<Canvas>().gameObject;
        _canvas.SetActive(false);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<MB_PlayerController>().eventInterract += Interract;
            _canvas.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<MB_PlayerController>().eventInterract -= Interract;
            _canvas.SetActive(false);
        }
    }

    /// <summary>
    /// Fonction appelée lorsque le player active l'interraction
    /// </summary>
    /// <param name="player">La transform du player</param>
    protected virtual void Interract(Transform player) { }
}