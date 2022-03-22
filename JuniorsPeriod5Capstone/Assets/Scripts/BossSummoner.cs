using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSummoner : MonoBehaviour
{
    public GameObject boss;
    public GameObject obj;

    public bool objActivate;
    public bool objDeactivate;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!boss)
        {
            if (objActivate)
            {
                obj.SetActive(true);
            }
            if (objDeactivate)
            {
                obj.SetActive(false);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            boss.SetActive(true);
            if (objActivate)
            {
                obj.SetActive(false);
            }
            if (objDeactivate)
            {
                obj.SetActive(true);
            }
        }
    }
}
