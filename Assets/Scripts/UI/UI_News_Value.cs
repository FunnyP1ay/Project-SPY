using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_News_Value : MonoBehaviour
{
    public RectTransform        rectTransform;
    public TextMeshProUGUI      newsText;
    public float                moveSpeed; // UI 패널의 이동 속도

    private void OnEnable()
    {

        newsText = GetComponent<TextMeshProUGUI>();
        StartCoroutine(Timer());
    }
    void Update()
    {
        // UI 패널을 오른쪽으로 이동
        rectTransform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

        // UI 패널이 화면을 벗어나면 종료
     
    }
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(14f);
        Destroy(gameObject);
    }
}
