using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MB_MainMenuController : MonoBehaviour
{
    [SerializeField, Tooltip("Page de menu principal")]
    private GameObject mainWindow;
    [SerializeField, Tooltip("Page de menu des paramètres")]
    private GameObject settingsWindow;
    [SerializeField, Tooltip("Page de confirmation de Quit")]
    private GameObject closeWindow;
    [SerializeField, Tooltip("Page de selection des niveaux")]
    private GameObject levelWindow;

    [Header("First selected")]
    [SerializeField, Tooltip("Boutton selectionné à l'apparition de la scène")]
    private GameObject loadFirstButton;
    [SerializeField, Tooltip("Boutton selectionné dans les levels")]
    private GameObject levelFirstButton;
    [SerializeField, Tooltip("Boutton selectionné à l'apparition des options")]
    private GameObject optionFirstButton;
    [SerializeField, Tooltip("Boutton selectionné en quittant les options")]
    private GameObject optionCloseButton;
    [SerializeField, Tooltip("Boutton selectionné à l'apparition du quit")]
    private GameObject quitFirstButton;
    [SerializeField, Tooltip("Boutton selectionné en quittant le quit")]
    private GameObject quitCloseButton;

    private void Awake()
    {
        Cursor.visible = false;
    }

    public void ButtonSelectLevel()
    {
        mainWindow.SetActive(false);
        levelWindow.SetActive(true);

        // Place le controller sur le premier boutton option
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(levelFirstButton);
    }

    public void ButtonBackLevel()
    {
        mainWindow.SetActive(true);
        levelWindow.SetActive(false);

        // Place le controller sur le premier boutton option
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(loadFirstButton);
    }

    public void ButtonPlay(string level)
    {
        StartCoroutine(LoadLevelCoroutine(level));
    }

    public void ButtonSettings()
    {
        mainWindow.SetActive(false);
        settingsWindow.SetActive(true);

        // Place le controller sur le premier boutton option
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionFirstButton);
    }

    public void ButtonQuit()
    {
        mainWindow.SetActive(false);
        closeWindow.SetActive(true);

        // Place le controller sur le premier boutton quit
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(quitFirstButton);
    }

    public void ButtonConfirmQuit()
    {
        Application.Quit();
        Debug.Log("Application quittée");
    }

    public void ButtonCancelQuit()
    {
        closeWindow.SetActive(false);
        mainWindow.SetActive(true);

        // Place le controller sur le premier boutton
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(quitCloseButton);
    }

    public void ButtonCloseSettings()
    {
        settingsWindow.SetActive(false);
        mainWindow.SetActive(true);

        // Place le controller sur le bon boutton MainMenu
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionCloseButton);
    }

    private IEnumerator LoadLevelCoroutine(string level)
    {
        FindObjectOfType<CanvasGroup>().GetComponent<Animator>().SetTrigger("Start");
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene(level);
    }
}