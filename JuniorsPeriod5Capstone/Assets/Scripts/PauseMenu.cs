using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenuCanvas;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenuCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // if pause menu isnt active in hierarchy
            if (!pauseMenuCanvas.activeInHierarchy)
            {
                Pause(); // pause the menu
            }
            else // if the menu is active in the hieracrhy
            {
                Resume(); // resume the menu
            }
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pauseMenuCanvas.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseMenuCanvas.SetActive(false);
    }

    public void Menu()
    {
        Debug.Log("Go to Menu");
    }

}
