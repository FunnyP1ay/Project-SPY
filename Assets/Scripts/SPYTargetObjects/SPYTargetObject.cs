using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SPYTargetObject : MonoBehaviour
{
    public float                    exposedRange;
    public VisualEffect             AttackEffect;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out PlayerMove player))
        {
            UI_Manager.Instance.ui_Key_Icon_Action.F_Key_SetActive_True();
            player.isBrokenAttack = true;
            player.spy_Target_Object = this;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerMove player))
        {
            UI_Manager.Instance.ui_Key_Icon_Action.F_key_SetActive_False();
            player.isBrokenAttack = false;
            player.spy_Target_Object = null;
        }
    }
    public void OnAttack()
    {
        AttackEffect.gameObject.SetActive(true);
        AttackEffect.Play();
    }
    public void Repair()
    {
        AttackEffect.gameObject.SetActive(false);
        AttackEffect.Stop();
    }
}
