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
    public GameObject clearPanel;

    float time = 0.0f;
    int score = 0;
    int stage = 1;
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
        // �ð� ����
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

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnInk();
            timer = 0f;
        }
    }
    public void isMatched()
    {
        if (firstCard.idx == secondCard.idx)
        {
            audioSource.PlayOneShot(matchClip);

            // idx�� ��ġ�ϸ� destroyCard
            firstCard.DestroyCard();
            secondCard.DestroyCard();
            // Board���� arr[i]�� �޾ƿ���
            cardCount -= 2;
            score++;

            if(cardCount == 0) // ��� ���߸� ���� ����
            {
                AudioManager.instance.BGMSound();
                Gameover();
                clearPanel.SetActive(true);
                //ShowEndUI();
            }
        }
        else
        {
            audioSource.PlayOneShot(notMatchClip);
            // idx�� ��ġ ���� ������ closeCard
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

    public GameObject Ink;
    public float spawnInterval = 1.0f; // �Թ� ���� ����
    public Vector2 spawnAreaMin = new Vector2(-5, -5); // ���� ���� �ּҰ�
    public Vector2 spawnAreaMax = new Vector2(5, 5);

    private float timer;

    void SpawnInk()
    {
        Vector2 randomPos = new Vector2(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y)
        );

        Instantiate(Ink, randomPos, Quaternion.identity);
    }
}