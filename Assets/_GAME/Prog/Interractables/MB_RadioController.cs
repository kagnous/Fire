using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MB_RadioController : MB_Interractable
{
    [SerializeField, Tooltip("Liste de musiques")]
    private RadioStation[] stations;

    /// <summary>
    /// Index de la station radio actuelle
    /// </summary>
    private int actualStation = 0;

    protected override void Interract(Transform player)
    {
        if(actualStation > stations.Length-1)
        {
            actualStation = 0;
            Debug.Log("Stop radio");
            // Coupe la radio
        }
        else
        {
            Debug.Log("Play : " + stations[actualStation].Name);
            actualStation++;
        }
    }
}

[System.Serializable]
public struct RadioStation
{
    [SerializeField, Tooltip("Nom de la station")]
    private string name; public string Name => name;

    [SerializeField, Tooltip("Liste des musiques jouées par la station")]
    private int[] songs; public int[] Songs => songs;
}