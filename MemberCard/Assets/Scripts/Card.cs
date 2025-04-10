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

    public void OpenCard()//play animation when the card is clicked
    {
        anim.SetTrigger("flip");

        if (clip != null)
            audioSource.PlayOneShot(clip);

        if (GameManager.Instance.firstCard == null)
        {
            GameManager.Instance.firstCard = this;//saving first card
        }
        else
        {
            GameManager.Instance.secondCard = this;//saving second card
            GameManager.Instance.isMatched();//define function to check if the cards are matched
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
    //Play animation when the card is not matched
    public void DestroyCard()
    {
        Invoke("DestroyCardInvoke", 0.5f);
    }

    private void DestroyCardInvoke()
    {
        anim.SetTrigger("Destroy");
        Destroy(this.gameObject, 0.3f);
    }
    //Destroy card after 0.3 seconds when the card is matched
    public void SwitchToFront()
    {
        front.SetActive(true);
        back.SetActive(false);
    }
    //Functions to switch between front and back of the card(insert to 'flip' animation)
    public void SwitchToBack()
    {
        front.SetActive(false);
        back.SetActive(true);
    }
    //Funtions to switch between front and back of the card(insert to 'flipback' animation)
    public void setting(int number)
    {
        idx = number;
        frontImage.sprite = Resources.Load<Sprite>($"Card{idx}");
    }
    //Get the card number and load the image
}

