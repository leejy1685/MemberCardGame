using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetCode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("stageClear",1);   //test Code
    }

}
