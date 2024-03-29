using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_News_Value : MonoBehaviour
{
    public RectTransform        rectTransform;
    public TextMeshProUGUI      newsText;
    public float                moveSpeed = 30f; // UI �г��� �̵� �ӵ�

    private void OnEnable()
    {
        rectTransform = GetComponent<RectTransform>();
        newsText = GetComponent<TextMeshProUGUI>();
        StartCoroutine(Timer());
    }
    void Update()
    {
        // UI �г��� ���������� �̵�
        rectTransform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

        // UI �г��� ȭ���� ����� ����
     
    }
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}
