using UnityEngine.SceneManagement;
using UnityEngine;

public class MB_EndingScene : MonoBehaviour
{
    public void Menu()
    {
        SceneManager.LoadScene("Menu3D");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
