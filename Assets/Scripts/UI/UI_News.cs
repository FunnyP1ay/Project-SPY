using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_News : MonoBehaviour
{
    public List<string>     baseNews_List;
    public List<string>     playerNews_List;

    public UI_News_Value    newsPrefab;
    public RectTransform    newsPos;
    private Queue<string>   newsQueue       = new Queue<string>();
    private Queue<string>   playerQueue     = new Queue<string>();
    private int             rand;

    public string playerNew_1 = "도시내 총격전이 발생했다는 소식입니다.";
    public string playerNew_2 = "괴한이 도심에서 사격을 했습니다.";
    public string playerNew_3 = "현재 경찰이 괴한을 지명수배 중 입니다.";

    public string LawerNew_1 = "정부가 계엄령을 선포 했습니다.";
    public string LawerNew_2 = "정부가 계엄령을 해제 했습니다.";
    public string LawerNew_3 = "정부가 여행 금지를 선포 했습니다.";
    public string LawerNew_4 = "정부가 여행 허용을 선포 했습니다.";
    public string LawerNew_5 = "정부가 연애 금지를 선포 했습니다.";
    public string LawerNew_6 = "정부가 연애 허용을 선포 했습니다.";
    public string LawerNew_7 = "정부가 게임 금지를 선포 했습니다.";
    public string LawerNew_8 = "정부가 게임 허용을 선포 했습니다.";
    public string LawerNew_9 = "정부가 금주령을 선포 했습니다.";
    public string LawerNew_10 = "정부가 금주령을 해제 했습니다.";
    public string LawerNew_11 = "정부가 흡연 금지를 선포 했습니다.";
    public string LawerNew_12 = "정부가 흡연 허용을 선포 했습니다.";
    public string LawerNew_13 = "정부가 반려동물 금지를 선포 했습니다.";
    public string LawerNew_14 = "정부가 반려동물 허용을 선포 했습니다.";
    public string LawerNew_15 = "정부가 세금 인상을 선포 했습니다.";
    public string LawerNew_16 = "정부가 세금 인하을 선포 했습니다.";
    public string LawerNew_17 = "정부가 스포츠 이벤트를 지원하기로 했습니다.";
    public string LawerNew_18 = "정부가 스포츠 이벤트를 중단하기로 했습니다.";
    public string LawerNew_19 = "정부가 두발자유 허용를 선포 했습니다.";
    public string LawerNew_20 = "정부가 단발령을 선포 했습니다.";
    public string LawerNew_21 = "정부가 헬스장 출입 금지를 선포 했습니다.";
    public string LawerNew_22 = "정부가 헬스장 금지를 해제 했습니다.";
    public string LawerNew_23 = "정부가 퍼레이드 지원을 선포 했습니다.";
    public string LawerNew_24 = "정부가 퍼레이드 금지를 선포 했습니다.";
    public string LawerNew_25 = "정부가 위대한 우리 시장님 영화를 개봉 했습니다.";
    public string LawerNew_26 = "정부가 병원비 지원을 선포 했습니다.";
    public string LawerNew_27 = "정부가 병원비 지원을 중단 했습니다.";

    private void Awake()
    {
     
        baseNews_List = new List<string> {
            "오늘 날씨는 매우 맑음 일것으로 예상됩니다. ",
            "최근 비만 인구가 늘고있다는 소식입니다.",
            "올해의 게임 ! SPY THE MAN ! 축하합니다!",
            "오늘 밤 9시 핫 엉덩이쇼 ! 놓치지 마세요 !",
            "(광고) 핫요가 And 핫초코 동호회 회원 모집 중",
            "오늘 예비군 훈련장에서 예비군이 탈영했습니다.",
            "정부가 교통을 위해 운전자 우선주의를 발표했습니다.",
            "모여라 먼지먼지 ! 청소기 할인행사중 입니다",
            "햄버거 최대 몇개까지?의 연구가 시작됐습니다",
            "최근 흡연율이 급격하게 증가하고 있습니다.",
            "쉰내제과의 사료맛 과자가 인기를 끌고있습니다.",
            "가수 유교걸씨가 신곡(방구뿡)을 발표했습니다",
            "도시에 스파이가 있다는 소문이 돌고있습니다.",
            "요즘 청년사이에 엉덩이붐 패션이 유행 하고있습니다.",
            "오늘 미세먼지 농도는 어제보다 낮습니다",
            "대머리 소프트의 주식이 10% 떨어졌습니다.",
            "작년 출산율은 재작년보다 감소한 2.0 이였습니다.",
            "올해 예상 우리도시 성장률은 2.1% 입니다.",
            "정부가 자동차세 인하를 검토중 이라고 밝혔습니다",
            "청년을 위한 '노답'주택 200가구가 공급 예정입니다.",
            "AI에 의한 일자리 문제가 심각해지고 있습니다",
            "직장인 평균 커피 소비가 증가하고 있습니다.",
            "대머리 소프트의 주식이 8% 올랐습니다.",
            "청년 실업이 심각해지고 있어 사회적으로 문제입니다.",
            "불우한 대머리 돕기 성금이 총 210$ 모였습니다.",
            "오늘 이웃간 친목 캠페인'빡빡이가 간다' 예정입니다.",
            "자신이 산타라고 주장하는 시민이 나타나 화제입니다.",
            "귓속에 도청 장치가 들어있읍니다 여러분! 귓속에 도청..",
            "올해 베스트 셀러'책 쓰기 싫다'가 200만권 팔렸습니다.",
            "해외에서 공격용 드론의 구매자가 많아졌습니다.",
            
            
        };  //안에 내용 넣음
        playerNews_List = new List<string> { "도시내 총격전이 발생했다는 소식입니다.","괴한이 도심에서 사격을 했습니다.","현재 경찰이 괴한을 지명수배 중 입니다."
        };
    }
    void Start()
    {

        StartCoroutine(ShowNewsRoutine());
        StartCoroutine(NextNewsSet());
    }

  
    public void AddNews(string news)
    {
        print("뉴스를 생성했습니다");
        newsQueue.Enqueue(news);
    }
    public void PlayerNew()
    {
        rand = Random.Range(0, playerNews_List.Count);
      
        playerQueue.Enqueue(playerNews_List[rand]);
    }
    private IEnumerator ShowNewsRoutine()
    {
        while (true)
        {
            string news;
            if (playerQueue.Count > 0)
            {
                news = playerQueue.Dequeue();
            }
            else
            {
                news = newsQueue.Dequeue(); // Queue에서 뉴스 꺼내기
            }

            // UI 텍스트 생성
            var newsText = Instantiate(newsPrefab, newsPos);
            newsText.newsText.text = news;
            newsText.newsText.fontSize = 40f;

            yield return new WaitForSeconds(4f); // 각 뉴스 표시 후 잠시 대기
        }
    }
    private IEnumerator NextNewsSet()
    {
        while (true)
        {
            print("일반뉴스를 생성했습니다");
            rand = Random.Range(0, baseNews_List.Count);
            AddNews(baseNews_List[rand]);
            yield return new WaitForSeconds(3.9f);
        }
    }

    // UI 패널이 화면을 벗어났는지 확인하는 함수
  
    // 새로운 뉴스 추가
 

}
