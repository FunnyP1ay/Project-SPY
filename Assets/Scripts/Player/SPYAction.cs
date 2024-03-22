using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPYAction : MonoBehaviour
{
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
        }
    }
    public void ChangeCoat()
    {

    }
    public void ChangeCoatUI(bool _value)
    {
        if(_value)
            UI_Manager.Instance.ui_Player_Coat_Icon.enabled = true;
        else
            UI_Manager.Instance.ui_Player_Coat_Icon.enabled = false;
    }
}
