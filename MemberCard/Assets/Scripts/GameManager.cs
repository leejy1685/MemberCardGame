using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Card firstCard;
    public Card secondCard;

    public Text timeTxt;
    public Text scoreTxt;
    public Text stageTxt;

    public GameObject endPanel;
    public GameObject clearPanel;
    //public GameObject hiddenPanel;

    float time = 60.0f;
    int score = 0;
    bool time20 = true;

    public int cardCount = 0;

    AudioSource audioSource;
    public AudioClip matchClip; //match sound
    public AudioClip notMatchClip;  //not match sound

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    void Start()
    {
        Time.timeScale = 1.0f;
        audioSource = GetComponent<AudioSource>();

        // TestDebug
        // Debug.Log($"clearStage : {PlayerPrefs.GetInt("stageClear")}");
    }
    void Update()
    {
        GameStart();
    }

    public void GameStart()
    {
        if (time < 0.0f)
        {
            time = 0.0f;
            Gameover();
            ShowEndUI();
        }
        else if (time < 20.0f && time20)
        {
            AudioManager.instance.timeOutSound();
            time20 = false;
        }
        else
        {
            time -= Time.deltaTime;
        }
        timeTxt.text = time.ToString("N2");
    }
    public void Gameover()
    {
        Time.timeScale = 0f;
    }
    public void isMatched()
    {
        if (firstCard.idx == secondCard.idx)
        {
            audioSource.PlayOneShot(matchClip);

            firstCard.DestroyCard();
            secondCard.DestroyCard();

            cardCount -= 2;
            score++;

            if(cardCount == 0) // Gameclear
            {
                PlayerSaveData();
                // TestDebug
                // Debug.Log($"clearStage : {PlayerPrefs.GetInt("stageClear")}");

                // ShowNextStageUI();
                Gameover();
                ShowClearUI();
            }
        }
        else
        {
            audioSource.PlayOneShot(notMatchClip);

            firstCard.CloseCard();
            secondCard.CloseCard();
        }
        firstCard = null;
        secondCard = null;
    }
    public void ShowEndUI()
    {
        endPanel.SetActive(true);

        scoreTxt.text = score.ToString();
        stageTxt.text = PlayerPrefs.GetInt("stageClear").ToString();
    }
    public void ShowClearUI()
    {
        clearPanel.SetActive(true);
    }
    //public void ShowHiddenUI()
    //{
    //    hiddenPanel.SetActive(true);
    //}
    public void PlayerSaveData()
    {
        int previous = PlayerPrefs.GetInt("stageClear", 1);
        int nextStage = previous + 1;

        PlayerPrefs.SetInt("stageClear", nextStage);
        PlayerPrefs.Save();
        Debug.Log($"스테이지 저장 : {nextStage}");
    }
}