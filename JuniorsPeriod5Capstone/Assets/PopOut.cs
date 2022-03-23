using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopOut : MonoBehaviour
{
    public Text popOutText;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void popManipulation(string what)
    {
        popOutText.text = what;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
