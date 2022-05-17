using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFuel", menuName = "Fuel")]
public class SO_FuelParameters : ScriptableObject
{
    [SerializeField, Tooltip("Forme des flammes")]
    private Texture _texture;   public Texture Texture => _texture;

    [SerializeField, Tooltip("Augmentaion de la taille du feu")]
    private float _upSize;       public float UpSize => _upSize;

    [GradientUsage(true), SerializeField, Tooltip("Couleur que prend le feu")]
    public Gradient _color;
}