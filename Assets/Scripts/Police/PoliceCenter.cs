using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceCenter : MonoBehaviour
{
    public Transform policeSpawnPos;
    public Mayor mayor;
    void Start()
    {
        policeSpawnPos =  transform.Find("PoliceSpawnPos").transform;
        MapData.Instance.policeCenterPos = policeSpawnPos;
    }
}
