using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(BulletLife());
    }
    IEnumerator BulletLife()
    {
        yield return new WaitForSecondsRealtime(1f);
        LeanPool.Despawn(gameObject);
        yield break;
    }
}
