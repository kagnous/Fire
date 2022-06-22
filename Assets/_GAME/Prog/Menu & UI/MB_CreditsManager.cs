using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MB_CreditsManager : MonoBehaviour
{
    Animator anim;

    [SerializeField]
    private GameObject BackButton;

    void Start()
    {
        anim = FindObjectOfType<CanvasGroup>().GetComponent<Animator>();

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(BackButton);
    }

    public void Quit()
    {
        anim.SetTrigger("Start");
        SceneManager.LoadScene("Menu 3D");
    }
}