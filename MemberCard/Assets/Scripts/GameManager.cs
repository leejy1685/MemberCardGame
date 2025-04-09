using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // Card에서 1번 2번카드 idx를 넘겨받음
    //public Card firstCard;
    //public Card secondCard;

    public Text timeTxt;
    float time = 0.0f;

    AudioSource audioSource;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        time += Time.deltaTime;
        timeTxt.text = time.ToString("N2");

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnInk();
            timer = 0f;
        }
    }
    void cardMached()
    {
        //if ()
        //{
        //    // 서로 idx가 일치하면 destroy
        //    // Destroy();
        //}
        //else
        //{
        //    // 일치 하지 않으면 카드 뒤집어 놓음

        //}
    }

    public GameObject Ink;
    public float spawnInterval = 1.0f; // 먹물 생성 간격
    public Vector2 spawnAreaMin = new Vector2(-5, -5); // 스폰 영역 최소값
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