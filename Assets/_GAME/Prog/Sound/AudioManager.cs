using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Reference to the Audio Source component used to play the clips in the list.")]
    private AudioSource _source = null;

    private void Awake()
    {
        if (_source == null) { _source = GetComponent<AudioSource>(); }
    }
}