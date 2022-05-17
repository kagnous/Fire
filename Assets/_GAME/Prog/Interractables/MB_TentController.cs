using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MB_TentController : MB_Interractable
{
    [SerializeField, Tooltip("Nombre d'interraction requise avec le feu pour finir le niveau")]
    private int _minInterract = 3;

    /// <summary>
    /// Nombre d'interractions déjà effectuées par le joueur
    /// </summary>
    private int actualFireInterract = 0;

    private void Start()
    {
        FindObjectOfType<MB_FireVFXController>().eventFireChange += FireIncrease;
    }
    /*private void OnDisable()
    {
        FindObjectOfType<FireVFXController>().eventFireChange -= FireIncrease;
    }*/

    protected override void Interract(Transform player)
    {
        if(actualFireInterract >= _minInterract)
        {
            Debug.Log("Dodo\nFin du niveau");
        }
        else
        {
            Debug.Log("nop : " + actualFireInterract);
        }
    }

    /// <summary>
    /// Fonction qui incrémente le feu
    /// </summary>
    private void FireIncrease()
    {
        actualFireInterract++;
    }
}