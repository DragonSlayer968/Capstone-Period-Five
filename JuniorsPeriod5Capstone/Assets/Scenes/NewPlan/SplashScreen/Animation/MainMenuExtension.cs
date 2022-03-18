using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuExtension : MonoBehaviour
{
    public MainMenu mm;
    // Start is called before the first frame update
    void Start()
    {
        mm = FindObjectOfType<MainMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExtensionAction()
    {
        mm.SelectedAction();
    }

}
