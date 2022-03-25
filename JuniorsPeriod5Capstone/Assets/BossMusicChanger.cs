using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMusicChanger : MonoBehaviour
{
    public AudioClip music;
    public AudioSource levelMusic;
    // Start is called before the first frame update
    void Start()
    {
        levelMusic.clip = music;
        levelMusic.Play();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
