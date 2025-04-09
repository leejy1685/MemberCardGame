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
        anim.SetTrigger("flip");//카드가 열리는 애니메이션 재생, 카드 열기

        if (clip != null)
            audioSource.PlayOneShot(clip);

        if (GameManager.Instance.firstCard == null)
        {
            GameManager.Instance.firstCard = this;//첫번째 카드 저장
        }
        else
        {
            GameManager.Instance.secondCard = this;//두번째 카드 저장
            GameManager.Instance.isMatched();//isMatched() 함수 호출
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
    //0.5초 동안 카드가 다시 뒤집히는 애니메이션 재생, 카드 뒤집기
    public void DestroyCard()
    {
        Invoke("DestroyCardInvoke", 0.5f);
    }

    private void DestroyCardInvoke()
    {
        anim.SetTrigger("Destroy");
        Destroy(this.gameObject, 0.3f);
    }
    //카드가 파괴되는 애니메이션 호출 후 0.3초 후에 카드 파괴
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
    //애니메이션 이벤트 함수
    public void setting(int number)
    {
        idx = number;
        frontImage.sprite = Resources.Load<Sprite>($"Card{idx}");
    }
    //앞면 이미지 리소스에서 가져오기
}

