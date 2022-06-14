using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MB_MainMenuController : MonoBehaviour
{
    [SerializeField, Tooltip("Nom de la Scene à charger en cas de Play")]
    private string levelToLoad = "Level_1";

    public void ButtonPlay()
    {
        FindObjectOfType<CanvasGroup>().GetComponent<Animator>().SetTrigger("Start");
        Invoke(nameof(LoadNewScene), 1.2f);
    }

    private void LoadNewScene()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void ButtonConfirmQuit()
    {
        Application.Quit();
        Debug.Log("Application quittée");
    }
}