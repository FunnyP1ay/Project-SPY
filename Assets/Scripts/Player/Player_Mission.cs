using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player_Mission : MonoBehaviour
{
    // 어떻게 보상을 받아 올 것인가, 한 행동 함수마다 미션 중이라면 카운팅을 해야하는지  고민하기
    // 데이터를 어떻게 전달해서 미션을 카운팅 하고 갱신할지 고민하기

    [Header("Mission DATA")]
    public List<Player_MissionScritableObject>      playerMissions;
    int                                             rand;
    public string                                   now_MissionName;
    public string                                   now_MissionStory;
    public GameObject                               now_TargetObject;
    public float                                    now_ClearValue;
    public float                                    now_ClearReward;
    public float                                    current_ClearValue;
    [Header("Mission UI")]
    public GameObject                               ui_Mission_Panel;
    public TextMeshProUGUI                          ui_Mission_Name;
    public TextMeshProUGUI                          ui_Mission_Story;
    public TextMeshProUGUI                          ui_Mission_CurrentValue;
    public TextMeshProUGUI                          ui_Mission_ClearValue;
    public TextMeshProUGUI                          ui_Mission_Reward;
    private void Awake()
    {
        QuestManager.Instance.player_Mission = this;
        gameObject.SetActive(false);
    }
    void Start()
    {
       NextMission();
    }

    public void NextMission()
    {
        rand = Random.Range(0, playerMissions.Count);
        CurrentMissionSetting(playerMissions[rand]);
        current_ClearValue = 0;
    }

    public void CurrentMissionSetting(Player_MissionScritableObject _value)
    {
        this.now_MissionName    = _value.MissionName;
        this.now_MissionStory   = _value.MissionStory;
        this.now_TargetObject   = _value.TargetObject;
        this.now_ClearValue     = _value.ClearValue;
        this.now_ClearReward    = _value.ClearReward;
    }

    public void Mission_UI_Setting()
    {
        ui_Mission_Name.text            = this.now_MissionName;
        ui_Mission_Story.text           = this.now_MissionStory;
        ui_Mission_CurrentValue.text    = this.current_ClearValue.ToString();
        ui_Mission_ClearValue.text      = this.now_ClearValue.ToString();
        ui_Mission_Reward.text          = this.now_ClearReward.ToString();
    }

    public void MisstionCounting(float _value)
    {
         current_ClearValue += _value;
        if(this.current_ClearValue >this.now_ClearValue) // 미션 체크 후 달성 하면 미션 자동 교체
        {
            NextMission(); 
        }
    }
    
}