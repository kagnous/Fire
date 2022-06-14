using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MB_UIGrab : MonoBehaviour
{
    public void GrabEventKey()
    {
        transform.parent.GetComponent<MB_UIController>().DisplayGrab(false);
    }
}