using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{   
    //Singleton
    public static AudioManager instance;

    
    AudioSource audioSource;
    public AudioClip BGMclip;  //BGM
    public AudioClip timeOutClip;   //timeOut

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        BGMSound();
    }

    public void timeOutSound()
    {
        audioSource.clip = timeOutClip;
        audioSource.Play(); //loop play
    }

    public void BGMSound()
    {
        audioSource.clip = BGMclip;
        audioSource.Play(); //loop play
    }


}
