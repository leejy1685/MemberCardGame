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

    public void StartStage1()
    {
        //for GameManager and Board 
        PlayerPrefs.SetInt("stage", 1);
        Time.timeScale = 1;
        audioSource.PlayOneShot(clip);
        Invoke("StartGameInvoke", 0.5f);    //interbal
    }    
    public void StartStage2()
    {
        PlayerPrefs.SetInt("stage", 2);
        Time.timeScale = 1;
        audioSource.PlayOneShot(clip);
        Invoke("StartGameInvoke", 0.5f);
    }    
    public void StartStage3()
    {
        PlayerPrefs.SetInt("stage", 3);
        Time.timeScale = 1;
        audioSource.PlayOneShot(clip);
        Invoke("StartGameInvoke", 0.5f);
    }    
    public void StartStageHidden()
    {
        PlayerPrefs.SetInt("stage", 4);
        Time.timeScale = 1;
        audioSource.PlayOneShot(clip);
        Invoke("StartGameInvoke", 0.5f);
    }

    public void retryButton()
    {
        //get current stage 
        PlayerPrefs.SetInt("stage", GameManager.Instance.getStage());
        SceneManager.LoadScene("MainScene");    //cat't play Invoke
    }

    public void stageButton()
    {
        AudioManager.instance.BGMSound();
        SceneManager.LoadScene("StageScene");
    }

    void StartGameInvoke()
    {
        SceneManager.LoadScene("MainScene");
    }
}
