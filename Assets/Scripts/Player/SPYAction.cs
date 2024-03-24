using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPYAction : MonoBehaviour
{
    private Coroutine needChageCoatCoroutine=null;
    public void BrokenObjectAttack(SPYTargetObject _target)
    {
        _target.OnAttack();
        ExposedAction(_target.exposedRange);
    }

    public void ExposedAction(float _range)
    {
        int police = LayerMask.GetMask("Police");
        Collider[] colliders = Physics.OverlapSphere(transform.position, _range, police);
        if (colliders.Length > 0)
        {
            foreach (Collider collider in colliders)
            {
                if (collider.TryGetComponent(out Police _police))
                {
                    _police.ChaseSpy(transform);
                }
            }
            ChangeCoatUI(true); // is Player Coat change UI True 
        }
    }
    public void ChangeCoat(bool _value)
    {
        //TODO Player Coat Change 실제로 구현
        print("Change Coat ! ");
        ChangeCoatUI(_value);
    }
    public void ChangeCoatUI(bool _value) // Police가 Player를 탐지 했을 때 True으로 실행함.
    {
        if (_value)
        {
            UI_Manager.Instance.ui_Player_Coat_Icon.gameObject.SetActive(true);
            UI_Manager.Instance.ui_Player_Coat_Icon.enabled = true;
            if(needChageCoatCoroutine == null)
            needChageCoatCoroutine = StartCoroutine(NeedChageCoat());
        }
        else
        {
            UI_Manager.Instance.ui_Player_Coat_Icon.gameObject.SetActive(false);
            UI_Manager.Instance.ui_Player_Coat_Icon.enabled = false;
            StopCoroutine(NeedChageCoat());
            needChageCoatCoroutine = null;
        }
    }
    public IEnumerator NeedChageCoat()
    {
        while (true)
        {
            //바로    ExposedAction(); 을 호출하면 스택오버 플로우 발생하므로 제일 조심
            yield return new WaitForSecondsRealtime(1f);
            ExposedAction(10f);
        }
    }
}
