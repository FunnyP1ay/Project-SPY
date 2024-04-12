using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using static UnityEngine.ParticleSystem;
using TMPro;

public class Player_Drone : MonoBehaviour
{
    public ParticleSystem       boom;
    public CinemachineFreeLook  droneCam;
    public Transform            target;
    public PlayerMove           player;
    public LayerMask            layerMask;
    public float                moveSpeed = 5f;

     

    private void OnEnable()
    {
        droneCam.Priority = 20;
    }
    private void Update()
    {

        MoveToTargetPosition();
    }
    
    private void MoveToTargetPosition()
    {

        if (target != null)
        {
                target.localRotation = Quaternion.identity;

                // 대상을 바라보는 회전을 계산합니다.
                Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);

                // 회전 속도를 기반으로 현재 방향에서 목표 방향까지 회전합니다.
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 60f * Time.deltaTime);


                Vector3 direction = (target.position - transform.position).normalized;
                transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);

            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Road _1) ||
            other.gameObject.TryGetComponent(out Building _2) ||
             other.gameObject.TryGetComponent(out BuildingBlock _3))
        {
            StartCoroutine(Boom());
        }
        StartCoroutine(Boom());
    }

    IEnumerator Boom()
    {
        var _boom = LeanPool.Spawn(boom,transform);
        _boom.Play();
        BoomDamage();
        CityControlData.Instance.safety_Rating -= 0.3f;
        yield return new WaitForSecondsRealtime(0.2f);
        droneCam.Priority = 0;
        player.isDroneAttack = false;
        LeanPool.Despawn(this.gameObject);
    }

    private void BoomDamage()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 5f, layerMask);
        {
            if(colliders.Length > 0)
            {
                foreach (Collider collider in colliders)
                {
                    if (collider.TryGetComponent(out Citizen citizen))
                    {
                        citizen.GetDamage(50f);
                    }
                    if (collider.TryGetComponent(out Police police))
                    {
                        police.GetDamage(50f);
                    }
                    if (collider.TryGetComponent(out Building building))
                    {
                        building.GetDamage(50f);
                    }
                }
            }
        }
    }
}
