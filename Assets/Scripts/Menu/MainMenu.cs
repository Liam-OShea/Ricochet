// References
// https://www.youtube.com/watch?v=zc8ac_qUXQY&t=419s - Brackeys - START MENU in Unity

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource hoverSFX;
    public AudioSource clickSFX;
    public void PlayGame(){
        SceneManager.LoadScene(1);
    }

    public void QuitGame(){
        Debug.Log("QUIT");
        Application.Quit();
    }

    public void PlayLevel2(){
        SceneManager.LoadScene(2);
    }

    public void PlayLevel3()
    {
        SceneManager.LoadScene(3);
    }

    public void PlayEscapeLevel(){
        SceneManager.LoadScene(4);
    }

    public void playHoverSound(){
        hoverSFX.Play();
    }

    public void playClickSound(){
        clickSFX.Play();
    }
}
