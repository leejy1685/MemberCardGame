using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int idx = 0;
    public GameObject front;
    public GameObject back;
    public Animator anim;

    public void OpenCard()
    {
        Debug.Log("OpenCard »£√‚µ !");
        anim.SetBool("isOpen", true);
        front.SetActive(true);
        back.SetActive(false); 
    }

    public SpriteRenderer frontImage;

    public void setting(int number)
    {
        idx = number;
        frontImage.sprite = Resources.Load<Sprite>($"card{idx}");
    }
}
