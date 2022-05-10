using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    private ParticleSystem particleSysteme;

    private void Awake()
    {
        particleSysteme = GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fuel"))
        {
            transform.localScale = new Vector3(transform.localScale.x +0.2f, transform.localScale.y + 0.2f, transform.localScale.z + 0.2f);

            Grabable fuel = other.GetComponent<Grabable>();
            ParticleSystemRenderer psr = new ParticleSystemRenderer();
            psr.material = fuel.FuelParam.ParticuleMaterial;
            
            fuel.Burn();
        }
    }
}