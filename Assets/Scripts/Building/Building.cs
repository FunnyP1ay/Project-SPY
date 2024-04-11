using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public float                HP = 100f;
    public Transform            building_NavTargetPoint;
    public void Setting()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.name == "NavTarget")
                building_NavTargetPoint = child;
        }
    }

    public void GetDamage(float _damage)
    {
        HP -= _damage;
        if(HP <= 0)
        {
            print("�ǹ��� �μ������ϴ� ! ");
            LeanPool.Despawn(this.gameObject);
            // �ǹ� �μ����°� ���� �ϱ�
        }
    }
}
