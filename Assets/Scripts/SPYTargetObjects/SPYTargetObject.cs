using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPYTargetObject : MonoBehaviour
{
    public ParticleSystem           particleSystem; // VFX 로 변경 가능
    public GameObject               Fkey;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out PlayerMove player))
        {
            Fkey.SetActive(true);
            player.isBrokenAttack = true;
            player.spy_Target_Object = this;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerMove player))
        {
            Fkey.SetActive(false);
            player.isBrokenAttack = false;
            player.spy_Target_Object = null;
        }
    }
    public void OnAttack()
    {

    }
}
