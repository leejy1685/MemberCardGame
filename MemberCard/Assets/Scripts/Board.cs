using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Board : MonoBehaviour
{
    public Transform Cards;
    public GameObject card;
    int currentStage = GameManager.Instance.stage;
    // Start is called before the first frame update
    void Start()
    {
        if (currentStage == 1)
        {
            int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5 };
            arr = arr.OrderBy(x => Random.Range(0f, 5f)).ToArray();
            for (int i = 0; i < 12; i++)
            {
                GameObject go = Instantiate(card, this.transform);

                float x = (i % 3) * 1.4f - 1.4f;
                float y = (i / 3) * 1.4f - 2.8f;
                go.transform.position = new Vector2(x, y);

                go.GetComponent<Card>().setting(arr[i]);

            }

        }
        else if (currentStage == 2)
        {
            int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };
            arr = arr.OrderBy(x => Random.Range(0f, 7f)).ToArray();
            for (int i = 0; i < 16; i++)
            {
                GameObject go = Instantiate(card, this.transform);
                float x = (i % 4) * 1.2f - 1.8f;
                float y = (i / 4) * 1.2f - 2.8f;
                go.transform.position = new Vector2(x, y);
                go.GetComponent<Card>().setting(arr[i]);
            }
        }
        else if (currentStage == 3)
        {
            int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9 };
            arr = arr.OrderBy(x => Random.Range(0f, 9f)).ToArray();
            for (int i = 0; i < 20; i++)
            {
                GameObject go = Instantiate(card, this.transform);

                float x = (i % 4) * 1.2f - 1.8f;
                float y = (i / 4) * 1.2f - 3.9f;
                go.transform.position = new Vector2(x, y);

                go.GetComponent<Card>().setting(arr[i]);
            }
        }

        else // position check(delete later)
        {
            int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };
            arr = arr.OrderBy(x => Random.Range(0f, 7f)).ToArray();
            for (int i = 0; i < 16; i++)
            {
                GameObject go = Instantiate(card, this.transform);
                float x = (i % 4) * 1.2f - 1.8f;
                float y = (i / 4) * 1.2f - 2.8f;
                go.transform.position = new Vector2(x, y);
                go.GetComponent<Card>().setting(arr[i]);


            }

        }



    }

    // Update is called once per frame
    void Update()
    {

    }
}
