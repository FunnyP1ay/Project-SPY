using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Mission : MonoBehaviour
{
    // ��� ������ �޾� �� ���ΰ�, �� �ൿ �Լ����� �̼� ���̶�� ī������ �ؾ��ϴ���  ����ϱ�
    // �����͸� ��� �����ؼ� �̼��� ī���� �ϰ� �������� ����ϱ�
    List<Player_MissionScritableObject>     playerMisstions;
    int                                     rand;
    public string                           now_MissionName;
    public SPYTargetObject                  now_TargetObject;
    public float                            now_ClearValue;
    public float                            now_ClearReward;
    public float                            current_ClearValue;
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
        current_ClearValue = 0;
    }

    public void CurrentMisstionSetting(Player_MissionScritableObject _value)
    {
        this.now_MissionName = _value.MissionName;
        this.now_TargetObject = _value.TargetObject;
        this.now_ClearValue = _value.ClearValue;
        this.now_ClearReward = _value.ClearReward;
    }

    public void MisstionCounting(float _value)
    {
         current_ClearValue += _value;
        if(this.current_ClearValue >this.now_ClearValue)
        {
            // now_ClearReward �� �÷��̾����� ��� �ٰ��� ������ �������� �ٲ��� ��ȹ �ϱ�
        }
    }
    
}