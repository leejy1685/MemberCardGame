using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    int stage;

    public GameObject stage2;
    public GameObject stage3;
    public GameObject hiddenStage;

    private void Start()
    { 

        stage = PlayerPrefs.GetInt("stageClear");

        if(stage >= 2)
        {
            stage2.SetActive(true);
        }
        if(stage >= 3)
        {
            stage3.SetActive(true);
        }
        if(stage >= 4)
        {
            hiddenStage.SetActive(true);
        }
    }
}
