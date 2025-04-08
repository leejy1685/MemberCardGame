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

    AudioSource audioSource;
    public AudioClip clip;  //flip sound

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OpenCard()
    {
        audioSource.PlayOneShot(clip);

        anim.SetBool("isOpen", true);
        front.SetActive(true);
        back.SetActive(false);
        /*
        if (GameManager.Instance.firstCard == null)
        {
            GameManager.Instance.firstCard = this;
        }
        else
        {
            GameManager.Instance.secondCard = this;
            GameManager.Instance.isMatched();
        }*/
    }
    /*public void DestroyCard()
    {
        Invoke("DestoryCardInvoke", 1.0f);
    }

    void DestoryCardInvoke()
    {
        Destroy(gameObject);
    }

    public void CloseCard()
    {
        Invoke("CloseCardInvoke", 1.0f);
    }

    void CloseCardInvoke()
    {
        anim.SetBool("isOepn", false);
        front.SetActive(false);
        back.SetActive(true);
    }*/
    public SpriteRenderer frontImage;

    public void setting(int number)
    {
        idx = number;
        frontImage.sprite = Resources.Load<Sprite>($"Card{idx}");
    }
}
