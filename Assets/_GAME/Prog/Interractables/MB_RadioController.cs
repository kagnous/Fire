using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MB_RadioController : MB_Interractable
{
    private AudioSource audioSource;

    AudioClip test;

    [SerializeField, Tooltip("Liste de musiques")]
    private RadioStation[] stations;

    /// <summary>
    /// Index de la station radio actuelle
    /// </summary>
    private int actualStation = 0;

    private void Start()
    {
        audioSource = FindObjectOfType<AudioSource>();
    }

    protected override void Interract(Transform player)
    {
        audioSource.Stop();
        if(actualStation > stations.Length-1)
        {
            actualStation = 0;
            Debug.Log("Stop radio");
        }
        else
        {
            audioSource.PlayOneShot(stations[actualStation].Songs[0]);
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
    private AudioClip[] songs; public AudioClip[] Songs => songs;
}