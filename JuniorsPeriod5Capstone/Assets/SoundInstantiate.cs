using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundInstantiate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InstantiateSound(GameObject Sound)
    {
        GameObject theSound = Instantiate(Sound);
        Destroy(theSound, 1f);
    }
}
