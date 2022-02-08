using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//known issue: dialogue ends too quickly. Dunno the solution for it
public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue; //calling the Dialogue script, now inspector will show places for npc name and stuff

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue); //once triggered, calls DiaManage script and starts dialogue
    }

    //once another object enters this collider
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")//ensures its just player that will cause text stuff to happen
        {
            TriggerDialogue(); //TriggerDialogue function will play out
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        FinishDialogue(); //once the player leaves the collider, dialogue ends
    }

    public void FinishDialogue()
    {
        FindObjectOfType<DialogueManager>().EndDialogue(); //see above, calls function in other script
    }
}
