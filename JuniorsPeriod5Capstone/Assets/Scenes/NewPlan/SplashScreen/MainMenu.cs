using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public Image MMImage;
    public Sprite[] playerSprites;
    public int currentSprite;

    public bool Play, Credits, Quit;
    public Animator SelectionAnimator;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MMIdle()
    {
        int randomSprite = Random.Range(0, playerSprites.Length);
        if(currentSprite == randomSprite)
        {
            randomSprite = Random.Range(0, playerSprites.Length);
        }

        MMImage.sprite = playerSprites[randomSprite];
        currentSprite = randomSprite;
    }

    public void PlaySelected()
    {
        if(Credits == false && Quit == false)
        {
            Play = true;
        }
        SelectionAnimator.SetTrigger("Selection");
    }

    public void QuitSelected()
    {
        if (Credits == false && Play == false)
        {
            Quit = true;
        }
        SelectionAnimator.SetTrigger("Selection");
    }

    public void CreditsSelected()
    {
        if (Play == false && Quit == false)
        {
            Credits = true;
        }
        SelectionAnimator.SetTrigger("Selection");
    }

    public void SelectedAction()
    {
        if(Quit == true)
        {
            Application.Quit();
        }

        if(Play == true)
        {
            print("SceneManager.LoadScene(WhateverLevelSelectSceneNameIs)");
        }

        if(Credits == true)
        {
            print("SceneManager.LoadScene(WhateverCreditsSceneNameIs)");
        }

    }

}
