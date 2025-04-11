# ❤ 팀 소개

<img src="https://github.com/user-attachments/assets/68e1279e-9c20-4b58-99f7-8ca8cf8c07b9" width="300" />

## **프로젝트 명**

- **[Unity 9기] 맴버 카드 프로젝트**

## **프로젝트 소개**

- **우리 팀, 우리만의 이야기를 담을 수 있는 ‘팀원 소개 카드게임’ 으로 만드는 것을 목표로 합니다.**

## **팀원 소개**

- 이준영 : 컴퓨터공학부 컴퓨터공학전공, 게임 제작 동아리 출신, Unity 조금 해봄
- 한예준 : 동양철학 전공, 미식축구팀 코치로 활동중입니다. 게임을 좋아하고 만들어 보고 싶어서 캠프참여를 했습니다.
- 최홍진 : 자동자학과 전공.  전공업종 퇴사하면서 Unity를 배우기로 결정, 게임관련 업종은 이번이 처음입니다.
- 송치웅 : 몇년전에 독학(C, WEB, Python)을 조금 하다가 이번에 Unity 게임 개발할 기회가 생겨서 캠프에 참여했습니다.
- 윤지민 : 코딩, 게임 엔진 다루는것 모두 처음이지만 게임 하는것을 좋아하여 도전



# 필수기능 제작
## 과제목표
    
- 한 사람 당 본인을 대표할 수 있는 이미지를 필요한 만큼 준비해주세요.
- 모든 카드 매칭 성공 시, 팀원들의 이름 및 사진 한 눈에 보여주기 / 실패 시 실패를 알리는 문구 노출
- 추가 기능 구현을 살펴보고 게임에 여러 요소를 더하여 우리만의 게임을 제작해봅시다.

## 필수기능 완성본 시연영상
<details>
<summary> 게임 진행 </summary>

<img src="https://github.com/user-attachments/assets/a20a8476-f106-4137-a212-3f6d579540c5" width="300" />

</details>

## 필수기능 기획
<details>
<summary>필수 구현 기능 와이어프레임</summary>

![image](https://github.com/user-attachments/assets/a8869c53-8fb4-42c1-820a-92e9af98c90b)

</details>

<details>
<summary> 역할 분담 </summary>

### 1. 이준영 : StartScene, Audio(시작 화면, 화면 전환, 사운드 추가)
<details>
<summary> 작업물 </summary>

Button.cs

```csharp
    AudioSource audioSource;  // 오디오 소스를 담을 변수. 버튼 클릭 시 소리를 재생하기 위해 사용.
    public AudioClip clip;    // 버튼 클릭 시 재생될 오디오 클립을 저장할 변수.

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // 게임 시작 버튼 클릭 시 호출
    public void StartGame()
    {
        Time.timeScale = 1.0f;  // 게임 시간을 정상 흐름으로 설정. 일시정지 상태를 해제하는 역할.
        audioSource.PlayOneShot(clip);  // 버튼 클릭 시 설정된 오디오 클립을 한 번 재생.
        AudioManager.instance.BGMSound();  // 오디오 매니저를 통해 배경 음악을 시작.
        Invoke("StartGameInvoke", 0.5f); //오디오 클립 재생이 끝날 때 까지 씬 0.5초 간격 만들기
    }

    // 리셋 버튼 클릭 시 호출
    public void resetButton()
    {   
        Time.timeScale = 1.0f;  // 게임 시간을 정상 흐름으로 설정, 일시정지 상태 해제.
        AudioManager.instance.BGMSound();  // 오디오 매니저를 통해 배경 음악을 시작.
        SceneManager.LoadScene("StartScene");  // 스타트 씬으로 전환, 게임을 초기 상태로 리셋.
    }

    // 0.5초 후에 호출
    void StartGameInvoke()
    {
        SceneManager.LoadScene("MainScene");  // 메인 씬으로 전환
    }

```
오디오까지 고려하여 미리준비
</details>

### 2. 한예준 : Card (프리팹의 기능과 Card 애니메이션 담당)

<details>
<summary> 작업물 </summary>

Card.cs

```csharp
    public int idx = 0;  // 카드의 고유 번호를 저장하는 변수
    public GameObject front;  // 카드의 앞면
    public GameObject back;   // 카드의 뒷면
    public Animator anim;  // 카드 애니메이션 제어

    AudioSource audioSource;
    public AudioClip clip;

  
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // 카드를 열 때 호출
    public void OpenCard()
    {
        audioSource.PlayOneShot(clip);  // 카드가 열릴 때 소리를 한 번 재생

        anim.SetBool("isOpen", true);  // 카드 애니메이션에서 "isOpen" 파라미터를 true로 설정하여 카드를 여는 애니메이션을 실행
        front.SetActive(true);  // 카드의 앞면을 활성화
        back.SetActive(false);  // 카드의 뒷면을 비활성화

        // 첫 번째 카드가 아직 선택되지 않았다면 첫 번째 카드로 설정
        if (GameManager.Instance.firstCard == null)
        {
            GameManager.Instance.firstCard = this;
        }
        else
        {
            // 두 번째 카드가 선택되었을 때
            GameManager.Instance.secondCard = this;
            GameManager.Instance.isMatched();  // 카드가 맞는지 검사
        }
    }

    // 카드를 삭제할 때 호출
    public void DestroyCard()
    {
        Invoke("DestoryCardInvoke", 1.0f);  // 1초 후 DestoryCardInvoke 메소드를 호출하여 카드를 삭제
    }

    // 카드 삭제 함수
    void DestoryCardInvoke()
    {
        Destroy(gameObject);  // 게임 오브젝트(카드)를 삭제
    }

    // 카드를 닫을 때 호출
    public void CloseCard()
    {
        Invoke("CloseCardInvoke", 1.0f);  // 1초 후 CloseCardInvoke 메소드를 호출하여 카드를 닫음
    }

    // 카드 닫기 함수
    void CloseCardInvoke()
    {
        anim.SetBool("isOpen", false);  // 카드 애니메이션에서 "isOpen" 파라미터를 false로 설정하여 카드를 닫는 애니메이션 실행
        front.SetActive(false);  // 카드의 앞면을 비활성화
        back.SetActive(true);    // 카드의 뒷면을 활성화
    }

    public SpriteRenderer frontImage;  // 카드의 앞면 이미지를 표시하는 SpriteRenderer

    // 카드의 이미지를 설정하는 함수
    public void setting(int number)
    {
        idx = number;  // 카드의 고유 번호를 설정
        frontImage.sprite = Resources.Load<Sprite>($"Card{idx}");  // Resources 폴더에서 해당 카드 이미지를 로드하여 적용
    }
```
</details>

### 3. 최홍진 : UI (와이어 프레임 작성, 시간 측정 UI, 게임 종료 UI)
<details>
<summary> 작업물 </summary>
와이어 프레임 기반으로 UI를 제작

![image](https://github.com/user-attachments/assets/288e8270-247f-4d08-abc5-0459027883cb)
![image](https://github.com/user-attachments/assets/0ccec7a6-9bc0-48f5-a6ab-26d447d541ba)

</details>

### 4. 송치웅 : GameManager (게임 진행에 필요한 C# 작성)
<details>
<summary> 작업물 </summary>

GameManager.cs

```csharp
 public static GameManager Instance;  // 싱글톤 패턴을 적용하여 다른 스크립트에서 접근할 수 있는 인스턴스

    public Card firstCard;  // 첫 번째로 선택된 카드
    public Card secondCard;  // 두 번째로 선택된 카드

    public Text timeTxt;  // 시간 표시를 위한 UI 텍스트
    public Text scoreTxt;  // 점수 표시를 위한 UI 텍스트
    public Text stageTxt;  // 단계 표시를 위한 UI 텍스트
    public GameObject endPanel;  // 게임 종료 시 표시될 UI 패널
    public GameObject clearPanel;  // 게임 클리어 시 표시될 UI 패널

    float time = 0.0f;  // 게임 시간
    int score = 0;  // 플레이어 점수
    int stage = 1;  // 현재 게임 단계
    bool time20 = true;  // 20초 지났을 때 알림을 한 번만 보내기 위한 변수

    public int cardCount = 0;  // 남은 카드 수

    AudioSource audioSource;  // 게임 오디오를 재생하기 위한 AudioSource
    public AudioClip matchClip;  // 카드가 맞았을 때 재생될 소리
    public AudioClip notMatchClip;  // 카드가 맞지 않았을 때 재생될 소리

    // 싱글톤 패턴을 위한 Awake
    void Awake()
    {
        if (Instance == null)
            Instance = this;  // 인스턴스가 없다면 현재 오브젝트를 인스턴스로 설정
    }


    void Start()
    {
        Time.timeScale = 1.0f;  // 게임의 시간을 정상 속도로 설정
        audioSource = GetComponent<AudioSource>();  // AudioSource 컴포넌트 가져오기
    }

    void Update()
    {
        // 시간 제한 체크 (30초가 지나면 게임 오버)
        if (time > 30.0f)
        {
            time = 30.0f;  // 시간을 30초로 제한
            Gameover();  // 게임 오버 호출
            ShowEndUI();  // 게임 종료 UI 표시
        }
        else
        {
            time += Time.deltaTime;  // 시간이 지나면 `time` 변수에 누적
        }

        // 게임 시간이 UI에 표시되도록 업데이트
        timeTxt.text = time.ToString("N2");  // 소수점 두 자리까지 표시
    }

    // 카드 두 개가 맞는지 확인
    public void isMatched()
    {
        // 두 카드의 번호가 일치하는지 확인
        if (firstCard.idx == secondCard.idx)
        {
            audioSource.PlayOneShot(matchClip);  // 카드가 맞으면 맞추기 소리 재생

            // 두 카드가 일치하면 삭제
            firstCard.DestroyCard();
            secondCard.DestroyCard();

            // 남은 카드 수를 2개 줄임 (두 카드를 맞췄기 때문에)
            cardCount -= 2;
            score++;  // 점수 1점 추가

            // 모든 카드를 맞췄으면 게임 종료
            if(cardCount == 0)
            {
                AudioManager.instance.BGMSound();  // 배경 음악을 재생
                Gameover();  // 게임 오버 처리
                clearPanel.SetActive(true);  // 게임 클리어 UI 활성화
            }
        }
        else
        {
            audioSource.PlayOneShot(notMatchClip);  // 카드가 맞지 않으면 틀리기 소리 재생

            // 두 카드가 일치하지 않으면 닫기
            firstCard.CloseCard();
            secondCard.CloseCard();
        }

        // 두 카드를 null로 설정하여 다음 카드 선택을 기다림
        firstCard = null;
        secondCard = null;
    }

    // 게임 오버 처리
    public void Gameover()
    {
        Time.timeScale = 0f;  // 게임 시간을 멈춤 (시간 흐르지 않게)
    }

```
싱글톤 처리하여 작업진행
</details>

### 5. 윤지민 : Board (카드 랜덤 배치 및 뒤집기, 파괴)
<details>
<summary> 작업물 </summary>

Board.cs

```csharp
    public Transform Cards;
    public GameObject card;

    // 게임 시작 시 카드들을 생성
    void Start()
    {
        // 카드에 할당될 번호 배열 (0~9까지의 숫자 두 개씩 포함)
        int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9 };
        
        // 배열을 무작위로 섞음 (랜덤 번호 배치를 위해)
        arr = arr.OrderBy(x => Random.Range(0f, 9f)).ToArray();

        // 카드 20개 생성
        for (int i = 0; i < 20; i++)
        {
            // 카드 프리팹을 인스턴스화하여 게임 오브젝트로 생성
            GameObject go = Instantiate(card, this.transform);

            // 카드의 위치를 계산하여 배치 (2D 좌표로 배치)
            float x = (i % 4) * 1.2f - 1.8f;  // x 좌표 계산 (4열로 배치)
            float y = (i / 4) * 1.2f - 3.9f;  // y 좌표 계산 (5행으로 배치)
            go.transform.position = new Vector2(x, y);  // 계산된 위치에 카드 배치

            // 각 카드에 번호를 설정
            go.GetComponent<Card>().setting(arr[i]);
        }

        // GameManager의 cardCount 변수에 생성된 카드 수 할당
        GameManager.Instance.cardCount = arr.Length;
    }

```
</details>

</details>

# 추가기능 제작

## 추가기능 완성본 시연영상

<details>
<summary> 게임 진행 </summary>

<img src="https://github.com/user-attachments/assets/28233523-ce94-4f85-8ca4-bf58b4409bb5" width="300" />

</details>

<details>
<summary> 난이도 증가 </summary>

<img src="https://github.com/user-attachments/assets/7ae69a64-b6df-4327-954e-cff5d7ca0cae" width="300" />

<br>

<img src="https://velog.velcdn.com/images/ghdwlsdl1/post/9108d430-76a3-44aa-a79f-bd89762be871/image.gif" width="300">

</details>

<details>
<summary> 히든스테이지 </summary>

[![게임 영상 보기](https://img.youtube.com/vi/rcHFXvefBkI/0.jpg)](https://www.youtube.com/shorts/rcHFXvefBkI)

</details>

## 추가기능 기획
- 난이도 시스템 구현
  - 1스테이지: 12장 (3x4)
  - 2스테이지: 16장 (4x4)
  - 3스테이지: 20장 (5x4)
  - 히든 스테이지: 20장 (5x4), 중간에 화면 가리는 오브젝트 출현
- 게임진행을 0->30초에서 60->0으로 변경
- 스테이지 클리어 시 아바타 및 사진 UI표시
- 기존 클리어 UI → 히든 스테이지 클리어 UI로 변경
- 게임 연출 추가
- 각종 행동에 사운드 삽입

</details>

<details>
<summary>추가 구현 기능 와이어프레임</summary>

![image](https://github.com/user-attachments/assets/c3fa60db-acb7-45d2-82a2-e16d660805f8)

[FigJam 링크](https://www.figma.com/board/kqfsLfo242uS1RmSHz0248/Welcome-to-FigJam?node-id=0-1&p=f&t=LT1XHxGTUypk7tS4-0)

</details>

<details>
<summary> 역할 분담 </summary>

## 1. 게임에 필요한 매니저 추가 작성
### 게임 매니저 추가기능 작성 (송치웅)
- 각 스테이지에 60초 시간 제한 추가
<details>
<summary> 작업물 </summary>

GameManager.cs

```csharp
    // 패배조건을 30초에서 60->0초로 변경
    if (time < 0.0f) // time == 0s -> Timeover
    {
        time = 0.0f;  // 시간은 0으로 설정
        Timeover();  // Timeover 메소드 호출
        ShowEndUI();  // 게임 종료 UI를 표시
    }
    // 시간이 20초 미만
    else if (time < 20.0f && time20)
    {
        AudioManager.instance.timeOutSound();  // 타임아웃 사운드 재생
        time20 = false;  //사운드가 다시 재생되지 않도록 처리
    }
    // 시간 감소
    else // time != 0 -> time Decrement
    {
        time -= Time.deltaTime;  // 매 프레임마다 시간 감소
    }

    // 현재 시간을 텍스트 형식으로 출력 (소수점 2자리까지 표시)
    timeTxt.text = time.ToString("N2");

```
</details>

- 게임 오버 시 점수와 스테이지 표기 추가
<details>
<summary> 작업물 </summary>

GameManager.cs

```csharp
    // 게임 종료 UI
    public void ShowEndUI()
    {
        endPanel.SetActive(true);  // 게임 종료 패널 활성화

        // 점수와 스테이지지 정보를 UI에 표시
        scoreTxt.text = score.ToString();
        stageTxt.text = stage.ToString();
    }

```
</details>

### 버튼 매니저 추가 (이준영)
- 스테이지 이동 버튼, 게임 재시작 버튼 등 일괄 관리
<details>
<summary> 작업물 </summary>

Button.cs

```csharp

    public void StartStage1()
    {
        // GameManager에게 1스테이지로 갈 것임을 알리기 위해 PlayerPrefs에 저장
        PlayerPrefs.SetInt("stage", 1); // "stage" 값으로 1을 설정
        Time.timeScale = 1;  // 시간 흐름을 정상으로 설정 (게임이 진행되도록)
        audioSource.PlayOneShot(clip);  // 게임 시작 소리 재생
        Invoke("StartGameInvoke", 0.5f);  // 0.5초 뒤에 StartGameInvoke 메소드 호출
    }

    // 스테이지2
    public void StartStage2()
    {
        // GameManager에게 2스테이지로 갈 것임을 알리기 위해 PlayerPrefs에 저장
        PlayerPrefs.SetInt("stage", 2);
        Time.timeScale = 1;
        audioSource.PlayOneShot(clip);
        Invoke("StartGameInvoke", 0.5f);
    }

    // 스테이지3
    public void StartStage3()
    {
        // GameManager에게 3스테이지로 갈 것임을 알리기 위해 PlayerPrefs에 저장
        PlayerPrefs.SetInt("stage", 3);
        Time.timeScale = 1;
        audioSource.PlayOneShot(clip);
        Invoke("StartGameInvoke", 0.5f);
    }

    // 히든 스테이지
    public void StartStageHidden()
    {
        PlayerPrefs.SetInt("stage", 4);
        Time.timeScale = 1;
        audioSource.PlayOneShot(clip);
        Invoke("StartGameInvoke", 0.5f);
    }

    // 리셋
    public void retryButton()
    {
        PlayerPrefs.SetInt("stage", GameManager.Instance.getStage());  // 현재 스테이지로 간다고 알려주기
        SceneManager.LoadScene("MainScene");  // 메인 씬으로 로드
    }

    // Main를 눌렀을 때, 스테이지 씬으로 넘어가는 메소드
    public void stageButton()
    {
        SceneManager.LoadScene("StageScene");  // 스테이지 씬으로 로드
    }

    // 게임 시작 소리 후, MainScene으로 넘어가는 메소드
    void StartGameInvoke()
    {
        SceneManager.LoadScene("MainScene");  // 메인 씬으로 로드
    }

```
StartStage는 GameManager에게 현재 스테이지 정보를 넘기는 역할도 하고 있다.
Button이 한 스테이지에서 많이 있기도 하고 소리도 들어가야 하기 때문에 ButtonManager라는 오브젝트를 만들어서 관리하였다.

</details>

### 사운드 매니저 추가 (이준영)
- 카드를 클릭하거나 뒤집을 때, 게임이 시작될 때, 진행 중 성공 또는 실패 시 효과음을 삽입
- 타이머 시간이 촉박할 때, 게이머에게 경고하는 배경 음악으로 변경

<details>
<summary> 작업물 </summary>
	
AudioManager.cs
	
```csharp
	
    public static AudioManager instance;
    
    AudioSource audioSource;  // 오디오 소스 컴포넌트
    public AudioClip BGMClip;  // BGM (배경 음악)
    public AudioClip timeOutClip;  // 타임아웃 사운드
    public AudioClip hurryUpSound;  // 급할 때 사운드

    // 오디오 매니저 인스턴스를 싱글톤으로 관리하는 Awake
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;  // 인스턴스를 현재 객체로 설정
            DontDestroyOnLoad(gameObject);  // 씬 전환 시에도 이 객체를 삭제하지 않도록 설정
        }
        else
        {
            Destroy(gameObject);  // 이미 인스턴스가 존재하면 중복 객체를 삭제
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();  // AudioSource 컴포넌트 가져오기
        BGMSound();  // 게임 시작 시 배경 음악을 재생
    }

    // 타임아웃 사운드
    public void timeOutSound()
    {
        audioSource.PlayOneShot(hurryUpSound);  // hurryUpSound를 한 번 재생
        audioSource.clip = timeOutClip;  // 타임아웃 사운드로 변경
        audioSource.Play();  // 타임아웃 사운드는 반복해서 재생
    }

    // 배경 음악
    public void BGMSound()
    {
        audioSource.clip = BGMClip;  // 배경 음악을 설정
        audioSource.Play();  // 배경 음악을 반복 재생
    }

```
</details>
    
## 2. 게임에 연출 (한예준)
- 카드가 등장할떄 애니메이션추가
- 카드가 뒤집어지는 모습을 애니메이션으로 추가
- 카드가 파괴되는 애니메이션 추가

<details>
<summary> 작업물 </summary>

Card.cs

```csharp
public int idx = 0;  // 카드의 고유 번호를 저장하는 변수
public GameObject front;  // 카드 앞면
public GameObject back;  // 카드 뒷면
public Animator anim;  // 카드의 애니메이션 제어
public SpriteRenderer frontImage;  // 카드 앞면의 이미지를 표시
AudioSource audioSource;
public AudioClip clip;

private void Start()
{
    anim = GetComponent<Animator>();  // Animator 컴포넌트를 가져옴
    audioSource = GetComponent<AudioSource>();  // AudioSource 컴포넌트를 가져옴
    front.SetActive(false);  // 카드 앞면을 처음에 보이지 않도록 설정
    back.SetActive(true);  // 카드 뒷면을 처음에 보이도록 설정
}

public void OpenCard()
{
    anim.SetTrigger("flip");  // "flip" 트리거를 사용해 카드를 뒤집는 애니메이션을 시작

    if (clip != null)
        audioSource.PlayOneShot(clip);  // 사운드 클립을 한 번 재생

    if (GameManager.Instance.firstCard == null)
    {
        GameManager.Instance.firstCard = this;  // 첫 번째 카드를 저장
    }
    else
    {
        GameManager.Instance.secondCard = this;  // 두 번째 카드를 저장
        GameManager.Instance.isMatched();  // 카드 매칭 여부 확인
    }
}

public void CloseCard()
{
    Invoke("CloseCardInvoke", 0.5f);  // 0.5초 후에 CloseCardInvoke 메서드 호출
}

private void CloseCardInvoke()
{
    anim.SetTrigger("flipback");  // "flipback" 트리거를 사용해 카드를 원위치로 뒤집는 애니메이션을 실행
}

public void SwitchToFront()
{
    front.SetActive(true);  // 카드 앞면을 보이게 설정
    back.SetActive(false);  // 카드 뒷면을 숨김
}

public void SwitchToBack()
{
    front.SetActive(false);  // 카드 앞면을 숨김
    back.SetActive(true);  // 카드 뒷면을 보이게 설정
}

public void DestroyCard()
{
    Invoke("DestroyCardInvoke", 0.5f);  // 0.5초 후에 DestroyCardInvoke 메서드 호출
}

private void DestroyCardInvoke()
{
    anim.SetTrigger("Destroy");  // "Destroy" 트리거를 사용해 카드 삭제 애니메이션 실행
    Destroy(this.gameObject, 0.3f);  // 카드 객체를 0.3초 후에 파괴
}

public void setting(int number)
{
    idx = number;  // 카드의 고유 번호 설정
    frontImage.sprite = Resources.Load<Sprite>($"Card{idx}");  // 카드 앞면 이미지를 Resources 폴더에서 로드하여 설정
}

```
애니메이션을 여러개 나누어 실행하고 이벤트를 나누어 앞면과 뒷면표현

</details>

## 3. 스테이지 or 난이도 추가하기
### 카드의 개수가 늘어난 더 어려운 스테이지 구현(윤지민)
- 난이도 변수를 가져와 1줄씩 추가
- 1스테이지: 12장 (3×4) 
- 2스테이지: 16장 (4×4) 
- 3스테이지: 20장 (5×4) 
<details>
<summary> 작업물 </summary>

Board.cs

```csharp

int currentStage = GameManager.Instance.getStage();  // 현재 스테이지 번호를 게임메니저에서 가져옴

// 스테이지1 카드 12개 배치
if (currentStage == 1)
{
    int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5 };  // 카드 12개 배치
    arr = arr.OrderBy(x => Random.Range(0f, 5f)).ToArray();

    for (int i = 0; i < 12; i++)
    {
        GameObject go = Instantiate(card, this.transform);

        float x = (i % 4) * 1.2f - 1.8f;  // x 위치 계산
        float y = (i / 4) * 1.2f - 2.8f;  // y 위치 계산
        go.transform.position = new Vector2(x, y);  // 계산된 위치로 카드 배치

        go.GetComponent<Card>().setting(arr[i]);  // 각 카드의 번호를 설정
    }

    GameManager.Instance.cardCount = arr.Length;  // 카드 개수 설정
}

// 스테이지2
else if (currentStage == 2)
{
    int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };  // 카드 16개 배치
    arr = arr.OrderBy(x => Random.Range(0f, 7f)).ToArray();

    for (int i = 0; i < 16; i++)
    {
        GameObject go = Instantiate(card, this.transform);
        float x = (i % 4) * 1.2f - 1.8f;
        float y = (i / 4) * 1.2f - 2.8f;
        go.transform.position = new Vector2(x, y);
        go.GetComponent<Card>().setting(arr[i]);
    }

    GameManager.Instance.cardCount = arr.Length;
}

// 스테이지3
else if (currentStage >= 3)
{
    int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9 };  // 카드 20개 배치
    arr = arr.OrderBy(x => Random.Range(0f, 9f)).ToArray();

    for (int i = 0; i < 20; i++)
    {
        GameObject go = Instantiate(card, this.transform);

        float x = (i % 4) * 1.2f - 1.8f;
        float y = (i / 4) * 1.2f - 3.9f;  //y 위치 계산 (y값을 약간 더 아래로 설정)
        go.transform.position = new Vector2(x, y);

        go.GetComponent<Card>().setting(arr[i]);
    }

    GameManager.Instance.cardCount = arr.Length;
}

```
</details>

### 스테이지 선택, 구분 가능한 화면 제작 (최홍진)
- 와이어 프레임 기반으로 UI제작
<details>
<summary> 작업물 </summary>
	
![image](https://github.com/user-attachments/assets/bfdfc628-3913-41f6-9bc2-49523a48ad64)
![image](https://github.com/user-attachments/assets/ed84fc7a-d1ff-44f4-ab02-7c02e8795454)
![image](https://github.com/user-attachments/assets/ab933b87-f83c-47c6-9833-e06284373fdf)
![image](https://github.com/user-attachments/assets/5c7c5472-b001-4aae-9709-dee6f062f07f)

</details>

## 4.히든 스테이지 구현하기
### 해금 조건  : 스테이지3을 20초 이상 남기고 클리어 (이준영)
<details>
<summary> 작업물 </summary>



```csharp

	public GameObject hiddenStageStart;	//히든 스테이크 클리어 조건 만족시 나오는 버튼
    	public GameObject ink;	//히든 스테이지 시 생성되는 오브젝트
    	//스테이지 클리어 마다 나오는 판넬이 다르기 때문에 배열로 구현
    	public GameObject[] stageClearPanel = new GameObject[4];
	
    	private void ShowClearUI()
    	{
        	if(stage == 3 && time <= 20) //히든 스테이지 조건 스테이지3 클리어 & 20초 이상 클리어
        	{	//히든 스테이지 오픈 조건에 맞으면 히든 스테이지로 바로 입장하는 버튼 생성
            		hiddenStageStart.SetActive(false);
        	}
        	//배열로 구현된 클리어 판넬 
        	stageClearPanel[stage-1].SetActive(true);
    	}
    
    	void Start()
    	{
    		... 중략 ...
    	
        	if(stage == 4)  //hidden stage
        	{	//1.5초 마다 잉크 생성
            	InvokeRepeating("MakeInk", 0.0f, 1.5f);
        	}
    	}
    
    	void MakeInk()	//잉크 생성
    	{
        	Instantiate(ink);
    	}
```
</details>

### 기본 베이스 스테이지 3에 중간 중간에 화면을 가리는 오브젝트 출현.(최홍진)
- 잉크(커지고 점점 사라지는 효과)프리팹 제작
<details>
<summary> 프리팹 </summary>
    
<img src="https://github.com/user-attachments/assets/340bbaad-f7ce-45d5-baaf-ebd15f95d30c" width="200" />

```csharp
    
    void Start()
    {
        float x = Random.Range(-2.0f, 2.0f); // 랜덤 좌표값
        float y = Random.Range(-4.0f, 2.0f);

        transform.position = new Vector3(x, y, 0); //랜덤생성
        Destroy(gameObject,6.0f); //오브젝트 파괴
    }

```
</details>

</details>


<details>
<summary>트러블 슈팅</summary>

# 이준영님
<details>
<summary> 접기 </summary>

Invoke 미작동
다 마무리 했다고 생각하고 테스트 하던 중 팀원 중 한분이 EndPanel에 RE? 버튼이 작동하지 않는 점을 찾아 주셨다.
직접 테스트 해보니, Go라는 사운드는 들리는데 LoadScene이 실행이 되지 않았다.
```csharp
    public void retryButton()
    {
        PlayerPrefs.SetInt("stage", GameManager.Instance.getStage());
        Time.timeScale = 1.0f;
        audioSource.PlayOneShot(clip);
        Invoke("StartGameInvoke", 0.5f);
    }

```
Button.cs에 있는 retryButton()이다. 혹시 getStage()에서 잘 못된 값을 가져와서 씬을 초기화 하지 못하는지 테스트 하기 위해서 Debug.Log()을 추가했다.
```csharp
    public void retryButton()
    {
        PlayerPrefs.SetInt("stage", GameManager.Instance.getStage());
         Debug.Log(GameManager.Instance.getStage())
        Time.timeScale = 1.0f;
        audioSource.PlayOneShot(clip);
        Invoke("StartGameInvoke", 0.5f);
    }

```
![image](https://github.com/user-attachments/assets/df97050d-9d5e-4d77-8eff-fb8ae35384d6)

잘 되는것 같다.
그러면 Go 라는 사운드 까지 잘 들렸으니 Invoke에 있는 StartGameInvoke안에 Debug를 찍어보기로 하였다.
```csharp
    void StartGameInvoke()
    {
        Debug.Log("asdasdsad");
        SceneManager.LoadScene("MainScene");
    }

```
![image](https://github.com/user-attachments/assets/e547ba67-c05a-42d4-a8ac-cc6c1b7d7b35)
처음 stage1에 들어올 때는 작동이 되지만 Re? 버튼에선 작동하지 않는다. 아쉽지만 Go 사운드를 포기하고 Invoke를 LoadScene으로 고쳐 보기로 했다.

```csharp
    public void retryButton()
    {
        PlayerPrefs.SetInt("stage", GameManager.Instance.getStage());

        Time.timeScale = 1.0f;
		SceneManager.LoadScene("MainScene");
    }

```
![image](https://github.com/user-attachments/assets/37124aab-ab02-4059-9ae9-1bae7af57428)

Invoke가 실행되지 않는게 원인이었는데 이유를 알 수가 없어서 인터넷에 검색해 보니, timeScale이 0이 되어 있으면 작동 되지 않는다고 한다.
아마 GameManager에서 남은 시간이 0초 이하가 되면 timeScale을 계속 0으로 고정 시키기 때문에 발생한 오류로 보인다.


</details>

# 한예준님
<details>
<summary> 접기 </summary>

기능 구현 중 문제 상황과 해결
- 카드 뒤집는 애니메이션 무한 반복 문제
- bool값에 의해 애니메이션이 재생
- bool값이 한번 정해지면 애니메이션이 계속 재생되므로 빠져나가는 조건 설정
- 하지만 빠져나가는 조건이 또 다른 애니메이션을 재생하는 조건을 충족시킴
- 따라서 무한 반복되는 문제가 발생하였음

문제 해결 방법
- 카드를 뒤집는 애니메이션을 재생하는 조건을 trigger로 설정
- trigger 로 설정 시 조건이 겹치는 문제가 없어짐.
- 또한 trigger 조건 충족 시 애니메이션은 한번만 재생되므로 문제 상황이 발생하지 않음.

</details>

# 최홍진님
<details>
<summary> 접기 </summary>
<img src="https://github.com/user-attachments/assets/4524e4cd-2411-4c41-9c6d-fa69aab1b415" width="300" />
	
이미지 위에 텍스트를 배치하고 그 안에 버튼을 넣는 방식으로 UI를 구성했을 때 예상치 못한 문제가 발생했습니다.

GitHub에서 작업을 합치면서 이 점을 사전에 알리지 못했고, 이미지에 버튼을 만들어 버튼이 작동하지 않았습니다.

![image](https://github.com/user-attachments/assets/bfbf55e6-4df2-4bd4-b3e1-96ef0f004670)

문제는 텍스트가 이미지보다 위에 배치되어 버튼이 텍스트에 가려져 클릭이 전달되지 않게 된 것이었습니다.

즉, 버튼은 시각적으로 존재했으나 실제로는 텍스트가 버튼을 가리고 있어 버튼이 작동하지 않았습니다.

이 경험을 통해 협업 시 내 작업 사항이 다른 작업에 영향을 미칠 수 있음을 깨달았습니다.

</details>

# 송치웅님
<details>
<summary> 접기 </summary>
	
Card idx가 일치했을 때 코드 구성이 헷갈려서 강의 보고 만들었던 작업 결과물 찾아서 수정 했습니다.
```csharp
// 변경 전
Destroy(firstCard);
Destroy(secondCard);

// 변경 후 
firstCard.DestroyCard();
secondCard.DestroyCard();

```
사전강의 들었을 땐 버전을 유니티6 [6000.0.36f1]을 사용했는데 PlayerPrefs가 없어져서 당시에는 다른 방법을 찾아서 적용했었습니다.
[22.3.17f1] 버전으로 와서 처음 써보는 PlayerPrefs 를 사용하려고 강의도 찾아보고 구글도 검색하느라 시간을 많이 사용했습니다.
</details>

# 윤지민님 
<details>
<summary> 접기 </summary>

가장 고민을 많이 한 부분
원인 분석
```csharp
int currentStage = GameManager.Instance.getStage() // int currentStage == GameManager.Instance.stage()(기능은 동일)

```
문장을
```csharp
void Start()
	{

	}

```
사이에 적지 않고 그 위에서 적용하여 현재 스테이지를 불러오지 못하는 문제가 있었다
그냥 Board.cs에 적기만 하면 알아서
```csharp
int currentStage == GameManager.Instance.stage;

```
를 통하여 항시적으로 GameManager에서 현재 스테이지 정보를 불러 오는 줄 알았으나
```csharp
void Start()
    {
	int currentStage == GameManager.Instance.stage;
    }
```
를 통해야 GameManager가 초기화 된 이후의 현재 저장된 사용자의 스테이지 값을 받아올 수 있었던 것

</details>




