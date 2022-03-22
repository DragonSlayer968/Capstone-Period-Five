using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public GameObject menu;
    public GameObject returnToLevelSelect, Quit;

    public float masterVolume;
    public float sfxVolume;
    public float musicVolume;

    public Slider masterVolumeSlide;
    public Slider sfxVolumeSlider;
    public Slider musicVolumeSlider;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        DontDestroyOnLoad(gameObject);

        masterVolume = masterVolumeSlide.value;
        sfxVolume = sfxVolumeSlider.value;
        musicVolume = musicVolumeSlider.value;

        if (SceneManager.GetActiveScene().name != "LevelSelect")
        {
            returnToLevelSelect.SetActive(true);
            Quit.SetActive(false);
        }
        else
        {
            returnToLevelSelect.SetActive(false);
            Quit.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenMenu();
        }

    }

    public void OpenMenu()
    {
        menu.SetActive(true);
        Time.timeScale = 0;
    }

    public void CloseMenu()
    {
        menu.SetActive(false);
        Time.timeScale = 1;
    }


    public void SavePressed()
    {
        if (SceneManager.GetActiveScene().name != "LevelSelect")
        {
            FindObjectOfType<GameController>().Save();
        }
        else
        {
            PlayerPrefs.SetInt("LevelsUnlocked", FindObjectOfType<LevelSelect>().levelsUnlocked);
        }

    }

    public void MainMenuPressed()
    {
        PlayerPrefs.SetInt("Cp", 0);

        SceneManager.LoadScene("LevelSelect");
        CloseMenu();
    }

    public void QuitPressed()
    {
        Application.Quit();
    }

    public GameObject soundmenu;
    public void OpenSoundMenu()
    {
        soundmenu.SetActive(true);
    }

    public void CloseSoundMenu()
    {
        soundmenu.SetActive(false);
    }
}
