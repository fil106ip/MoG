using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenu : MonoBehaviour
{

    public GameObject MainMenuObject;
    public GameObject OptionMenu; 

    public void Update()
       
    {
        if (OptionMenu.active)
        {
            CursorOn();
        }

        if (MainMenuObject.active)
        {
            CursorOn();
        }
    }
    
    public void PlayGame ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
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
