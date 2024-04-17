using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Jamming : MonoBehaviour
{
    public float        moveSpeed = 20f;
    public Transform    target;
    // Update is called once per frame
    void Update()
    {

        Vector3 direction = (target.position - transform.position).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out CitizenINFO citizen))
        {
            citizen.emotionPoint -= 5f;
            LeanPool.Despawn(this.gameObject);
        }
        else
        {
            LeanPool.Despawn(this.gameObject);
        }
    }
}
