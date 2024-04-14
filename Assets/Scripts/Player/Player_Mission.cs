using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player_Mission : MonoBehaviour
{
    // ��� ������ �޾� �� ���ΰ�, �� �ൿ �Լ����� �̼� ���̶�� ī������ �ؾ��ϴ���  ����ϱ�
    // �����͸� ��� �����ؼ� �̼��� ī���� �ϰ� �������� ����ϱ�

    [Header("Mission DATA")]
    public List<Player_MissionScritableObject>  playerMissions;
    public Player_MissionScritableObject        current_Mission;
    int                                         rand;
    public string                               missionName;
    public string                               missionStory;
    public float                                clearValue;
    public float                                clearReward;
    public float                                now_ClearValue;
    [Header("Mission UI")]
    public GameObject                           ui_Mission_Panel;
    public TextMeshProUGUI                      ui_Mission_Name;
    public TextMeshProUGUI                      ui_Mission_Story;
    public TextMeshProUGUI                      ui_Mission_CurrentValue;
    public TextMeshProUGUI                      ui_Mission_ClearValue;
    public TextMeshProUGUI                      ui_Mission_Reward;

    //-------------------------- Count Value -----------------------------
    
    public int                                  mission_Police;
    public int                                  mission_Citizen;
    public int                                  mission_Electricity;
    public int                                  mission_ATM;

    private void Awake()
    {
        QuestManager.Instance.player_Mission = this;
        NextMission();
        gameObject.SetActive(false);
    }

    public void NextMission()
    {
        rand = Random.Range(0, playerMissions.Count);
        CurrentMissionSetting(playerMissions[rand]);
        current_Mission = playerMissions[rand];
        now_ClearValue = 0;
    }

    public void CurrentMissionSetting(Player_MissionScritableObject _value)
    {
        this.missionName = _value.MissionName;
        this.missionStory = _value.MissionStory;
        this.clearValue = _value.ClearValue;
        this.clearReward = _value.ClearReward;
    }

    public void Mission_UI_Setting()
    {
        ui_Mission_Name.text = this.missionName;
        ui_Mission_Story.text = this.missionStory;
        ui_Mission_CurrentValue.text = this.now_ClearValue.ToString();
        ui_Mission_ClearValue.text = this.clearValue.ToString();
        ui_Mission_Reward.text = this.clearReward.ToString();
    }
    public void MissionCounting()
    {
        switch (current_Mission.mission_Target)
        {
            case Player_MissionScritableObject.Mission_Target.Police:
                now_ClearValue = mission_Police;
                break;
            case Player_MissionScritableObject.Mission_Target.Citizen:
                now_ClearValue = mission_Citizen;
                break;
            case Player_MissionScritableObject.Mission_Target.Electricity:
                now_ClearValue = mission_Electricity;
                break;
            case Player_MissionScritableObject.Mission_Target.ATM:
                now_ClearValue = mission_ATM;
                break;
        }
        CountCheck();
    }
    public void CountCheck()
    {
        if (this.now_ClearValue >= this.clearValue) // �̼� üũ �� �޼� �ϸ� �̼� �ڵ� ��ü
        {
            NextMission();
            GetQuestReward();
            QuestReset();
        }
    }
    public void QuestReset()
    {
        mission_Police = 0;
        mission_Citizen = 0;
        mission_Electricity = 0;
        mission_ATM = 0;
    }
    public void GetQuestReward()
    {
        rand = Random.Range(0, 3);
        switch (rand)
        {
            case 0:
                CityControlData.Instance.approval_Rating    -= 0.1f * clearReward;
                QuestManager.Instance.missionClearText.text = $"�������� {0.1f * clearReward} ��ŭ ����Ʈ�Ƚ��ϴ�.";
                print("�̼Ǽ������� �������� ����Ʈ�Ƚ��ϴ� ! ");
                break;
            case 1:
                CityControlData.Instance.safety_Rating      -= 0.1f * clearReward;
                QuestManager.Instance.missionClearText.text = $"ġ������ {0.1f * clearReward} ��ŭ ����Ʈ�Ƚ��ϴ�.";
                print("�̼Ǽ������� ġ������ ����Ʈ�Ƚ��ϴ� ! ");
                break;
            case 2:
                CityControlData.Instance.building_Tax       -= 100*(int)clearReward;
                QuestManager.Instance.missionClearText.text = $"���� ������ {10 * (int)clearReward} ��ŭ ���ƽ��ϴ�.";
                print("�̼Ǽ������� ���ü����� ���ƽ��ϴ� ! ");
                break;
            case 3:
                CityControlData.Instance.citizen_Tax        -= 100*(int)clearReward;
                QuestManager.Instance.missionClearText.text = $"�ù� ������ {10 * (int)clearReward} ��ŭ ���ƽ��ϴ�.";
                print("�̼Ǽ������� �ùμ����� ���ƽ��ϴ� ! ");
                break;
            default:
                break;
        }
        if(!QuestManager.Instance.missionClearPanel.activeSelf)
        StartCoroutine(closePanel());
    }
    IEnumerator closePanel()
    {
        UI_Manager.Instance.PopUp(QuestManager.Instance.missionClearPanel, false);
        yield return new WaitForSecondsRealtime(1.5f);
        UI_Manager.Instance.PopUp(QuestManager.Instance.missionClearPanel, true);
    }
}