using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class In_FruitsStore : MonoBehaviour
{
    public Transform doorPos;
    private void Start()
    {
        MapData.Instance.playerFruitsStore_InPos = doorPos.transform;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerMove player))
        {
            UI_Manager.Instance.ui_Key_Icon_Action.F_Key_SetActive_True();
            player.isGetIn = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerMove player))
        {
            UI_Manager.Instance.ui_Key_Icon_Action.F_Key_SetActive_False();
            player.isGetIn = false;
        }
    }
}
