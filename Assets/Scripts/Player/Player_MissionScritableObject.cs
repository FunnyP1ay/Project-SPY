using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MissionDATA", menuName = "Scripts Objectable/MisstionDATA", order = int.MaxValue)]
public class Player_MissionScritableObject : ScriptableObject
{
    [SerializeField]
    private string              missionName;
    public string               MissionName { get { return missionName; } set { missionName = value; } }
    [SerializeField]
    private string              missionStory;
    public string               MissionStory { get { return missionStory; } set { missionStory = value; } }
    [SerializeField]
    private GameObject          targetObject;
    public GameObject           TargetObject { get { return targetObject; } set { targetObject = value; } }
    [SerializeField]
    private float               clearValue;
    public float                ClearValue { get { return clearValue; } set { clearValue = value; } }
    [SerializeField]
    private float               clearReward;
    public float                ClearReward { get { return clearReward; } set { clearReward = value; } }
}
