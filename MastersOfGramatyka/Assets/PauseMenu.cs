using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject pauseMenuObject;
    public GameObject OptionMenu;

    void Update()
    {

        //damit man im options menü nich das pause menü öffnen kann 
        if (!OptionMenu.active)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CursorOn();

                if (GameIsPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }   

        //Maus wird angezeigt im OptionsMenü und PauseMenü
        if (pauseMenuObject.active)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        if (OptionMenu.active)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false; 
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true; 
    }

    public void QuitGame()
    {
        print("QUIT!");
        Application.Quit();
    }

    public void CursorOn()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

}
