using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_SkillAtack : MonoBehaviour
{

    public ParticleSystem boom; // ��ƼŬ �ý��� ����

    public Transform target; // �̵��� ��ǥ ����
    public float moveSpeed = 5f; // �̵� �ӵ�

    private void Update()
    {
        // ��ǥ �������� �̵��ϴ� �Լ� ȣ��
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
