using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SPYTargetObject : MonoBehaviour
{
    public float                    exposedRange;
    public VisualEffect             AttackEffect;
    public bool                     isBroken =false;

    public enum TargetDATA
    {
        Electricity,
        ATM,

    }
    public TargetDATA targetData;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out PlayerMove player))
        {
            UI_Manager.Instance.ui_Key_Icon_Action.F_Key_SetActive_True();
            player.isBrokenAttack = true;
            player.spy_Target_Object = this;
        }
        if(isBroken && other.gameObject.TryGetComponent(out Citizen citizen))
        {
            citizen.Question_MarkSet();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerMove player))
        {
            UI_Manager.Instance.ui_Key_Icon_Action.F_Key_SetActive_False();
            player.isBrokenAttack = false;
            player.spy_Target_Object = null;
        }
    }
    public void OnAttack()
    {
        AttackEffect.gameObject.SetActive(true);
        isBroken = true;
        AttackEffect.Play();
        MissionCount();


    }
    public void Repair()
    {
        AttackEffect.gameObject.SetActive(false);
        isBroken = false;
        AttackEffect.Stop();
    }
    public void MissionCount()
    {
        switch (targetData)
        {
            case TargetDATA.Electricity:
                QuestManager.Instance.player_Mission.mission_Electricity++;
                break;
            case TargetDATA.ATM:
                QuestManager.Instance.player_Mission.mission_ATM++;
                break;
        }

        QuestManager.Instance.player_Mission.MissionCounting();
    }

}
