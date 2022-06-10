using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MB_AutoRotate : MonoBehaviour
{
    [SerializeField]
    private Vector3 _rotateSpeed = new Vector3(0, 1, 0);


    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(_rotateSpeed * Time.fixedDeltaTime);
    }
}
