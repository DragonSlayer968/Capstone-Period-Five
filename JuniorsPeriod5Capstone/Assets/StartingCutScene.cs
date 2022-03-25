using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartingCutScene : MonoBehaviour
{
    public int part;
    public int TextValue;
    public string[] part1, part2, part3, part4;
    public string[] currentPart;

    public Animator anim;
    public Text dialogue;


    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("TV") != 0)
        {
            Destroy(gameObject);
            AfterCutScene.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if(part == 0)
        {
            currentPart = part1;
        }

        if (part == 1)
        {
            currentPart = part2;
        }

        if (part == 2)
        {
            currentPart = part3;
        }

        if (part == 3)
        {
            currentPart = part4;
        }
        dialogue.text = currentPart[TextValue];
    }

    public void NextText()
    {
        if(TextValue < currentPart.Length - 1)
        {
            TextValue++;
        }
        else
        {
            anim.SetTrigger("Next");
            TextValue = 0;
        }

    }

    public void NewPart()
    {
        part++;
    }

    public GameObject AfterCutScene;
    public void EndScene()
    {
        AfterCutScene.SetActive(true);
        Destroy(gameObject);
        PlayerPrefs.SetInt("TV", 0);
    }



}
