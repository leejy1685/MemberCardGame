using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // Card���� 1�� 2��ī�� idx�� �Ѱܹ���
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
        //    // ���� idx�� ��ġ�ϸ� destroy
        //    // Destroy();
        //}
        //else
        //{
        //    // ��ġ ���� ������ ī�� ������ ����

        //}
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