using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    // Nik les conventions, l� c'est plus sipmle pour s'y retrouver avec les diff�rents ParticuleSystem
    private ParticleSystem PSystem;
    private ParticleSystemRenderer PSRenderer;
    private ParticleSystem.ShapeModule PSShape;

    private void Awake()
    {
        PSystem = GetComponent<ParticleSystem>();
        PSRenderer = GetComponent<ParticleSystemRenderer>();
        PSShape = PSystem.shape;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fuel"))
        {
            // On r�cup�re le script de combustible
            MB_Grabable fuel = other.GetComponent<MB_Grabable>();

            // On set le feu en fonction des param�tres du combustible
            //PSRenderer.material = fuel.FuelParam.ParticuleMaterial;
            //PSShape.shapeType = fuel.FuelParam.ShapeType;

            // On br�le (d�truit) le combustible
            fuel.Burn();
        }
    }
}