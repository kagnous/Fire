using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MB_AnimalsController : MonoBehaviour
{
    private MB_LevelManager _levelManager;

    [SerializeField, Tooltip("Niveau du feu pour faire venir l'animal")]
    private int _fireLevel = 1;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private float _speed = 1;

    private bool isGoOut = false;

    private SkinnedMeshRenderer[] meshes;

    void Start()
    {
        //GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
        meshes = GetComponentsInChildren<SkinnedMeshRenderer>();
        for (int i = 0; i < meshes.Length; i++)
        {
            meshes[i].enabled = false;
        }

        _levelManager = FindObjectOfType<MB_LevelManager>();
        _levelManager.eventFire += Move;
    }

    private void FixedUpdate()
    {
        if(isGoOut)
        {
            if (Vector3.Distance(transform.position, target.position) > 0.3f)
            {
                Vector3 dir = target.position - transform.position;
                transform.Translate(dir.normalized * _speed * Time.fixedDeltaTime, Space.World);
            }
            else
            {
                // Anim idle
                GetComponentInChildren<Animator>().SetTrigger("InPosition");
                this.enabled = false;
            }
        }
    }

    private void Move(int fire)
    {
        if(fire >= _fireLevel)
        {
            isGoOut = true;
            //GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
            meshes = GetComponentsInChildren<SkinnedMeshRenderer>();
            for (int i = 0; i < meshes.Length; i++)
            {
                meshes[i].enabled = true;
            }
            // Anim marche
            _levelManager.eventFire -= Move;
        }
    }
}