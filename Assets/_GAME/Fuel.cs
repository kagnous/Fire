using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFuel", menuName = "Fuel")]
public class Fuel : ScriptableObject
{
    [SerializeField, Tooltip("Couleur du feu à la combustion")]
    private Color fireColor; public Color FireColor => fireColor;

    [SerializeField, Tooltip("Material des particules")]
    private Material material; public Material ParticuleMaterial => material;
}