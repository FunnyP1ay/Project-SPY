using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Mission : MonoBehaviour
{
    // ��� ������ �޾� �� ���ΰ�, �� �ൿ �Լ����� �̼� ���̶�� ī������ �ؾ��ϴ���  ����ϱ�
    // �����͸� ��� �����ؼ� �̼��� ī���� �ϰ� �������� ����ϱ�
    List<Player_MissionScritableObject>     playerMisstions;
    int                                     rand;
    public string                          missionName;
    public SPYTargetObject                 targetObject;
    public float                           clearValue;
    public float                           clearReward;

    PlayerMove player;

    private void Awake()
    {
        player = GetComponent<PlayerMove>();
    }
    void Start()
    {
       NextMission();
    }

    public void NextMission()
    {
        rand = Random.Range(0, playerMisstions.Count);
        CurrentMisstionSetting(playerMisstions[rand]);
    }

    public void CurrentMisstionSetting(Player_MissionScritableObject _value)
    {
        this.missionName = _value.MissionName;
        this.targetObject = _value.TargetObject;
        this.clearValue = _value.ClearValue;
        this.clearReward = _value.ClearReward;
    }

    public void MisstionCounting()
    {
        // �Ƹ� �̱������� �޾� �;� �� �� ����
    }
    
}