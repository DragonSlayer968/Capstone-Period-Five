using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
//class of npc dialogue sets, can be used to make multiple npcs
public class Dialogue
{
    public string name; //npc's name
    //attribute makes the text area in inspector bigger, min 3 lines max 10 lines
    [TextArea(3, 10)]
    public string[] scentences; //what the npc says
}
