using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPYTargetObject : MonoBehaviour
{
    public GameObject   Fkey;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out PlayerMove player))
        {
            Fkey.SetActive(true);
            player.isBrokenAttack = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerMove player))
        {
            Fkey.SetActive(false);
            player.isBrokenAttack = false;
        }
    }
    public void OnAttack()
    {

    }
}
