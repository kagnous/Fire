using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MB_AmbianceController : MonoBehaviour
{
    private MB_LevelManager _levelManager;

    private GameObject wind;
    [SerializeField]
    private AudioSource _crickets;
    [SerializeField]
    private AudioSource _owl;
    [SerializeField]
    private GameObject _fireflies;

    [SerializeField, Tooltip("Niveau du feu a l'apparition du vent")]
    private int _windValue;

    [SerializeField, Tooltip("Niveau du feu a l'apparition du bruit de chouette")]
    private int _owlValue;

    [SerializeField, Tooltip("Niveau du feu a l'apparition du bruit de crickets")]
    private int _cricketsValue;

    [SerializeField, Tooltip("Niveau du feu a l'apparition des lucioles")]
    private int _firefliesValue;

    void Start()
    {
        _levelManager = FindObjectOfType<MB_LevelManager>();
        _levelManager.eventFire += TestEffects;

        wind = FindObjectOfType<WindInstancier>().gameObject;
        if (_windValue > 0)
            wind.SetActive(false);

        if (_cricketsValue > 0)
            _crickets.enabled = false;

        if (_owlValue > 0)
            _owl.enabled = false;

        if (_firefliesValue > 0)
            _fireflies.SetActive(false);
    }

    private void TestEffects(int fireValue)
    {
        if (fireValue >= _windValue)
            wind.SetActive(true);

        if (fireValue >= _cricketsValue)
            _crickets.enabled = true;

        if (fireValue >= _owlValue)
            _owl.enabled = true;

        if (fireValue >= _firefliesValue)
            _fireflies.SetActive(true);
    }
}