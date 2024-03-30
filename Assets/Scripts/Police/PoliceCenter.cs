using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceCenter : MonoBehaviour
{
    public Transform policeSpawnPos;
    void Start()
    {
        MapData.Instance.policeCenterPos = policeSpawnPos;
    }
}
