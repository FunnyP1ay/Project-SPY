using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_News : MonoBehaviour
{
    public float moveSpeed = 100f; // UI 패널의 이동 속도
    private RectTransform rectTransform;
    private float panelWidth;

    private string baseNews_1 = "오늘 날씨는 맑음 입니다.";
    private string baseNews_2 = "최근 비만 인구가 늘고있습니다.";
    private string baseNews_3 = "올해의 게임. intern SPY 를 플레이 해보세요 !";
    private string baseNews_4 = "오늘 밤 9시 엉덩이쇼 ! 놓치지 마세요 !";
    private string baseNews_5 = "핫요가와 핫초코 동호회 회원 모집 !";
    private string baseNews_6 = "모여라 먼지먼지 ! 청소기 할인합니다 !";
    

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        panelWidth = rectTransform.rect.width;
    }

    void Update()
    {
        // UI 패널을 오른쪽으로 이동
        rectTransform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

        // UI 패널이 화면을 벗어나면 종료
        if (IsOffScreen())
        {
            // 비활성화하거나 삭제
            gameObject.SetActive(false);
            // 또는 Destroy(gameObject);
        }
    }

    // UI 패널이 화면을 벗어났는지 확인하는 함수
    bool IsOffScreen()
    {
        // UI 패널의 오른쪽 좌표가 화면 왼쪽 끝보다 작으면 화면을 벗어난 것으로 판단
        float rightEdge = rectTransform.position.x + panelWidth / 2;
        return rightEdge < 0f;
    }
}
