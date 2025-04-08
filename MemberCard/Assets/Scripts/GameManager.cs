using System.Collections;
using System.Collections.Generic;
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
    int stage = 1;

    public int cardCount = 0;

    AudioSource audioSource;

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
        // TimeAttack
        if (time > 30.0f)
        {
            time = 30.0f;
            Gameover();
            ShowEndUI();
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
            // destroyCard
            firstCard.DestroyCard();
            secondCard.DestroyCard();
            
            cardCount -= 2;
            score++;

            if(cardCount == 0)
            {
                Gameover();
                ShowEndUI();
            }
        }
        else
        {
            // closeCard
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