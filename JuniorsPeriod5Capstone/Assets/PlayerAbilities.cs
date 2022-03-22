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
    void Start()
    {
        playerCoin = PlayerPrefs.GetInt("coins");
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = ": " + playerCoin;
        PlayerPrefs.SetInt("coins", playerCoin);
    }
}
