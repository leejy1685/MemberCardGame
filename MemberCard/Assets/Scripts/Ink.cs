using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ink : MonoBehaviour
{
    void Start()
    {
        float x = Random.Range(-2.0f, 2.0f);
        float y = Random.Range(-4.0f, 2.0f);

        transform.position = new Vector3(x, y, 0);
        Destroy(gameObject,6.0f);
    }

}
