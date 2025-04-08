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
}