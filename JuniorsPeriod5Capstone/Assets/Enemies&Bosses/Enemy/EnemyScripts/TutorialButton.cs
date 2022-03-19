using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialButton : MonoBehaviour
{
    public Animator RisingInk;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerProj")
        {
            GetComponent<Animator>().SetTrigger("Pressed");
        }
    }


    public void DestroySelf()
    {
        RisingInk.SetTrigger("InkRise");
        Destroy(gameObject);
    }

}
