using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool isPaused;

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            EscapePressed();
        }
    }
    public void Resume()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        isPaused = false;
    }
    public void Pause()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        isPaused = true;
    }
    private void EscapePressed()
    {
        if (!isPaused)
        {
            Pause();
        }
        else
        {
            Resume();
        }
    }
    public void Menu()
    {
        Resume();
        LevelLoader.i.LoadMenu();
    }
    public void Quit()
    {
        Application.Quit();
    }
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            if (!isPaused)
            {
                Pause();
            }
        }
    }

}
