# ❤ 팀 소개
- **프로젝트 명 :  [Unity 9기] 맴버 카드 프로젝트**
- **프로젝트 소개 :  우리 팀, 우리만의 이야기를 담을 수 있는 ‘팀원 소개 카드게임’ 으로 만드는 것을 목표로 합니다.**
<img src="https://github.com/user-attachments/assets/68e1279e-9c20-4b58-99f7-8ca8cf8c07b9" width="300" />



# 필수 구현 기능 제작
### 과제목표
    
- 한 사람 당 본인을 대표할 수 있는 이미지를 필요한 만큼 준비해주세요.
- 모든 카드 매칭 성공 시, 팀원들의 이름 및 사진 한 눈에 보여주기 / 실패 시 실패를 알리는 문구 노출
- 추가 기능 구현을 살펴보고 게임에 여러 요소를 더하여 우리만의 게임을 제작해봅시다.


</details>

</details>

<details>
<summary>필수 구현 기능 와이어프레임</summary>

![image](https://github.com/user-attachments/assets/a8869c53-8fb4-42c1-820a-92e9af98c90b)

</details>

<details>
<summary> 역할 분담 </summary>

1. 이준영 : StartScene, Audio(시작 화면, 화면 전환)
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
    
사운드 매니저 추가 (이준영)
- 카드를 클릭하거나 뒤집을 때, 게임이 시작될 때, 진행 중 성공 또는 실패 시 효과음을 삽입
- 타이머 시간이 촉박할 때, 게이머에게 경고하는 배경 음악으로 변경
    
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
    
기본 베이스 스테이지 3에 중간 중간에 화면을 가리는 오브젝트 출현. (최홍진)
- 잉크(커지고 점점 사라지는 효과)프리팹 생성

<details>
<summary> 프리팹 </summary>
<img src="https://github.com/user-attachments/assets/340bbaad-f7ce-45d5-baaf-ebd15f95d30c" width="200" />

C#스크립트
```csharp

    void Start()
    {
        float x = Random.Range(-2.0f, 2.0f); //x축값 설정
        float y = Random.Range(-4.0f, 2.0f); //y축값 설정

        transform.position = new Vector3(x, y, 0); // 랜덤생성
        Invoke("DestroyInvoke", 6.0f); //오브잭트 파괴 불러오기
    }

    void DestroyInvoke()
    {
        Destroy(gameObject); //오브잭트 파괴
    }

```

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

<img src="https://github.com/user-attachments/assets/08162e3f-3741-4ade-b44d-96fd312fe270" width="300" />

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









