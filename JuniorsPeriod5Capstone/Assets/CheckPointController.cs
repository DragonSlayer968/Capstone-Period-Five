using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    public GameObject[] checkPoints;
    public int cpvalue;

    public GameObject player;

    private void Awake()
    {
        cpvalue = PlayerPrefs.GetInt("Cp");
        
    }

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        
        //cpvalue = FindObjectOfType<PlayerHealth>().checkPointValue;
    }

    public void CheckPointLoad()
    {
        if(cpvalue < checkPoints.Length)
        {
            player.transform.position = checkPoints[cpvalue].transform.position;
        }
        else
        {
            cpvalue = 0;
            PlayerPrefs.SetInt("Cp", 0);
            player.transform.position = checkPoints[0].transform.position;
        }
       
    }

}
