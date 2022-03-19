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
        playerDist = Mathf.Abs(transform.position.x - player.transform.position.x);

        if(playerDist <= distToTut)
        {
            Tutorial.text = tutorialText;
            if(Hardens == true)
            {
                Barrier.SetActive(true); //I now realise this is broken... whatever
            }

            Destroy(gameObject);
        }
    }
}
