using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText, dialogueText1;

    //this keeps track of all dialogue scentences
    public Queue<string> scentences;

    // Start is called before the first frame update
    void Start()
    {
        scentences = new Queue<string>(); //initializing scentences var
    }

    //testing to see if dialogue can be called
    public void StartDialogue(Dialogue dialogue)
    {
        dialogueText.gameObject.SetActive(true);
        dialogueText1.gameObject.SetActive(true);
        Debug.Log("Starting conversation with " + dialogue.name);

        //nameText.text = dialogue.name;

        scentences.Clear(); //clearns previous convo scentences

        foreach (string scentence in dialogue.scentences)
        {
            scentences.Enqueue(scentence);//queues up a new scentence
        }

        DisplayNextScentence();
    }

    public void DisplayNextScentence()
    {
        //if there are no more scentences in queue
        if (scentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string scentence = scentences.Dequeue();
        dialogueText.text = scentence;
        dialogueText1.text = scentence;
    }

    //what happens when convo ends
    public void EndDialogue()
    {
        //placeholder
        Debug.Log("End of dialogue");
        dialogueText.gameObject.SetActive(false);
        dialogueText1.gameObject.SetActive(false);
    }
}
