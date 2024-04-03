using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Mission : MonoBehaviour
{
    // 어떻게 보상을 받아 올 것인가, 한 행동 함수마다 미션 중이라면 카운팅을 해야하는지  고민하기
    // 데이터를 어떻게 전달해서 미션을 카운팅 하고 갱신할지 고민하기
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
            // now_ClearReward 를 플레이어한테 어떻게 줄건지 보상이 무엇으로 줄껀지 기획 하기
        }
    }
    
}