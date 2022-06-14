using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

/// <summary>
/// Controle et modifie les VFX du feu
/// </summary>
[DisallowMultipleComponent]
public class MB_FireVFXController : MonoBehaviour
{
    /// <summary>
    /// Le principal VFX du feu (flammes, fumée...)
    /// </summary>
    private VisualEffect _VFX;
    /// <summary>
    /// Particle System pour la lumière
    /// </summary>
    private ParticleSystem _light;
    /// <summary>
    /// Module principal du particle system
    /// </summary>
    private ParticleSystem.MainModule mainModule;

    private void Awake()
    {
        _VFX = GetComponentInChildren<VisualEffect>();
        _light = GetComponentInChildren<ParticleSystem>();
        mainModule = _light.main;
    }

    /// <summary>
    /// Change les paramètres du feu en fonction des paramètres du script
    /// </summary>
    public void ChangeVFX (SO_FuelParameters fuel)
    {
        //_VFX.Stop();
        _VFX.SetTexture("FlameTexture", fuel.Texture);

        IncreaseSize("FlameMaxSize", "FlameMinSize", fuel.UpSize);

        _VFX.SetVector3("FlameVelocity", fuel.FlameVelocity);

        IncreaseSize("FlameMaxLifetime", "FlameMinLifetime", fuel.FlameLifeTime);

        if(fuel.ChangeColor)
        {
            _VFX.SetGradient("FlameGradient", fuel.Color);
            mainModule.startColor = fuel.Color.colorKeys[0].color;
        }

        _VFX.SetVector3("SmokeVelocity", fuel.SmokeVelocity);
        IncreaseSize("SmokeMaxLifetime", "SmokeMinLifetime", fuel.SmokeLifeTime);

        _VFX.SetVector3("EmberVelocity", fuel.EmberVelocity);
        IncreaseSize("EmberMaxLifetime", "EmberMinLifetime", fuel.EmberLifeTime);
    }

    /// <summary>
    /// Augmente les paramètres entrés d'une valeur égale à _upSize
    /// </summary>
    /// <param name="variableMax"></param>
    /// <param name="variableMin"></param>
    private void IncreaseSize(string variableMax, string variableMin, float upSize)
    {
        _VFX.SetFloat(variableMax, _VFX.GetFloat(variableMax) + upSize);
        _VFX.SetFloat(variableMin, _VFX.GetFloat(variableMin) + upSize);
    }
}