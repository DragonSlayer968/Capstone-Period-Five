using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSummoner : MonoBehaviour
{
    public GameObject boss;
    public GameObject obj;
    public GameObject scenetran;

    public bool objActivate;
    public bool objDeactivate;

    public Animator anim;
    public GameObject playerCamera;
    public GameObject camera;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(boss == null)
        {
            scenetran.SetActive(true);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            anim.SetTrigger("Start");
        }
    }

    public void StopPlayer()
    {
        playerCamera.SetActive(false);
        FindObjectOfType<PlayerMovement>().canMove = false;
        FindObjectOfType<PlayerMovement>().jtNotDOne = true;
        FindObjectOfType<PlayerAttack>().AttackNotObtained = true;
        FindObjectOfType<PlayerAttack>().SlashTutorialNotFinished = true;
        FindObjectOfType<PlayerAbilities>().TutorialCoinNotFound = true;
        if (camera)
        {
            camera.SetActive(true);
        }
    }
    public void EndScene()
    {
        
        playerCamera.SetActive(true);
        FindObjectOfType<PlayerMovement>().canMove = true;
        FindObjectOfType<PlayerMovement>().jtNotDOne = false;
        FindObjectOfType<PlayerAttack>().AttackNotObtained = false;
        FindObjectOfType<PlayerAttack>().SlashTutorialNotFinished = false;
        FindObjectOfType<PlayerAbilities>().TutorialCoinNotFound = false;
        boss.SetActive(true);
       
    }

    

}
