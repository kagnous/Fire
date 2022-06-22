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

    [SerializeField]
    private string _nextLevel = "Menu 3D";

    [SerializeField, Tooltip("Nombre d'interractions requises pour terminer le niveau")]
    private int _interractionsMin = 1;

    /// <summary>
    /// Nombre d'interactions entre le joueur et le niveau
    /// </summary>
    private int _interractions; public int Interractions { get { return _interractions; } set { _interractions = value; } }

    //Events
    public delegate void IncreaseFireDelegate(int fireSize);
    public event IncreaseFireDelegate eventFire;

    void Start()
    {
        Cursor.visible = false;
        _player = FindObjectOfType<MB_PlayerController>().gameObject;
        _tent = FindObjectOfType<MB_TentController>();
    }

    /// <summary>
    /// Incrémente le nombre d'interraction effectuées avec le camp
    /// </summary>
    /// <param name="value">Valeur de l'interraction</param>
    public void AddInterractions(int value)
    {
        _interractions += value;

        eventFire?.Invoke(_interractions);

        if(_interractions >= _interractionsMin)
        {
            _tent.OpenTent();
        }
    }

    /// <summary>
    /// Lance le niveau en question
    /// </summary>
    /// <param name="level">nom de la scène chargée ensuite</param>
    public void FinishLevel()
    {
        // Screenshot
        FindObjectOfType<CanvasGroup>().GetComponent<Animator>().SetTrigger("Start");
        Invoke(nameof(LoadNewScene), 1.5f);
        //SceneManager.LoadScene(level);
    }

    private void LoadNewScene()
    {
        SceneManager.LoadScene(_nextLevel);
    }
}