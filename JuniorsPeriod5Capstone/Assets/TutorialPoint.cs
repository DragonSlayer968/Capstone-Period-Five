using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPoint : MonoBehaviour
{
    // Start is called before the first frame update

    public float playerDist, distToTut;
    public GameObject player;

    public string tutorialText;
    public Text Tutorial;

    public bool Hardens;
    public GameObject Barrier;

    public int TutorialValue;

   // public float Timetilnexttext;


    void Start()
    {
        player = GameObject.FindWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {

        if (TutorialValue == PlayerPrefs.GetInt("TV"))
        {
            player.GetComponent<PlayerMovement>().jtNotDOne = false;


            player.GetComponent<PlayerAttack>().AttackNotObtained = false;


            player.GetComponent<PlayerAttack>().SlashTutorialNotFinished = false;


            player.GetComponent<PlayerHealth>().blockNotObtained = false;


            player.GetComponent<PlayerAbilities>().TutorialCoinNotFound = false;



            player.GetComponent<PlayerHealth>().Invulnerable = false;
        }

        playerDist = Mathf.Abs(transform.position.x - player.transform.position.x);

        if(playerDist <= distToTut)
        {
            Tutorial.text = tutorialText;
            if(Hardens == true)
            {
                Barrier.SetActive(true); //I now realise this is broken... whatever
            }
            if(TutorialValue != PlayerPrefs.GetInt("TV"))
            {
                if (TutorialValue == 3)
                {
                    player.GetComponent<PlayerMovement>().jtNotDOne = false;
                }
                if (TutorialValue == 7)
                {
                    player.GetComponent<PlayerAttack>().AttackNotObtained = false;
                }
                if (TutorialValue == 9)
                {
                    player.GetComponent<PlayerAttack>().SlashTutorialNotFinished = false;
                }
                if (TutorialValue == 11)
                {
                    player.GetComponent<PlayerHealth>().blockNotObtained = false;
                }
                if (TutorialValue == 13)
                {
                    player.GetComponent<PlayerAbilities>().TutorialCoinNotFound = false;
                }

                if (TutorialValue == 14)
                {
                    player.GetComponent<PlayerHealth>().Invulnerable = false;
                    PlayerPrefs.SetInt("TV", TutorialValue);
                }
            }
           

            
            Destroy(gameObject);
        }
    }
}
