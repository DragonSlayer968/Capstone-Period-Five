using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    // Start is called before the first frame update
    public int ShopPage;
    public GameObject[] ShopPages;
    public PlayerAbilities playerAbilities;
    public int PagesUnlocked;

    [Header("UI")]
    public Text levelName;    


    public Image Subpath1Image;
    public Image Subpath2Image;
    public Text Subpath1Text;
    public Text Subpath2Text;

    public Image UpgradeImage;
    public Text UpgradeText;

    public Text Page3ShopName;
    public Text Page4ShopName;

    [Header("Shield")]
   
    public Sprite[] Page3Shield; public string[] Page3ShieldName;
    public Sprite[] UpgradeSubPath1; public string[] UpgradeSubPath1Name;
    public Sprite[] UpgradeSubPath2; public string[] UpgradeSubPath2Name;

    [Header("Sword")]
   
    public Sprite[] Page3Sword; public string[] Page3SwordName;
    public Sprite[] SwordUpgradeSubPath1; public string[] SwordUpgradeSubPath1Name;
    public Sprite[] SwordUpgradeSubPath2; public string[] SwordUpgradeSubPath2Name;

    [Header("Boots")]
    
    public Sprite[] Page3Boots; public string[] Page3BootsName;
    public Sprite[] BootsUpgradeSubPath1; public string[] BootsUpgradeSubPath1Name;
    public Sprite[] BootsUpgradeSubPath2; public string[] BootsUpgradeSubPath2Name;

    [Header("Opening")]
    public GameObject player;
    public float talkDist;
    public GameObject talkObject;
    public float playerDist;

    public int test;


    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerAbilities = player.GetComponent<PlayerAbilities>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if(test < 7)
            {
                test++;
            }

            else
            {
                test = 0;
            }


            PlayerPrefs.SetInt("ShopLevel", test);
            print(PlayerPrefs.GetInt("ShopLevel"));
        }

        if(PlayerPrefs.GetInt("LevelsUnlocked") >= 3 && playerAbilities.mainPath == 0)
        {
            PagesUnlocked = 2;
        }

        else if(playerAbilities.mainPath > 0)
        {
            if(PlayerPrefs.GetInt("LevelsUnlocked") >= 4)
            {
                PagesUnlocked = 3;
            }

            if(playerAbilities.subPath > 0 && PlayerPrefs.GetInt("LevelsUnlocked") >= 6)
            {
                PagesUnlocked = 4;
            }

            if (playerAbilities.subPathLevel == 1 && PlayerPrefs.GetInt("LevelsUnlocked") >= 7)
            {
                PagesUnlocked = 5;
            }

            if (playerAbilities.subPathLevel == 2 && PlayerPrefs.GetInt("LevelsUnlocked") >= 8)
            {
                PagesUnlocked = 6;
            }

            
        }


        playerDist = Mathf.Abs(gameObject.transform.position.x - player.transform.position.x);
        if(playerDist < talkDist)
        {
            talkObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                OpenShop();
            }
        }

        else
        {
            talkObject.SetActive(false);
        }

        for(int i = 0; i < ShopPages.Length; i++)
        {
            if(ShopPage < 4)
            {
                if (i != ShopPage)
                {
                    ShopPages[i].SetActive(false);

                }

                else
                {
                    ShopPages[i].SetActive(true);
                }
            }

            else
            {
                ShopPages[3].SetActive(false);
                ShopPages[4].SetActive(true);
            }
           
        }

        IconUi();
        TextUi();
        OtherUI();
        Prices();
    }

    public int price;
    public void Prices()
    {
        if(ShopPage <= 1)
        {
            price = 0;
        }

        if(ShopPage == 2)
        {
            price = 150;
        }

        if(ShopPage == 3)
        {
            price = 200;
        }

        if(ShopPage == 4)
        {
            if (ShopPage == 4 && playerAbilities.subPathLevel == 0)
            {
                price = 225;
            }

            if (ShopPage == 5 && playerAbilities.subPathLevel == 1)
            {
                price = 250;
            }

            if (ShopPage == 6 && playerAbilities.subPathLevel == 2)
            {
                price = 275;
            }
        }

    }

    public GameObject ShopMenu;
    
    public void OpenShop()
    {
        ShopMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void CloseShop()
    {
        ShopMenu.SetActive(false);
        ShopPage = 1;
        Time.timeScale = 1;
    } 

    public void OtherUI()
    {
        levelName.text = "Level " + ShopPage;

        //PageNames
        if(playerAbilities.mainPath == 1)
        {
            Page3ShopName.text = "Choose Blade";
            Page4ShopName.text = "Upgrade Blade";
        }

        if (playerAbilities.mainPath == 2)
        {
            Page3ShopName.text = "Choose Boot Type";
            Page4ShopName.text = "Upgrade Boots";
        }

        if (playerAbilities.mainPath == 3)
        {
            Page3ShopName.text = "Choose Engraving";
            Page4ShopName.text = "Upgrade Engraving";
        }

    }

    public void IconUi()
    {
        if (ShopPage == 3)
        {

            if (playerAbilities.mainPath == 3)
            {
                Subpath1Image.sprite = Page3Shield[0];
                Subpath2Image.sprite = Page3Shield[1];
            }

            if (playerAbilities.mainPath == 1)
            {
                Subpath1Image.sprite = Page3Sword[0];
                Subpath2Image.sprite = Page3Sword[1];
            }

            if (playerAbilities.mainPath == 2)
            {
                Subpath1Image.sprite = Page3Boots[0];
                Subpath2Image.sprite = Page3Boots[1];
            }

        }

        if (ShopPage > 3)
        {
            if (playerAbilities.mainPath == 1)
            {
                if (playerAbilities.subPath == 1)
                {
                    UpgradeImage.sprite = SwordUpgradeSubPath1[ShopPage - 4];
                }

                else
                {
                    UpgradeImage.sprite = SwordUpgradeSubPath2[ShopPage - 4];
                }
            }

            if (playerAbilities.mainPath == 2)
            {
                if (playerAbilities.subPath == 1)
                {
                    UpgradeImage.sprite = BootsUpgradeSubPath1[ShopPage - 4];
                }

                else
                {
                    UpgradeImage.sprite = BootsUpgradeSubPath2[ShopPage - 4];
                }
            }

            if (playerAbilities.mainPath == 3)
            {
                if (playerAbilities.subPath == 1)
                {
                    UpgradeImage.sprite = UpgradeSubPath1[ShopPage - 4];
                }

                else
                {
                    UpgradeImage.sprite = UpgradeSubPath2[ShopPage - 4];
                }
            }

        }
    }

    public void TextUi()
    {
        if (ShopPage == 3)
        {

            if (playerAbilities.mainPath == 3)
            {
                Subpath1Text.text = Page3ShieldName[0];
                Subpath2Text.text = Page3ShieldName[1];
            }

            if (playerAbilities.mainPath == 1)
            {
                Subpath1Text.text = Page3SwordName[0];
                Subpath2Text.text = Page3SwordName[1];
            }

            if (playerAbilities.mainPath == 2)
            {
                Subpath1Text.text = Page3BootsName[0];
                Subpath2Text.text = Page3BootsName[1];
            }

        }

        if (ShopPage > 3)
        {
            if (playerAbilities.mainPath == 1)
            {
                if (playerAbilities.subPath == 1)
                {
                    UpgradeText.text = SwordUpgradeSubPath1Name[ShopPage - 4];
                }

                else
                {
                    UpgradeText.text = SwordUpgradeSubPath2Name[ShopPage - 4];
                }
            }

            if (playerAbilities.mainPath == 2)
            {
                if (playerAbilities.subPath == 1)
                {
                    UpgradeText.text = BootsUpgradeSubPath1Name[ShopPage - 4];
                }

                else
                {
                    UpgradeText.text = BootsUpgradeSubPath2Name[ShopPage - 4];
                }
            }

            if (playerAbilities.mainPath == 3)
            {
                if (playerAbilities.subPath == 1)
                {
                    UpgradeText.text = UpgradeSubPath1Name[ShopPage - 4];
                }

                else
                {
                    UpgradeText.text = UpgradeSubPath2Name[ShopPage - 4];
                }
            }

        }
    }

    public Text DescriptionTitle;
    public Text DescriptionDescribe;
    public string page1Title, page1Description;
    public string[] page2Title, page2Descriptions;
    public string[] swordpage3Title, sword3Description, shieldpage3Title, shielpage3Description, bootspage3Title, bootspage3Description;
    public string swordpage4Title, sword4Description, shieldpage4Title, shielpage4Description, bootspage4Title, bootspage4Description;
    public string swordpage5Title, sword5Description, shieldpage5Title, shielpage5Description, bootspage5Title, bootspage5Description;
    public string swordpage6Title, sword6Description, shieldpage6Title, shielpage6Description, bootspage6Title, bootspage6Description;

    public string path2swordpage4Title, path2sword4Description, path2shieldpage4Title, path2shielpage4Description, path2bootspage4Title, path2bootspage4Description;
    public string path2swordpage5Title, path2sword5Description, path2shieldpage5Title, path2shielpage5Description, path2bootspage5Title, path2bootspage5Description;
    public string path2swordpage6Title, path2sword6Description, path2shieldpage6Title, path2shielpage6Description, path2bootspage6Title, path2bootspage6Description;

    public GameObject descriptionMenu;

    public void Description(int what)
    {
        descriptionMenu.SetActive(true);
        if(ShopPage <= 1)
        {
            DescriptionTitle.text = page1Title;
            DescriptionDescribe.text = page1Description;
        }

        if(ShopPage == 2)
        {
            DescriptionTitle.text = page2Title[what];
            DescriptionDescribe.text = page2Descriptions[what];
        }

        if(ShopPage == 3)
        {
            if(playerAbilities.mainPath == 1)
            {
                DescriptionTitle.text = swordpage3Title[what];
                DescriptionDescribe.text = sword3Description[what];
            }

            if(playerAbilities.mainPath == 3)
            {
                DescriptionTitle.text = shieldpage3Title[what];
                DescriptionDescribe.text = shielpage3Description[what];
            }

            if(playerAbilities.mainPath == 2)
            {
                DescriptionTitle.text = bootspage3Title[what];
                DescriptionDescribe.text = bootspage3Description[what];
            }
           
        }

        if(playerAbilities.subPath == 1)
        {
            if (ShopPage == 4)
            {
                if (playerAbilities.mainPath == 1)
                {
                    DescriptionTitle.text = swordpage4Title;
                    DescriptionDescribe.text = sword4Description;
                }

                if (playerAbilities.mainPath == 3)
                {
                    DescriptionTitle.text = shieldpage4Title;
                    DescriptionDescribe.text = shielpage4Description;
                }

                if (playerAbilities.mainPath == 2)
                {
                    DescriptionTitle.text = bootspage4Title;
                    DescriptionDescribe.text = bootspage4Description;
                }
            }

            if (ShopPage == 5)
            {
                if (playerAbilities.mainPath == 1)
                {
                    DescriptionTitle.text = swordpage5Title;
                    DescriptionDescribe.text = sword5Description;
                }

                if (playerAbilities.mainPath == 3)
                {
                    DescriptionTitle.text = shieldpage5Title;
                    DescriptionDescribe.text = shielpage5Description;
                }

                if (playerAbilities.mainPath == 2)
                {
                    DescriptionTitle.text = bootspage5Title;
                    DescriptionDescribe.text = bootspage5Description;
                }
            }

            if (ShopPage == 6)
            {
                if (playerAbilities.mainPath == 1)
                {
                    DescriptionTitle.text = swordpage6Title;
                    DescriptionDescribe.text = sword6Description;
                }

                if (playerAbilities.mainPath == 3)
                {
                    DescriptionTitle.text = shieldpage6Title;
                    DescriptionDescribe.text = shielpage6Description;
                }

                if (playerAbilities.mainPath == 2)
                {
                    DescriptionTitle.text = bootspage6Title;
                    DescriptionDescribe.text = bootspage6Description;
                }
            }
        }

        if (playerAbilities.subPath == 2)
        {
            if (ShopPage == 4)
            {
                if (playerAbilities.mainPath == 1)
                {
                    DescriptionTitle.text = path2swordpage4Title;
                    DescriptionDescribe.text = path2sword4Description;
                }

                if (playerAbilities.mainPath == 3)
                {
                    DescriptionTitle.text = path2shieldpage4Title;
                    DescriptionDescribe.text = path2shielpage4Description;
                }

                if (playerAbilities.mainPath == 2)
                {
                    DescriptionTitle.text = path2bootspage4Title;
                    DescriptionDescribe.text = path2bootspage4Description;
                }
            }

            if (ShopPage == 5)
            {
                if (playerAbilities.mainPath == 1)
                {
                    DescriptionTitle.text = path2swordpage5Title;
                    DescriptionDescribe.text = path2sword5Description;
                }

                if (playerAbilities.mainPath == 3)
                {
                    DescriptionTitle.text = path2shieldpage5Title;
                    DescriptionDescribe.text = path2shielpage5Description;
                }

                if (playerAbilities.mainPath == 2)
                {
                    DescriptionTitle.text = path2bootspage5Title;
                    DescriptionDescribe.text = path2bootspage5Description;
                }
            }

            if (ShopPage == 6)
            {
                if (playerAbilities.mainPath == 1)
                {
                    DescriptionTitle.text = path2swordpage6Title;
                    DescriptionDescribe.text = path2sword6Description;
                }

                if (playerAbilities.mainPath == 3)
                {
                    DescriptionTitle.text = path2shieldpage6Title;
                    DescriptionDescribe.text = path2shielpage6Description;
                }

                if (playerAbilities.mainPath == 2)
                {
                    DescriptionTitle.text = path2bootspage6Title;
                    DescriptionDescribe.text = path2bootspage6Description;
                }
            }
        }
    }

    public void CloseDescription()
    {
        descriptionMenu.SetActive(false);
    }

    public void NextPage()
    {
        if(ShopPage < PagesUnlocked)
        {
            ShopPage++;
        }
    }

    public void PreviousPage()
    {
        if(ShopPage > 1)
        {
            ShopPage--;
        }
    }

    public void Purchase(int type) 
    {
        if(playerAbilities.playerCoin >= price)
        {
            if (ShopPage == 1)
            {
                PlayerPrefs.SetInt("HasRoll", 1);
                playerAbilities.playerCoin -= price;
            }

            if (ShopPage == 2)
            {
                playerAbilities.mainPath = type;
                playerAbilities.subPath = 0;
                playerAbilities.subPathLevel = 0;
                playerAbilities.playerCoin -= price;
            }

            if (ShopPage == 3)
            {
                playerAbilities.subPath = type;
                playerAbilities.subPathLevel = 0;
                playerAbilities.playerCoin -= price;
            }

            if (ShopPage >= 4)
            {
                if (ShopPage == 4 && playerAbilities.subPathLevel == 0)
                {
                    playerAbilities.subPathLevel = 1;
                    playerAbilities.playerCoin -= price;
                }

                if (ShopPage == 5 && playerAbilities.subPathLevel == 1)
                {
                    playerAbilities.subPathLevel = 2;
                    playerAbilities.playerCoin -= price;
                }

                if (ShopPage == 6 && playerAbilities.subPathLevel == 2)
                {
                    playerAbilities.subPathLevel = 3;
                    playerAbilities.playerCoin -= price;
                }
            }
        }
        

    }


}
