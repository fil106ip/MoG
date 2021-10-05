using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool GameIsPaused = false;

    public GameObject pauseMenuUI, pauseMenuObject, OptionMenu, pauseFirstButton, optionsFirstButton, optionsClosedButton;
    public ThirdPersonMovement tpm;


    void Update()
    {

        //damit man im options menü nich das pause menü öffnen kann 
        if (!OptionMenu.activeSelf)
        {

            if (Input.GetButtonDown("Menu"))
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
        if (pauseMenuObject.activeSelf)
        {
            CursorOn();
        }

        if (OptionMenu.activeSelf)
        {
            CursorOn();
        }   

    }

    public void OpenMenu()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false; 
    }   

    public void ClosedOptions()
    {
        OptionMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsClosedButton);
    }

    public void OpenOptions()
    {
        OptionMenu.SetActive(true);
        //clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(optionsFirstButton);
    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(pauseFirstButton);

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
