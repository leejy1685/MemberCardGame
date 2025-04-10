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

    public GameObject hiddenStageStart;
    public GameObject ink;
    public GameObject endPanel;
    public GameObject[] stageClearPanel = new GameObject[4];

    float time = 60.0f;
    int score = 0;
    bool time20 = true;

    int stage;
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
        audioSource = GetComponent<AudioSource>();
        AudioManager.instance.BGMSound();
        getStage();
        Time.timeScale = 1.0f;

        if(stage == 4)  //hidden stage
        {
            InvokeRepeating("MakeInk", 0.0f, 1.5f);
        }
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
                AudioManager.instance.BGMSound();
                Gameover();
                ShowClearUI();
                PlayerSaveData();
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
        stageTxt.text = stage.ToString();
    }
    private void ShowClearUI()
    {
        if(stage == 3 && time <= 20)
        {
            hiddenStageStart.SetActive(false);
        }
        stageClearPanel[stage-1].SetActive(true);
    }

    public void PlayerSaveData()
    {
        int bestStage = PlayerPrefs.GetInt("stageClear");
        //hidden stage open condition
        if (stage == 3 && time <= 20)
        {
            stage--;
        }
        stage++;
        //best clear data save
        if (bestStage < stage)
        {
            bestStage = stage;
        }
        //maxStage
        if(bestStage > 4)
        {
            bestStage = 4;
        }

        PlayerPrefs.SetInt("stageClear", bestStage);
        PlayerPrefs.Save();

    }

    public int getStage()
    {
        stage = PlayerPrefs.GetInt("stage");
        return stage;
    }

    void MakeInk()
    {
        Instantiate(ink);
    }
}