using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void PlayTutorial ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }
    public void PlayFirstLevel ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }
    public void QuitGame ()
    {
        Debug.Log("The Quit Button has been pressed");
        Application.Quit();
    }
    public void PlayCredits ()
    {
        SceneManager.LoadScene("Credits");
    }
    public void PlayStartMenu ()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
