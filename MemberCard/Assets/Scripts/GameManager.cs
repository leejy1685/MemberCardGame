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
}