using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public abstract class MB_Interractable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<MB_PlayerController>().eventInterract += Interract;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<MB_PlayerController>().eventInterract -= Interract;
        }
    }

    /// <summary>
    /// Fonction appelée lorsque le player active l'interraction
    /// </summary>
    /// <param name="player">La transform du player</param>
    protected virtual void Interract(Transform player) { }
}