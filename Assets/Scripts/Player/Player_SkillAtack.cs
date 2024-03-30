using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_SkillAtack : MonoBehaviour
{

    public ParticleSystem boom; // 파티클 시스템 참조

    public Transform target; // 이동할 목표 지점
    public float moveSpeed = 5f; // 이동 속도

    private void Update()
    {
        // 목표 지점까지 이동하는 함수 호출
        MoveToTargetPosition();
    }

    private void MoveToTargetPosition()
    {
      
        if (target != null)
        {
          
            Vector3 direction = (target.position - transform.position).normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out Road _1)||
            other.gameObject.TryGetComponent(out Building _2) ||
             other.gameObject.TryGetComponent(out BuildingBlock _3))
        {
            boom.Play();
        }

    }
    public void OnParticleSystemStopped()
    {

    }
}
