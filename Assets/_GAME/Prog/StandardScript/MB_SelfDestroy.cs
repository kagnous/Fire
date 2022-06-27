using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MB_SelfDestroy : MonoBehaviour
{
    [SerializeField]
    private float _timeBeforeDestroy = 5;

    void Start()
    {
        Destroy(gameObject,_timeBeforeDestroy);
    }
}