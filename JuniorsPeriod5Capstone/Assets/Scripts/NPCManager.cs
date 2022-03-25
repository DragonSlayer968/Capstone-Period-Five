using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCManager : MonoBehaviour
{
    public string npcString;
    public Text npcText;
    public GameObject eObj;
    public GameObject textBackground;

    public bool InRange;
    public bool Fountain;
    public Teleport tele;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(InRange == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                eObj.SetActive(false);
                npcText.text = npcString;
                textBackground.SetActive(true);
                if(Fountain == true)
                {
                    tele.teleport();
                }
            }
        }   
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            eObj.SetActive(true);
            InRange = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        InRange = false;
        if (other.gameObject.tag == "Player")
        {
            eObj.SetActive(false);
            npcText.text = "";
            textBackground.SetActive(false);
        }
    }
}
