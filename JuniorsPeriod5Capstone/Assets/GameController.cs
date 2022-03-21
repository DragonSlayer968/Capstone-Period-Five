using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Load();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Save()
    {
        PlayerAbilities player = FindObjectOfType<PlayerAbilities>();

        PlayerPrefs.SetInt("Mainpath", player.mainPath);
        PlayerPrefs.SetInt("Subpath", player.subPath);
        PlayerPrefs.SetInt("Subpathlevel", player.subPathLevel);
     
        PlayerPrefs.SetInt("HasRoll", 1);
    }

    public void Load()
    {
        PlayerAbilities player = FindObjectOfType<PlayerAbilities>();
        player.mainPath = PlayerPrefs.GetInt("Mainpath");
        player.subPath = PlayerPrefs.GetInt("Subpath");
        player.subPathLevel = PlayerPrefs.GetInt("Subpathlevel");
    }

}
