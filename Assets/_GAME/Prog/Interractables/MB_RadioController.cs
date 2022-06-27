using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MB_RadioController : MB_Interractable
{
    private AudioSource audioSource;

    AudioClip test;

    [SerializeField, Tooltip("Liste de musiques")]
    private RadioStation[] stations;

    [SerializeField, Tooltip("Son quand on change de station")]
    private AudioClip _changeStationSound;

    /// <summary>
    /// Index de la station radio actuelle
    /// </summary>
    private int actualStation = 0;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    protected override void Interract(Transform player)
    {
        player.GetComponentInChildren<Animator>().SetTrigger("Interract");

        audioSource.Stop();
        if(actualStation > stations.Length-1)
        {
            actualStation = 0;
            Debug.Log("Stop radio");
        }
        else
        {
            StartCoroutine(RadioCoroutine(actualStation));
            Debug.Log("Play : " + stations[actualStation].Name);
            actualStation++;
        }
    }

    IEnumerator RadioCoroutine(int stationIndex)
    {
        audioSource.PlayOneShot(_changeStationSound);
        yield return new WaitForSeconds(_changeStationSound.length);
            //Debug.Log(stationIndex);
        audioSource.PlayOneShot(stations[stationIndex].Songs[0]);
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