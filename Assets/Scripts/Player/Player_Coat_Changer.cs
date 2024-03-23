using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Coat_Changer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerMove _player))
        {
            _player.isCoatChange = true;
            UI_Manager.Instance.ui_Key_Icon_Action.F_Key_SetActive_True();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent(out PlayerMove _player))
        {
            _player.isCoatChange = false;
            UI_Manager.Instance.ui_Key_Icon_Action.F_Key_SetActive_False();
        }
    }
}
