using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    public GameObject[] checkPoints;
    public int cpvalue;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        cpvalue = PlayerPrefs.GetInt("Cp");
    }

    // Update is called once per frame
    void Update()
    {
        cpvalue = FindObjectOfType<PlayerHealth>().checkPointValue;
    }

    public void CheckPointLoad()
    {
        player.transform.position = checkPoints[cpvalue].transform.position;
    }

}
