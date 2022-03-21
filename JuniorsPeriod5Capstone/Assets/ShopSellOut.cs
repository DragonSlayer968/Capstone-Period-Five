using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSellOut : MonoBehaviour
{
    public int shopValue;
    public Button buyButton;
    public Text buyText;
    public Text price;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(shopValue == 1 && FindObjectOfType<PlayerMovement>().rollUnlocked == true)
        {
            buyButton.interactable = false;
            buyText.text = "Sold Out!";
            price.text = "XXX";
        }

        else
        {
            buyButton.interactable = true;
            buyText.text = "Purchase";
            price.text = FindObjectOfType<Shop>().price.ToString(); 
        }
        
        PlayerAbilities pa = FindObjectOfType<PlayerAbilities>();
        if (shopValue == 2)
        {
            if (FindObjectOfType<Shop>().ShopPage == 4)
            {
                if (pa.subPathLevel >= 1)
                {
                    buyButton.interactable = false;
                    buyText.text = "Sold Out!";
                    price.text = "XXX";
                }

                else
                {
                    buyButton.interactable = true;
                    buyText.text = "Purchase";
                    price.text = FindObjectOfType<Shop>().price.ToString();
                }
            }

            if (FindObjectOfType<Shop>().ShopPage == 5)
            {
                if (pa.subPathLevel >= 2)
                {
                    buyButton.interactable = false;
                    buyText.text = "Sold Out!";
                    price.text = "XXX";
                }

                else
                {
                    buyButton.interactable = true;
                    buyText.text = "Purchase";
                    price.text = FindObjectOfType<Shop>().price.ToString();
                }
            }

            if (FindObjectOfType<Shop>().ShopPage == 6)
            {
                if (pa.subPathLevel >= 3)
                {
                    buyButton.interactable = false;
                    buyText.text = "Sold Out!";
                    price.text = "XXX";
                }

                else
                {
                    buyButton.interactable = true;
                    buyText.text = "Purchase";
                    price.text = FindObjectOfType<Shop>().price.ToString();
                }
            }

        }

       

    }
}
