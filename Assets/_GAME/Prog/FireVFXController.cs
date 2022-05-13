using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FireVFXController : MonoBehaviour
{
    private VisualEffect _VFX;
    private ParticleSystem _light;
    private ParticleSystem.MainModule mainModule;

    [SerializeField, Tooltip("Forme des flammes")]
    private Texture _texture;

    [SerializeField, Tooltip("Augmentaion de la taille du feu")]
    public float _upSize;

    [GradientUsage(true), SerializeField, Tooltip("Couleur que prend le feu")]
    public Gradient _color;

    private void Awake()
    {
        _VFX = GetComponentInChildren<VisualEffect>();
        _light = GetComponentInChildren<ParticleSystem>();
        mainModule = _light.main;
    }

    /// <summary>
    /// Change les paramètres du feu en fonction des paramètres du script
    /// </summary>
    public void ChangeVFX ()
    {
        //_VFX.Stop();
        //_VFX.SetTexture("FlameTexture", _texture);
        _VFX.SetGradient("FlameGradient", _color);


        mainModule.startColor = Color.green;//_color.colorKeys[0].color;
        //_light.startColor = _color.colorKeys[0].color;
        IncreaseSize("FlameMaxSize", "FlameMinSize");
    }

    /// <summary>
    /// Augmente les paramètres entrés d'une valeur égale à _upSize
    /// </summary>
    /// <param name="variableMax"></param>
    /// <param name="variableMin"></param>
    private void IncreaseSize(string variableMax, string variableMin)
    {
        _VFX.SetFloat(variableMax, _VFX.GetFloat(variableMax) + _upSize);
        _VFX.SetFloat(variableMin, _VFX.GetFloat(variableMin) + _upSize);
    }
}