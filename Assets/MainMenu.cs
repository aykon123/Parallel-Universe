using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject MainMenuUI;
    public GameObject OptionsMenuUI;

    void Awake()
    {
        OptionsMenuUI.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(4);
    }

    public void QuitGame()
    {
        UnityEngine.Debug.Log("Quit Game");
        UnityEngine.Application.Quit();
    }

    public void OptionsMenu()
    {
        OptionsMenuUI.SetActive(true);
        MainMenuUI.SetActive(false);
    }
}
