using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Collider currentCollider = null;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Police police))
        {
            police.GetDamage(50f);
           
            LeanPool.Despawn(this.gameObject);
        }
        if (other.TryGetComponent(out Citizen citizen))
        {
            citizen.GetDamage(50f);
            LeanPool.Despawn(this.gameObject);
        }
        if(other.TryGetComponent(out Building building))
        {
            LeanPool.Despawn(this.gameObject);
        }
    }

    public void BulletLifeSetting()
    {
        StartCoroutine(BulletLife());
    }
    public IEnumerator BulletLife()
    {
        yield return new WaitForSecondsRealtime(5f);
        LeanPool.Despawn(gameObject);
        yield break;
    }
}
