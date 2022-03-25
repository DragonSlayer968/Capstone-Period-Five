using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject TeleportPoint;
    public GameObject playerCamera;
    public GameObject otherCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.transform.position = TeleportPoint.transform.position;
            playerCamera.SetActive(true);
            otherCamera.SetActive(false);
        }
    }

    public void teleport()
    {
        FindObjectOfType<PlayerAbilities>().gameObject.transform.position = TeleportPoint.transform.position;
        playerCamera.SetActive(false);
        otherCamera.SetActive(true);
    }

}
