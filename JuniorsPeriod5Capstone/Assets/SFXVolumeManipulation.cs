using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXVolumeManipulation : MonoBehaviour
{
    public AudioSource audiothing;
    // Start is called before the first frame update
    void Start()
    {
        audiothing = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        audiothing.volume = FindObjectOfType<InGameMenu>().sfxVolume;   
    }
}
