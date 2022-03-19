using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public Sprite[] Icons;
    public string[] levelName;
    public string[] levelToLoad;
    public int LevelValue;
    public Animator levelSelectAnimation;

    public Text title;
    public Image Icon;

    public bool onAuthor = true;
    

    public void Close()
    {
        if(LevelValue <= levelName.Length - 1)
        {
            levelSelectAnimation.SetTrigger("CloseFromFront");
        }

        else
        {
            levelSelectAnimation.SetTrigger("CloseFromBack");
        }

        LevelValue = 0;
        onAuthor = true;
    }

    public void FlipPage()
    {
        if(LevelValue < levelName.Length - 1 && onAuthor == false)
        {
            LevelValue++;
           
        }

        else
        {         
            if(LevelValue == levelName.Length - 1)
            {
                LevelValue = levelName.Length;
            }

            if(onAuthor == true)
            {
               
                onAuthor = false;
            }

        }
    }

    public void FlipPageBack()
    {
        if (LevelValue > 0)
        {
            LevelValue--;
           
        }

        else
        {
            if (LevelValue == 0)
            {
               
                onAuthor = true;
            }
        }
    }

    public void NextPage()
    {
        if (LevelValue < levelName.Length - 1 && onAuthor == false)
        {
            
            levelSelectAnimation.SetTrigger("PageFlip");
        }

        else
        {
            if (LevelValue >= levelName.Length - 1)
            {
                levelSelectAnimation.SetTrigger("Final");
            }

            else if (onAuthor == true)
            {
                levelSelectAnimation.SetTrigger("Play");
                
            }

        }
    }

    public void PreviousPage()
    {
        if (LevelValue > 0)
        {
           
            levelSelectAnimation.SetTrigger("PageFlipBack");
        }

        else
        {
            if (LevelValue == 0)
            {
                levelSelectAnimation.SetTrigger("BackToAuthor");
                
            }
        }
    }

    public void SwitchAppearance()
    {
        Icon.sprite = Icons[LevelValue];
        title.text = levelName[LevelValue];
        
    }

    public void StartLevelSelect()
    {
        levelSelectAnimation.SetTrigger("Author");
    }


    public void LoadSelectedLevel(string selectedLevel)//Parameter allows the creater to set what scene they want to go to
    {
        selectedLevel = levelToLoad[LevelValue];
        SceneManager.LoadScene(selectedLevel);//Loads selected Level
    }

    
}
