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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            eObj.SetActive(true);
            if (Input.GetKey(KeyCode.E))
            {
                eObj.SetActive(false);
                npcText.text = npcString;
                textBackground.SetActive(true);
            }
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            eObj.SetActive(false);
            npcText.text = "";
            textBackground.SetActive(false);
        }
    }
}
