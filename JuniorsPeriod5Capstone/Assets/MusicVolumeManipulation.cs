using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicVolumeManipulation : MonoBehaviour
{
    public AudioSource target;

    // Start is called before the first frame update
    void Start()
    {
        target = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        target.volume = FindObjectOfType<InGameMenu>().musicVolume;
    }
}
