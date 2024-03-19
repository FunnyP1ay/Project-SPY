using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFire : MonoBehaviour
{
    public GameObject   bullet;
    public Transform    firePos;
    public float        bulletSpeed   = 10; 

    void Start()
    {
    }

    public void Fire()
    {
        var _bullet =  LeanPool.Spawn(bullet);
        _bullet.transform.position = firePos.position;
        _bullet.transform.rotation = firePos.rotation;
        Rigidbody rb = _bullet.GetComponent<Rigidbody>();
        rb.velocity = firePos.forward * bulletSpeed;
    }
}
