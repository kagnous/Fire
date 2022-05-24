using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controle les valeurs essentielles du niveau
/// </summary>
[DisallowMultipleComponent]
public class MB_LevelManager : MonoBehaviour
{
    private GameObject _player;
    private MB_TentController _tent;

    [SerializeField, Tooltip("Nombre d'interractions requises pour terminer le niveau")]
    private int _interractionsMin = 1;

    /// <summary>
    /// Nombre d'interactions entre le joueur et le niveau
    /// </summary>
    private int _interractions; public int Interractions { get { return _interractions; } set { _interractions = value; } }

    void Start()
    {
        _player = FindObjectOfType<MB_PlayerController>().gameObject;
        _tent = FindObjectOfType<MB_TentController>();
    }

    /// <summary>
    /// Incr�mente le nombre d'interraction effectu�es avec le camp
    /// </summary>
    /// <param name="value">Valeur de l'interraction</param>
    public void AddInterractions(int value)
    {
        _interractions += value;
        if(_interractions >= _interractionsMin)
        {
            _tent.OpenTent();
        }
    }

    /// <summary>
    /// Lance le niveau en question
    /// </summary>
    /// <param name="level">nom de la sc�ne charg�e ensuite</param>
    public void FinishLevel(string level)
    {
        // Screenshot
        // Transitions
        SceneManager.LoadScene(level);
    }
}