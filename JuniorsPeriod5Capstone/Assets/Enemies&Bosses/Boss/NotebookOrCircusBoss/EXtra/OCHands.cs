using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OCHands : MonoBehaviour
{
    public GameObject body;

    public void DestroySelf()
    {
        Destroy(body);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<FillerHealth>().Hit();
        }
    } 


}
