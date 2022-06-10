using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MB_UIController : MonoBehaviour
{
    private GameObject _cameraIcon;

    private void Start()
    {
        _cameraIcon = transform.Find("CameraIcon").gameObject;
    }
}
        //_cameraIcon.SetActive(false);