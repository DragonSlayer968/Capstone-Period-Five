using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FinalBossCutscene : MonoBehaviour
{
    public int TextValue;
    public int part;
    public string[] stuffToSay, afterstuff;
    public string[] stuff;
    public GameObject boss;
    public bool start;
    public Text text;
    public GameObject TextObject;
    public Animator anim;
    public GameObject Camera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (boss && start == true)
        {
            if (boss.GetComponent<EnemyHealth>().enemyHealth <= boss.GetComponent<EnemyHealth>().maxHealth / 2)
            {
                part = 1;
                anim.SetTrigger("Flee");
                Destroy(boss);
            }
        }
       

        if (part == 0)
        {
            stuff = stuffToSay;
        }
        else
        {
            stuff = afterstuff;
        }

        text.text = stuff[TextValue];

    }

    public void NextSpeech()
    {
        if(part == 0)
        {
            if (TextValue < stuff.Length - 1)
            {
                TextValue++;
            }

            else
            {
                FindObjectOfType<BossSummoner>().EndScene();
                TextObject.SetActive(false);
                TextValue = 0;
                start = true;
                Camera.SetActive(false);
            }
        }

        else
        {
            if (TextValue < stuff.Length - 1)
            {
                TextValue++;
            }

            else
            {
                anim.SetTrigger("Run");
                
            }
        }
       
    }

    public void LoadNectScene()
    {
        FindObjectOfType<SceneTransition>().LoadNewScene();
    }

}
