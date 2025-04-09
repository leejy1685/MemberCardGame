using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ink : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float x = Random.Range(-2.0f, 2.0f);
        float y = Random.Range(-4.0f, 4.0f);

        transform.position = new Vector3(x, y, 0);
        Invoke("DestroyInvoke", 10.0f);
    }
    void DestroyInvoke()
    {
        Destroy(gameObject);
    }


}
