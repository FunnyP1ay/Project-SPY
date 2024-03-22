using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_In_House : MonoBehaviour
{
    public Transform doorPos;
    void Start()
    {
        MapData.Instance.playerHouse_InPos = doorPos.transform;
    }
}
