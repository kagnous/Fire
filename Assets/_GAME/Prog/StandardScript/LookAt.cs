using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    private Transform _camera;

    private void Start()
    {
        _camera = FindObjectOfType<Camera>().transform;
    }

    void FixedUpdate()
    {
        transform.LookAt(_camera);
    }
}