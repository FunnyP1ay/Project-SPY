using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public IEnumerator BulletLife()
    {
        yield return new WaitForSecondsRealtime(5f);
        LeanPool.Despawn(gameObject);
        yield break;
    }
}
