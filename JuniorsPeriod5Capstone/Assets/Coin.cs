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
        if(coinValue < 7)
        {
            coinSize = 1;
        }

        else if(coinValue < 14)
        {
            coinSize = 1.5f;
        }

        else
        {
            coinSize = 2f;
        }

        transform.localScale = new Vector3(coinSize, coinSize, 1);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<PlayerAbilities>().playerCoin += coinValue;
            Destroy(gameObject);
        }
    }
}
