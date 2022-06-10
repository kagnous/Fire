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

    [Header("Flammes Settings")]

    [SerializeField, Tooltip("Augmentaion de la taille du feu")]
    private float _upSize;      public float UpSize => _upSize;

    [SerializeField, Tooltip("Velocité des flammes")]
    private Vector3 _flameVelocity; public Vector3 FlameVelocity => _flameVelocity;

    [SerializeField, Tooltip("Augmentation de la durée de vie des flammes")]
    private float _flameLifeTime;   public float FlameLifeTime => _flameLifeTime;

    [SerializeField, Tooltip("Si le feu doit changer de couleur")]
    private bool _changeColor = false; public bool ChangeColor { get { return _changeColor; } set { _changeColor = value; } }

    [SerializeField, GradientUsage(true), Tooltip("Couleur que prend le feu\n(uniquement si Change Color est coché")]
    private Gradient _color;   public Gradient Color { get { return _color; } set { _color = value; } }

    [Header("Smoke Settings")]

    [SerializeField, Tooltip("Velocité de la fumée")]
    private Vector3 _smokeVelocity; public Vector3 SmokeVelocity => _smokeVelocity;

    [SerializeField, Tooltip("Augmentation de la durée de vie de la fumée")]
    private float _smokeLifeTime; public float SmokeLifeTime => _smokeLifeTime;

    [Header("Ember Settings")]

    [SerializeField, Tooltip("Velocité des cendres")]
    private Vector3 _emberVelocity; public Vector3 EmberVelocity => _emberVelocity;

    [SerializeField, Tooltip("Augmentation de la durée de vie des cendres")]
    private float _emberLifeTime; public float EmberLifeTime => _emberLifeTime;
}