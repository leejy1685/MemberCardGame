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
    public SpriteRenderer frontImage;
    AudioSource audioSource;
    public AudioClip clip;  //flip sound

    private void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        front.SetActive(false);
        back.SetActive(true);
    }

    public void OpenCard()
    {
        anim.SetTrigger("flip");

        if (clip != null)
            audioSource.PlayOneShot(clip);

        if (GameManager.Instance.firstCard == null)
        {
            GameManager.Instance.firstCard = this;
        }
        else
        {
            GameManager.Instance.secondCard = this;
            GameManager.Instance.isMatched();
        }
    }
    public void CloseCard()
    {
        Invoke("CloseCardInvoke", 0.5f);
    }

    private void CloseCardInvoke()
    {
        anim.SetTrigger("flipback");
    }
    public void SwitchToFront()
    {
        front.SetActive(true);
        back.SetActive(false);
    }

    public void SwitchToBack()
    {
        front.SetActive(false);
        back.SetActive(true);
    }

    public void DestroyCard()
    {
        Invoke("DestroyCardInvoke", 0.5f);
    }

    private void DestroyCardInvoke()
    {
        anim.SetTrigger("Destroy");
        Destroy(this.gameObject, 0.5f);
    }
    public void setting(int number)
    {
        idx = number;
        frontImage.sprite = Resources.Load<Sprite>($"Card{idx}");
    }
}

