// References
// https://www.youtube.com/watch?v=zc8ac_qUXQY&t=419s - Brackeys - START MENU in Unity

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    public AudioSource hoverSFX;
    public AudioSource clickSFX;
    public void PlayGame()
    {
        SceneManager.LoadScene("josh's level 1");
    }

    public void Menu()
    {
        SceneManager.LoadScene("GameMenu");
    }

    public void playHoverSound()
    {
        hoverSFX.Play();
    }

    public void playClickSound()
    {
        clickSFX.Play();
    }
}