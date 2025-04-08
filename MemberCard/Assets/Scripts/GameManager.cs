using System.Collections;
using System.Collections.Generic;
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

    float time = 0.0f;
    int score = 0;
    public int stage = 1;

    public GameManager(int stage)
    {
        this.stage = stage;
    }

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
    }
    void Update()
    {
        // 시간 제한
        if (time > 30.0f)
        {
            time = 30.0f;
            Gameover();
            ShowEndUI();
        }
        else if(time > 20.0f && time20)
        {
            AudioManager.instance.timeOutSound();
            time20 = false;
        }
        else
        {
            time += Time.deltaTime;
        }
        // time += Time.deltaTime;
        timeTxt.text = time.ToString("N2");
    }
    public void isMatched()
    {
        if (firstCard.idx == secondCard.idx)
        {
            audioSource.PlayOneShot(matchClip);

            // idx가 일치하면 destroyCard
            firstCard.DestroyCard();
            secondCard.DestroyCard();
            // Board에서 arr[i]값 받아오기
            cardCount -= 2;
            score++;

            if(cardCount == 0) // 모두 맞추면 게임 종료
            {
                Gameover();
                ShowEndUI();
            }
        }
        else
        {
            audioSource.PlayOneShot(notMatchClip);
            // idx가 일치 하지 않으면 closeCard
            firstCard.CloseCard();
            secondCard.CloseCard();
        }
        firstCard = null;
        secondCard = null;
    }

    public void Gameover()
    {
        Time.timeScale = 0f;
    }

    public void ShowEndUI()
    {
        endPanel.SetActive(true);

        scoreTxt.text = score.ToString();
        stageTxt.text = stage.ToString();
    }
}