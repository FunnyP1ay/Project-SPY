using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player_Mission : MonoBehaviour
{
    // ��� ������ �޾� �� ���ΰ�, �� �ൿ �Լ����� �̼� ���̶�� ī������ �ؾ��ϴ���  ����ϱ�
    // �����͸� ��� �����ؼ� �̼��� ī���� �ϰ� �������� ����ϱ�

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
        if(this.current_ClearValue >this.now_ClearValue) // �̼� üũ �� �޼� �ϸ� �̼� �ڵ� ��ü
        {
            NextMission(); 
        }
    }
    
}