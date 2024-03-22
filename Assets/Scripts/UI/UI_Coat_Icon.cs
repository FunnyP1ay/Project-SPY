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
        // ��������Ʈ ���� ����
        if (isRed)
        {
            Coat_Icon.color = Color.Lerp(originalColor, targetColor, elapsedTime / transitionDuration);
        }
        else
        {
            Coat_Icon.color = Color.Lerp(targetColor, originalColor, elapsedTime / transitionDuration);
        }

        // ���� �ð� ������Ʈ
        elapsedTime += Time.deltaTime;

        // ������ �Ϸ�Ǹ� �ݴ� ������ �����ϰ� ���� �ð��� ����
        if (elapsedTime >= transitionDuration)
        {
            isRed = !isRed;
            elapsedTime = 0f;
        }
    }
}
