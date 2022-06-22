using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MB_Champi : MonoBehaviour
{
    private MB_LevelManager _levelManager;

    [SerializeField]
    private int _fireLevel;

    [SerializeField]
    private float _emissiveValue = 5;

    private Material mat;

    private void Start()
    {
        mat = GetComponent<MeshRenderer>().material;

        if(_fireLevel > 0)
        mat.SetFloat("_Intensity", 0);

        _levelManager = FindObjectOfType<MB_LevelManager>();
        _levelManager.eventFire += ChangeEmissive;
    }

    private void ChangeEmissive(int i)
    {
        if(i >= _fireLevel)
        {
            //mat.SetFloat("_Intensity", _emissiveValue);
            StartCoroutine(nameof(GlowCoroutine));
            _levelManager.eventFire -= ChangeEmissive;
        }
    }

    IEnumerator GlowCoroutine()
    {
        float intensiti = 0;
        while(intensiti < _emissiveValue)
        {
            intensiti += 0.05f;
            mat.SetFloat("_Intensity", intensiti);
            yield return new WaitForSeconds(0.05f);
        }

    }
}