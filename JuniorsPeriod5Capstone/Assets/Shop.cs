using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    // Start is called before the first frame update
    public int ShopPage;
    public GameObject[] ShopPages;

    [Header("UI")]
    public Text levelName;    

    public Image BaseImage;
    public Text BaseText;

    public Image Page2ShieldImage;
    public Image Page2SwordImage;
    public Image Page2BootsImage;

    public Image Subpath1Image;
    public Image Subpath2Image;

    [Header("Shield")]
    public Sprite Page2Shield; public string Page2ShieldName;
    public Sprite[] Page3Shield; public string[] Page3ShieldName;
    public Sprite[] UpgradeSubPath1; public string[] UpgradeSubPath1Name;
    public Sprite[] UpgradeSubPath2; public string[] UpgradeSubPath2Name;

    [Header("Sword")]
    public Sprite Page2Sword; public string Page2SwordName;
    public Sprite[] Page3Sword; public string[] Page3SwordName;
    public Sprite[] SwordUpgradeSubPath1; public string[] SwordUpgradeSubPath1Name;
    public Sprite[] SwordUpgradeSubPath2; public string[] SwordUpgradeSubPath2Name;

    [Header("Boots")]
    public Sprite Page2Boots; public string Page2BootsName;
    public Sprite[] Page3Boots; public string[] Page3BootsName;
    public Sprite[] BootsUpgradeSubPath1; public string[] BootsdUpgradeSubPath1Name;
    public Sprite[] BootsUpgradeSubPath2; public string[] BootsUpgradeSubPath2Name;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ShopPage == 2)
        {

        }
    }
}
