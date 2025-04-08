using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip clip;  //go sound

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    
    public void StartGame()
    {
        audioSource.PlayOneShot(clip);
        Invoke("StartGameInvoke", 0.5f);
    }

    public void resetButton()
    {   
        Time.timeScale = 1.0f;
        AudioManager.instance.BGMSound();
        SceneManager.LoadScene("StartScene");
    }

    void StartGameInvoke()
    {
        SceneManager.LoadScene("MainScene");
    }
}
