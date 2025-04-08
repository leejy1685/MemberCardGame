using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{   
    //Singleton
    public static AudioManager instance;

    
    AudioSource audioSource;
    public AudioClip clip;  //BGM

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

        audioSource.clip = clip;
        audioSource.Play(); //loop play
    }


}
