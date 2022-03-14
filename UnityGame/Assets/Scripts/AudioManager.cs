using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource[] soundEffects;
    public AudioSource backGroundMusic;
    public AudioSource endLevelMusic;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySFX(int soundIndex)
    {
        soundEffects[soundIndex].Stop();

        soundEffects[soundIndex].pitch = Random.Range(0.85f, 1.15f);

        soundEffects[soundIndex].Play();
    }
}