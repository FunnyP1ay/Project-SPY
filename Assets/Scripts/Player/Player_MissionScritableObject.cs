using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MissionDATA", menuName = "Scripts Objectable/MisstionDATA", order = int.MaxValue)]
public class Player_MissionScritableObject : MonoBehaviour
{
    [SerializeField]
    private string              missionName;
    public string               MissionName { get { return missionName; } set { missionName = value; } }
    [SerializeField]
    private SPYTargetObject     targetObject;
    public SPYTargetObject      TargetObject { get { return targetObject; } set { targetObject = value; } }
    [SerializeField]
    private float               clearValue;
    public float                ClearValue { get { return clearValue; } set { clearValue = value; } }
    [SerializeField]
    private float               clearReward;
    public float                ClearReward { get { return clearReward; } set { clearReward = value; } }
}
