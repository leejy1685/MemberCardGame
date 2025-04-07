using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    //public Card firstCard;
    //public Card seconedCard;

    public Text timeTxt;
    float time;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    void Start()
    {

    }
    void Update()
    {
        time += Time.deltaTime;
        timeTxt.text = time.ToString("N2");
    }
    void Mached()
    {
        
    }
}