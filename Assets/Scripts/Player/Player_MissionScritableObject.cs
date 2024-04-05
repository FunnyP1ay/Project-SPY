using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MissionDATA", menuName = "Scripts Objectable/MisstionDATA", order = int.MaxValue)]

public class Player_MissionScritableObject : ScriptableObject
{

    public enum Mission_Target
    {
        Police,
        Citizen,
        Electricity,
        ATM
    }
    [SerializeField]
    private string              missionName;
    public string               MissionName { get { return missionName; } set { missionName = value; } }
    [SerializeField]
    private string              missionStory;
    public string               MissionStory { get { return missionStory; } set { missionStory = value; } }
    [SerializeField]
    public Mission_Target       mission_Target;
    [SerializeField]
    private float               clearValue;
    public float                ClearValue { get { return clearValue; } set { clearValue = value; } }
    [SerializeField]
    private float               clearReward;
    public float                ClearReward { get { return clearReward; } set { clearReward = value; } }
}
