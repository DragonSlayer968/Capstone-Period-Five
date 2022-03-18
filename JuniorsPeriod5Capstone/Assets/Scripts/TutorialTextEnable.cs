using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TutorialTextEnable : MonoBehaviour
{
    public Text tutorialText;


    public void Start()
    {
        tutorialText.enabled = false;
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            // if in range of player, activates the text
            tutorialText.enabled = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //if out of range of the player, deactivates the test.
            tutorialText.enabled = false;
        }
    }
}
