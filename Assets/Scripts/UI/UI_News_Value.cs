using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_News_Value : MonoBehaviour
{
    public RectTransform        rectTransform;
    public TextMeshProUGUI      newsText;
    public float                moveSpeed; // UI �г��� �̵� �ӵ�

    private void OnEnable()
    {

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
        yield return new WaitForSeconds(14f);
        Destroy(gameObject);
    }
}
