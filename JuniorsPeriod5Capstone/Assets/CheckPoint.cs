using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public int checkValue;
    public bool DeathPoint;

    // Start is called before the first frame update
    void Start()
    {
        if(FindObjectOfType<PlayerHealth>().checkPointValue == checkValue)
        {
            GetComponent<Animator>().SetTrigger("Checked");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && PlayerPrefs.GetInt("Cp") != checkValue)
        {
            if(DeathPoint == true)
            {
                collision.GetComponent<PlayerHealth>().health = 0;
            }
            GetComponent<Animator>().SetTrigger("Check");
            collision.GetComponent<PlayerHealth>().checkPointValue = checkValue;
           
        }
    }

}
