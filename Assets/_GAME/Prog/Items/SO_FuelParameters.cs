using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Paramètres du combustibles
/// </summary>
[CreateAssetMenu(fileName = "NewFuel", menuName = "Fuel")]
public class SO_FuelParameters : ScriptableObject
{
    [SerializeField, Tooltip("Nombre d'interraction que vaut la combustion de l'objet"), Min(0)]
    private int _interractionValue = 1; public int InterractionValue => _interractionValue;

    [SerializeField, Tooltip("Forme des flammes")]
    private Texture _texture;   public Texture Texture => _texture;

    [SerializeField, Tooltip("Augmentaion de la taille du feu")]
    private float _upSize;      public float UpSize => _upSize;

    [SerializeField, Tooltip("Si le feu doit changer de formes")]
    private bool _changeColor = false; public bool ChangeColor { get { return _changeColor; } set { _changeColor = value; } }

    [SerializeField, GradientUsage(true), Tooltip("Couleur que prend le feu\n(uniquement si Change Color est coché")]
    private Gradient _color;   public Gradient Color { get { return _color; } set { _color = value; } }
}