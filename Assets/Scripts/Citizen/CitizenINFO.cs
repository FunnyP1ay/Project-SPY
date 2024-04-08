using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static CitizenINFO;

public class CitizenINFO : MonoBehaviour
{

    private Transform       cam;
    private bool            isPanelOn = false;
    public TextMeshProUGUI  nameText;
    public TextMeshProUGUI  moneyText;
    public Sprite           current_Emotion;
    public GameObject       infoPanel;
    public int              money;
    public List<Sprite>     emotion_List;
    public float            emotionPoint = 10f;

    public enum Emotion
    {
        good,
        soso,
        bad,
        demo
    }
    public Emotion emotion;

    private void Start()
    {
        cam = Camera.main.transform;
        money = Random.Range(0, 10);
    }

    //infoPanel.transform.LookAt(transform.position + cam.rotation * Vector3.forward, cam.rotation * Vector3.up);
    public void EmotionCheck()
    {
        if (emotionPoint < 3f)
        {
            emotion = Emotion.bad;
            current_Emotion = emotion_List[2];
        }
        else if(emotionPoint < 6f)
        {
            emotion = Emotion.soso;
            current_Emotion = emotion_List[1];
        }
        else
        {
            emotion = Emotion.good;
            current_Emotion = emotion_List[0];
        }
    }
    public void GetMoney(int _Value)
    {
        money += _Value;
    }
    public void TakeMoney(int _Cost)
    {
        if (money >= _Cost)
        {

            money -= _Cost;
            CityControlData.Instance.citizen_Tax += _Cost;
            print("시민이 세금을 냈습니다 !");
        }
        else
            print("시민이 돈이 부족합니다."); //TODO 돈 쓰는거 상황 ,구현
    }

    public void Show_INFO_Panel()
    {
        moneyText.fontSize = 0.5f;
        moneyText.text = money.ToString();
        infoPanel.SetActive(true);
        // infoPanel.transform.LookAt(transform.position + cam.rotation * Vector3.forward, cam.rotation * Vector3.up);
        if (isPanelOn == false)
            StartCoroutine(HidePanelTimer());
    }
    IEnumerator HidePanelTimer()
    {
        isPanelOn = true;
        yield return new WaitForSecondsRealtime(3f);
        isPanelOn = false;
        infoPanel.SetActive(false);
    }
}
