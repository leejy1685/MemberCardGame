# ❤ 팀 소개
- **프로젝트 명 :  [Unity 9기] 맴버 카드 프로젝트**
- **프로젝트 소개 :  우리 팀, 우리만의 이야기를 담을 수 있는 ‘팀원 소개 카드게임’ 으로 만드는 것을 목표로 합니다.**
<img src="https://github.com/user-attachments/assets/68e1279e-9c20-4b58-99f7-8ca8cf8c07b9" width="300" />



# 필수 구현 기능 제작
### 과제목표
    
- 한 사람 당 본인을 대표할 수 있는 이미지를 필요한 만큼 준비해주세요.
- 모든 카드 매칭 성공 시, 팀원들의 이름 및 사진 한 눈에 보여주기 / 실패 시 실패를 알리는 문구 노출
- 추가 기능 구현을 살펴보고 게임에 여러 요소를 더하여 우리만의 게임을 제작해봅시다.


<details>
<summary>필수 구현 기능 와이어프레임</summary>

![image](https://github.com/user-attachments/assets/a8869c53-8fb4-42c1-820a-92e9af98c90b)

</details>

<details>
<summary> 역할 분담 </summary>

1. 이준영 : StartScene, Audio(시작 화면, 화면 전환)
<details>
<summary> 작업물 </summary>
StageManager.cs
```csharp
int stage;

    public GameObject stage2;
    public GameObject stage3;
    public GameObject hiddenStage;

    private void Start()
    {
    	AudioManager.instance.BGMSound();

        stage = PlayerPrefs.GetInt("stageClear");

        if(stage >= 2)
        {
            stage2.SetActive(true);
        }
        if(stage >= 3)
        {
            stage3.SetActive(true);
        }
        if(stage >= 4)
        {
            hiddenStage.SetActive(true);
        }
    }

```
StageScene의 UI를 관리하기 위한 코드이다. PlayerPrefs에 저장된 나의 스테이지 클리어 기록을 가져와서 입장 가능한 stage를 표시한다.
BGMSound()가 있는 이유는 MainScene에서 실패한 후 돌아오면 사운드가 변경되지 않기 때문이다.
![image](https://github.com/user-attachments/assets/1279df3c-b9aa-45a5-bb8f-eaa18bf5abd8)

```csharp

    void Start()
    {
        PlayerPrefs.SetInt("stageClear",1);   //test Code
    }

```
게임의 클리어 기록을 초기화하기 위한 코드이다. StartScene에 있는 ResetCode 오브젝트를 활성화하고 실행하면 클리어 기록이 초기화 된다.
![image](https://github.com/user-attachments/assets/1933bbe6-d527-4eb1-8228-001a72ece823)

</details>

2. 한예준 : Card (랜덤 이미지 삽입)
3. 최홍진 : UI (시간 측정, 게임 종료 UI)
4. 송치웅 : GameManager (게임 진행에 필요한 C# 작성)
5. 윤지민 : Board (카드 랜덤 배치 및 뒤집기, 파괴)







</details>





















































# 추가 구현 기능 제작
## 추가 구현 기능 기획
- 난이도 시스템 구현
  - 1스테이지: 12장 (3x4)
  - 2스테이지: 16장 (4x4)
  - 3스테이지: 20장 (5x4)
  - 히든 스테이지: 20장 (5x4), 중간에 화면 가리는 오브젝트 출현
- 스테이지 클리어 시 아바타 및 사진 UI표시
- 기존 클리어 UI → 히든 스테이지 클리어 UI로 변경
- 게임 연출 추가
- 각종 행동에 사운드 삽입


</details>

<details>
<summary>추가 구현 기능 와이어프레임</summary>

![image (1)](https://github.com/user-attachments/assets/9bce4bca-68ec-476e-b479-f991524d396f)

[FigJam 링크](https://www.figma.com/board/kqfsLfo242uS1RmSHz0248/Welcome-to-FigJam?node-id=0-1&p=f&t=LT1XHxGTUypk7tS4-0)

</details>















<details>
<summary> 역할 분담 </summary>

## 1. 게임에 필요한 매니저 추가 작성
게임 매니저 추가기능 작성 (송치웅)
- 난이도 시스템 추가 (정보 값을 저장하여 다른 씬으로 전달)
- 각 스테이지에 60초 시간 제한 추가
- 게임 오버 시 점수와 스테이지 표기 추가
    
버튼 매니저 추가 (이준영)
-스테이지 이동 버튼, 게임 재시작 버튼 등 일괄 관리
<details>
<summary> 작업물 </summary>
Button.cs
```csharp
    AudioSource audioSource;
    public AudioClip clip;  //go sound

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void StartStage1()
    {
        //for GameManager and Board 
        PlayerPrefs.SetInt("stage", 1);	//1스테이지로 갈거야라고 GameManager에게 전달
        Time.timeScale = 1;	//timeScale이 1이어야 invoke가 실행 됨
        audioSource.PlayOneShot(clip);	//go sound 실행
        Invoke("StartGameInvoke", 0.5f);    //go sound를 들을 수 있게 0.5초의 간격
    }    
    public void StartStage2()
    {
        PlayerPrefs.SetInt("stage", 2);
        Time.timeScale = 1;
        audioSource.PlayOneShot(clip);
        Invoke("StartGameInvoke", 0.5f);
    }    
    public void StartStage3()
    {
        PlayerPrefs.SetInt("stage", 3);
        Time.timeScale = 1;
        audioSource.PlayOneShot(clip);
        Invoke("StartGameInvoke", 0.5f);
    }    
    public void StartStageHidden()
    {
        PlayerPrefs.SetInt("stage", 4);
        Time.timeScale = 1;
        audioSource.PlayOneShot(clip);
        Invoke("StartGameInvoke", 0.5f);
    }

    public void retryButton()
    {
        //현재 스테이지로 다시 간다고 GameManager 보내기
        PlayerPrefs.SetInt("stage", GameManager.Instance.getStage());
        SceneManager.LoadScene("MainScene");    //cat't play Invoke
    }

    public void stageButton()	//StageScene으로 넘어가는 코드
    {
        SceneManager.LoadScene("StageScene");
    }

    void StartGameInvoke()	//간격을 만들기 위한 인보크
    {
        SceneManager.LoadScene("MainScene");
    }

```
Button.cs는 Button을 관리하기 위해서 만든 스크립트이다. StartStage는 GameManager에게 현재 스테이지 정보를 넘기는 역할도 하고 있다.

Button이 한 스테이지에서 많이 있기도 하고 소리도 들어가야 하기 때문에 ButtonManager라는 오브젝트를 만들어서 관리하였다.
</details>

사운드 매니저 추가 (이준영)
- 카드를 클릭하거나 뒤집을 때, 게임이 시작될 때, 진행 중 성공 또는 실패 시 효과음을 삽입
- 타이머 시간이 촉박할 때, 게이머에게 경고하는 배경 음악으로 변경

<details>
<summary> 작업물 </summary>
AudioManager.cs
```csharp
//Singleton
    public static AudioManager instance;
    
    AudioSource audioSource;
    public AudioClip BGMClip;  //BGM
    public AudioClip timeOutClip;   //timeOut
    public AudioClip hurryUpSound;  //hurry up

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        BGMSound();
    }

    public void timeOutSound()
    {
        audioSource.PlayOneShot(hurryUpSound);  //one play
        audioSource.clip = timeOutClip;
        audioSource.Play(); //loop play
    }

    public void BGMSound()
    {
        audioSource.clip = BGMClip;
        audioSource.Play(); //loop play
    }


```
AudioManager.cs는 주로 BGM을 다루는 스크립트이다. 게임 시작 시에는 BGM이 나오지만 게임 플레이 중 20초 이내로 가게 되면 hurry up 사운드와 함께 째깍째깍하는 소리로 바뀌게하기 위해서 timeOutSound()함수를 만들었다.

AudioManager 오브젝트에는 AudioSource 컴포넌트의 loop를 true값으로 바꿔줘야 소리가 정상 작동 할 수 있다. 만약 이와 다른 방법으로 하고 싶다면 Start()함수에서 audioSource.loop = true; 를 추가하면 된다.

</details>
    
## 2. 게임에 연출 (한예준)
카드가 뒤집어지는 모습을 애니메이션으로 추가
- 카드를 클릭했을 때 애니메이션으로 Y축을 180도 회전
- 두 카드의 사진이 서로 같을 시 회전하며 소멸하는 애니메이션 추가
- 두 카드의 사진이 서로 다를 시
    
## 3. 스테이지 or 난이도 추가하기
카드의 개수가 늘어난 더 어려운 스테이지 구현(윤지민)
- 난이도 변수를 가져와 1줄씩 추가
- 1스테이지: 12장 (3×4) 이준영님 사진추가
- 2스테이지: 16장 (4×4) 한예준님, 윤지민님 사진추가
- 3스테이지: 20장 (5×4) 최홍진님, 송치웅님 사진추가
    
스테이지 선택, 구분 가능한 화면 제작 (최홍진)
- 와이어 프레임 기반으로 UI제작
    
## 4.히든 스테이지 구현하기
해금 조건  : 스테이지3을 20초 이상 남기고 클리어 (이준영)
- 3스테이지 클리어시 20초 조건을 확인하여 만족 못할시 난이도 변수값 - / 만족시 해금
<details>
<summary> 작업물 </summary>
게임 매니저는 내가 전부 만든게 아니라 내가 만든 부분만 적기로 하였다.
GameManager.cs
```csharp
public GameObject hiddenStageStart;	//히든 스테이크 클리어 조건 만족시 나오는 버튼
    public GameObject ink;	//히든 스테이지 시 생성되는 오브젝트
    //스테이지 클리어 마다 나오는 판넬이 다르기 때문에 배열로 구현
    public GameObject[] stageClearPanel = new GameObject[4];
	
    private void ShowClearUI()
    {
        if(stage == 3 && time <= 20)
        {	//히든 스테이크 오픈 조건에 맞으면 히든 스테이지로 바로 입장하는 버튼 생성
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
먼저 히든 스테이지를 구현하기 위해서 만든 코드 들이다. 3스테이지에서 남은 시간이 20초 이하 일 때 히든 스테이지로 넘어 갈 수 없게 버튼을 비활성화 시킨다.
그리고 만약 스테이지가 히든 스테이지(stage == 4)면 1.5초 마다 잉크가 생성 되게 설정 하였다.
```csharp
    bool time20 = true;
    
    AudioSource audioSource;	//소리 재생을 위한 클래스
    public AudioClip matchClip; //match sound
    public AudioClip notMatchClip;  //not match sound
    
    void Start()
    {	//오디오 소스 컴포넌트 가져오기
        audioSource = GetComponent<AudioSource>();
        
        ... 생략 ...
    }
    
    void Update()
    {
        GameStart();
    }
    
    public void GameStart()
	{
    	if (time < 0.0f)
    	{
			... 생략 ...
    	}
    	else if (time < 20.0f && time20)
    	{
       		AudioManager.instance.timeOutSound();
        	time20 = false;
    	}
    	else
    	{
        	... 생략 ...
    	}
    	... 생략 ...
	}
    
    
 	public void isMatched()
  	{
      	if (firstCard.idx == secondCard.idx)	//카드가 일치하면 
      	{
        	audioSource.PlayOneShot(matchClip);	//일치하는 소리
            ... 생략 ...
        }
        else	//일치하지 않으면
        {
        	audioSource.PlayOneShot(notMatchClip);	//일치하지 않는 소리
        }

```
다음은 사운드 기능이다. 남은 시간이 20초 미만이 되면 시간이 부족한 사운드로 변경을 해준다. 여기서 bool타입 변수로 해준 이유는 Update 함수에서 무한 반복 되면 소리가 첫 음만 계속 반복 되기 때문에 딱 한번만 실행 되도록 bool타입 변수로 조정 해주었다.

카드가 일치하면 일치하는 소리가 일치하지 않으면 일치하지 않는 소리가 재생되도록 넣어주었다.
```csharp
	int stage;
    
    public int getStage()	//현재 스테이지를 파악하고 넘겨주는 함수
    {						//Button.cs에선 GameManager에게 스테이지 정보를 넘기기 위해
    						//Board.cs에선 GameManager의 스테이지 정보를 받기 위해서 사용
        stage = PlayerPrefs.GetInt("stage");
        return stage;
    }
    
    void Start()
    {
		... 생략 ...
        getStage();	//이전 스테이지를 클리어하고 넘어오면 stage 변수가 초기화 되기 때문에 실행
		... 생략 ...
    }
    public void PlayerSaveData()
    {
    	//최고 클리어 기록 가져오기
        int bestStage = PlayerPrefs.GetInt("stageClear"); //히든 스테이지는 클리어 조건을 만족해야 함.
        if (stage == 3 && time <= 20)
        {
            stage--;
        }
        stage++;	//현재 스테이지를 클리어 했기 때문에 다음 스테이지까지 플레이 가능
        //최고 클리어 기록을 저장
        if (bestStage < stage)
        {
            bestStage = stage;
        }
        //최대 스테이지는 히든까지
        if(bestStage > 4)
        {
            bestStage = 4;
        }
		
        PlayerPrefs.SetInt("stageClear", bestStage);	//저장
        PlayerPrefs.Save();

    }

```
다음 기능은 스테이지 관리 기능이다. 플레이어 프리펩으로 스테이지를 관리하였다.
stage는 현재 스테이지를 말하고 stageClear는 내가 최대 플레이 가능한 스테이지를 말한다.

</details>

    
기본 베이스 스테이지 3에 중간 중간에 화면을 가리는 오브젝트 출현. (최홍진)
- 잉크(커지고 점점 사라지는 효과)프리팹 생성(최홍진)
<details>
<summary> 프리팹 </summary>
    
<img src="https://github.com/user-attachments/assets/340bbaad-f7ce-45d5-baaf-ebd15f95d30c" width="200" />

</details>

- 잉크 랜덤 드랍 (이준영)

<details>
<summary> Ink.cs </summary>
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
</details>




<details>
<summary>트러블 슈팅</summary>



</details>






# 시연영상
## 필수기능 완성본 시연영상
<details>
<summary>게임진행</summary>

<img src="https://github.com/user-attachments/assets/a20a8476-f106-4137-a212-3f6d579540c5" width="300" />

</details>

## 추가기능 완성본 시연영상

<details>
<summary> 게임진행 </summary>

<img src="https://github.com/user-attachments/assets/28233523-ce94-4f85-8ca4-bf58b4409bb5" width="300" />

</details>

<details>
<summary> 난이도증가 </summary>

<img src="https://github.com/user-attachments/assets/7ae69a64-b6df-4327-954e-cff5d7ca0cae" width="300" />

</details>

<details>
<summary> 히든 스테이지 </summary>

[![게임 영상 보기](https://img.youtube.com/vi/rcHFXvefBkI/0.jpg)](https://www.youtube.com/shorts/rcHFXvefBkI)

</details>




----------------------------------- 접기 예제 ----------------------------------------------


<details>
<summary> 접기 </summary>

내용

</details>




코드 작성 하실분
```csharp

코드

```





<details>
<summary> 작업물 </summary>

```csharp

코드

```

</details>







