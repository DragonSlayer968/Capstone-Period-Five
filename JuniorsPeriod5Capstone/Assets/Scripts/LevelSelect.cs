using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
  
    public void LoadSelectedLevel(string selectedLevel)//Parameter allows the creater to set what scene they want to go to
    {
        SceneManager.LoadScene(selectedLevel);//Loads selected Level
    }
}
