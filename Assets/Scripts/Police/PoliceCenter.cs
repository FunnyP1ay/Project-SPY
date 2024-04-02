using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceCenter : MonoBehaviour
{
    private Transform policeSpawnPos;
    void Start()
    {
        policeSpawnPos = transform.Find("Police Spawn Pos").transform;
        MapData.Instance.policeCenterPos = policeSpawnPos;
    }
}
