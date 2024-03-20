using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBugFix : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out Citizen citizen))
        {
            citizen.state = Citizen.State.needNextMove;
        }
        else if (other.gameObject.TryGetComponent(out Police police))
        {
            police.moveState = Police.MoveState.needNextMove;
        }
    }
}
