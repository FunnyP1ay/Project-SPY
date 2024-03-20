using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PlayerState : MonoBehaviour
{
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
            UI_Manager.Instance.playerCoatStateImage.color = Color.Lerp(originalColor, targetColor, elapsedTime / transitionDuration);
        }
        else
        {
            UI_Manager.Instance.playerCoatStateImage.color = Color.Lerp(targetColor, originalColor, elapsedTime / transitionDuration);
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
