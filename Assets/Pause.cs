using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Net.Mime.MediaTypeNames;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f; // Set the time scale back to normal to resume the game
        pauseMenuUI.SetActive(false); // Hide the pause menu UI
        isPaused = false;
    }

    void Pause()
    {
        Time.timeScale = 0f; // Set the time scale to 0 to pause the game
        pauseMenuUI.SetActive(true); // Show the pause menu UI
        isPaused = true;
    }
/*
    public void Restart()
    {
        // Implement code to restart the level or scene
    }

    public void OpenOptions()
    {
        // Implement code to open the options menu
    }

    public void QuitGame()
    {
        // Implement code to quit the game (for standalone builds)
        Application.Quit();
    }
*/
}
