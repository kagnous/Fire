using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controle et modifie les valeurs et �tats du feu
/// </summary>
public class MB_FireController : MonoBehaviour
{
    private MB_LevelManager _levelManager;

    private MB_FireVFXController _VFX;

    private void Start()
    {
        _levelManager = FindObjectOfType<MB_LevelManager>();
        _VFX = GetComponentInChildren<MB_FireVFXController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fuel"))
        {
            // On r�cup�re le script de combustible
            MB_Grabable item = other.GetComponent<MB_Grabable>();

            // On change les VFX selon les param�tres du combustible
            _VFX.ChangeVFX(item.FuelParam);

            // On dit qu'il y a eu une interraction (selon la valeur de l'objet)
            _levelManager.AddInterractions(item.FuelParam.InterractionValue);

            // On br�le (d�truit) le combustible
            item.Burn(transform);

            if(TryGetComponent(out AudioSource audioSource))
                audioSource.Play();
        }
    }
}