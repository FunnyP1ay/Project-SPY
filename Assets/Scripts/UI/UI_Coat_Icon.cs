using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Coat_Icon : MonoBehaviour
{
    public Image Coat_Icon;
    public Color targetColor = Color.red;
    public float transitionDuration = 1f;

    private Color originalColor;
    private bool isRed = false;
    private float elapsedTime = 0f;

    private void Start()
    {
        originalColor = Color.clear;
    }

    private void Update()
    {
        // 스프라이트 색상 변경
        if (isRed)
        {
            Coat_Icon.color = Color.Lerp(originalColor, targetColor, elapsedTime / transitionDuration);
        }
        else
        {
            Coat_Icon.color = Color.Lerp(targetColor, originalColor, elapsedTime / transitionDuration);
        }

        // 보간 시간 업데이트
        elapsedTime += Time.deltaTime;

        // 보간이 완료되면 반대 색으로 변경하고 보간 시간을 리셋
        if (elapsedTime >= transitionDuration)
        {
            isRed = !isRed;
            elapsedTime = 0f;
        }
    }
}
