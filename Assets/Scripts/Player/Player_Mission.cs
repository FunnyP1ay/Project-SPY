using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Mission : MonoBehaviour
{
    // 어떻게 보상을 받아 올 것인가, 한 행동 함수마다 미션 중이라면 카운팅을 해야하는지  고민하기
    // 데이터를 어떻게 전달해서 미션을 카운팅 하고 갱신할지 고민하기
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
        // 아마 싱글톤으로 받아 와야 할 것 같음
    }
    
}