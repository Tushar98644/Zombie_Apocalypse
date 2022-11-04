using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ALLMenus : MonoBehaviour
{
    [Header("All Menu's")]
    public GameObject PauseMenu;
    public GameObject RestartMenu;
    public GameObject ObjectiveMenu;

    public static bool Gameisstopped = false;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(Gameisstopped)
            {
                Resume();
                Cursor.lockState = CursorLockMode.Locked;
            }

            else
            {
                Pause();
                Cursor.lockState = CursorLockMode.None;
            }

        }

        else if(Input.GetKeyDown("m"))
        {
            if (Gameisstopped)
            {
                Removeobjective();
                Cursor.lockState = CursorLockMode.Locked;
            }

            else
            {
                showobjective();
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    public void showobjective()
    {
        ObjectiveMenu.SetActive(true);
        Time.timeScale = 0f;
        Gameisstopped = true;
    }

    public void Removeobjective()
    {
        ObjectiveMenu.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Gameisstopped = false;
    }

    public void Qutigame()
    {
        Debug.Log("Quiting game....");
        Application.Quit();
    }


    public void Resume()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Gameisstopped =false;
    }


    public void Restart()
    {
        SceneManager.LoadScene("MainMenu");

    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Pause()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        Gameisstopped = true;
    }
}
