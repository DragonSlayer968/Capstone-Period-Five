using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called before the first frame update
    public int coinValue;
    public float coinSize;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(coinValue < 11)
        {
            coinSize = 1;
        }

        else if(coinValue < 21)
        {
            coinSize = 1.1f;
        }

        else
        {
            coinSize = 1.2f;
        }

        transform.localScale = new Vector3(coinSize, coinSize, 1);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<PlayerAbilities>().playerCoin += coinValue;
        }
    }
}
