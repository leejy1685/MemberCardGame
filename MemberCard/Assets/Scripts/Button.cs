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
        PlayerPrefs.SetInt("stage", 1);
        Time.timeScale = 1.0f;
        audioSource.PlayOneShot(clip);
        Invoke("StartGameInvoke", 0.5f);
    }    
    public void StartStage2()
    {
        PlayerPrefs.SetInt("stage", 2);
        Time.timeScale = 1.0f;
        audioSource.PlayOneShot(clip);
        Invoke("StartGameInvoke", 0.5f);
    }    
    public void StartStage3()
    {
        PlayerPrefs.SetInt("stage", 3);
        Time.timeScale = 1.0f;
        audioSource.PlayOneShot(clip);
        Invoke("StartGameInvoke", 0.5f);
    }    
    public void StartStageHidden()
    {
        PlayerPrefs.SetInt("stage", 4);
        Time.timeScale = 1.0f;
        audioSource.PlayOneShot(clip);
        Invoke("StartGameInvoke", 0.5f);
    }

    public void retryButton()
    {
        PlayerPrefs.SetInt("stage", GameManager.Instance.getStage());
        Time.timeScale = 1.0f;
        audioSource.PlayOneShot(clip);
        Invoke("StartGameInvoke", 0.5f);
    }

    public void stageButton()
    {
        SceneManager.LoadScene("StageScene");
    }

    void StartGameInvoke()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void SaveDataResetBtn()
    {
        // TestDebug
        Debug.Log($"초기화 전 {PlayerPrefs.GetInt("stageClear")}");

        PlayerPrefs.SetInt("stageClear", 1);
        PlayerPrefs.Save();

        // TestDebug
        Debug.Log($"데이터 초기화 {PlayerPrefs.GetInt("stageClear")}");
    }
}
