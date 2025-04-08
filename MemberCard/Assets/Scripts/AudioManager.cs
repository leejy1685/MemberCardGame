using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{   
    //Singleton
    public static AudioManager instance;

    
    AudioSource audioSource;
    public AudioClip BGMClip;  //BGM
    public AudioClip timeOutClip;   //timeOut
    public AudioClip hurryUpSound;  //hurry up

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);            // 중복재생방지
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        BGMSound();
    }

    public void timeOutSound()
    {
        audioSource.PlayOneShot(hurryUpSound);  //one play
        audioSource.clip = timeOutClip;
        audioSource.Play(); //loop play
    }

    public void BGMSound()
    {
        audioSource.clip = BGMClip;
        audioSource.Play(); //loop play
    }


}
