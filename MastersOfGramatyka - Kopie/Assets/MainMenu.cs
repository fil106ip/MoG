using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{

    public GameObject MainMenuObject, OptionMenu, menuFirstButton, menuoptFirstButton;

    public void Update()     
    {
        if (OptionMenu.activeSelf)
        {
            CursorOn();
        }

        if (MainMenuObject.activeSelf)
        {
            CursorOn();
        }

    }

    public void OpenOptions()
    {
        OptionMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(menuoptFirstButton);
    }

    public void SpielLaden()
    {
        print("Spielständen werden geladen...");
    }
    public void CloseOptionsOpenMenu()
    {
        OptionMenu.SetActive(false);
        MainMenuObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(menuFirstButton);
    }
    public void PlayGame ()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex -1);
        Time.timeScale = 1f;
    }

    public void QuitGame ()
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
