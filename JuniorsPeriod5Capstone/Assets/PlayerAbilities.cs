using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerAbilities : MonoBehaviour
{
    // Start is called before the first frame update
    public bool AbilityTreeUnlocked;
    public int mainPath; 
    public int subPath; //1 - first subpath, 2 - second subpath
    public int subPathLevel; //Max 3

    public int playerCoin;
    public Text coinText;
    public GameObject coinImage;
    public bool TutorialCoinNotFound;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(TutorialCoinNotFound == false)
        {
            coinText.text = ": " + playerCoin;
            coinImage.SetActive(true);
        }

        else
        {
            coinImage.SetActive(false);
        }

    }
}
