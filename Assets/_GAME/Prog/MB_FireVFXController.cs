using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

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

    //Event
    public delegate void FireChange();
    public event FireChange eventFireChange;

    private void Awake()
    {
        _VFX = GetComponentInChildren<VisualEffect>();
        _light = GetComponentInChildren<ParticleSystem>();
        mainModule = _light.main;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fuel"))
        {
            // On récupère le script de combustible
            MB_Grabable item = other.GetComponent<MB_Grabable>();

            // On change les VFX selon les paramètres du carburant
            ChangeVFX(item.FuelParam);

            // On dit que le feu a changé de comportement
            eventFireChange?.Invoke();

            // On brûle (détruit) le combustible
            item.Burn();
        }
    }

    /// <summary>
    /// Change les paramètres du feu en fonction des paramètres du script
    /// </summary>
    public void ChangeVFX (SO_FuelParameters fuel)
    {
        //_VFX.Stop();
        _VFX.SetTexture("FlameTexture", fuel.Texture);
        _VFX.SetGradient("FlameGradient", fuel._color);

        mainModule.startColor = fuel._color.colorKeys[0].color;
        IncreaseSize("FlameMaxSize", "FlameMinSize", fuel.UpSize);
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