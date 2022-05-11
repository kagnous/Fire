using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFuel", menuName = "Fuel")]
public class Fuel : ScriptableObject
{
    [SerializeField, Tooltip("Material des particules")]
    private Material material; public Material ParticuleMaterial => material;

    [SerializeField, Tooltip("Forme du feu")]
    private ParticleSystemShapeType shapeType; public ParticleSystemShapeType ShapeType => shapeType;
}