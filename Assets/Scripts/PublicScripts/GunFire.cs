using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFire : MonoBehaviour
{
    public Bullet       bullet;
    public Transform    firePos;
    public float        bulletSpeed   = 100f; 



    public void Fire()
    {
        var _bullet =  LeanPool.Spawn(bullet);
        _bullet.transform.position = firePos.position;
        _bullet.transform.rotation = firePos.rotation;
        _bullet.StartCoroutine(_bullet.BulletLife());
        Rigidbody rb = _bullet.GetComponent<Rigidbody>();
        rb.velocity = firePos.forward * bulletSpeed;
    }
}
