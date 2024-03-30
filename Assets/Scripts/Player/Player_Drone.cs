using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Drone : MonoBehaviour
{
    public ParticleSystem   boom;
    public Transform        target;
    public float            moveSpeed = 5f;

    private void Update()
    {

        MoveToTargetPosition();
    }

    private void MoveToTargetPosition()
    {

        if (target != null)
        {

            Vector3 direction = (target.position - transform.position).normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
        else
        {
            LeanPool.Despawn(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Road _1) ||
            other.gameObject.TryGetComponent(out Building _2) ||
             other.gameObject.TryGetComponent(out BuildingBlock _3))
        {
            boom.Play();
        }

    }
    public void OnParticleSystemStopped()
    {
        LeanPool.Despawn(this);
    }
}
