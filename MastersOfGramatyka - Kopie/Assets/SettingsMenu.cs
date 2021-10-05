using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{

    public AudioMixer audioMixer;

   //Lautstärke im Options Menü
   public void SetVolume (float volume)
    {
        audioMixer.SetFloat("volume", volume); 
    }

    //Fullscreen im Options Menü
    public void SetFullScreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

}
