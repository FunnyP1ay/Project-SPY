using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using static UnityEngine.ParticleSystem;

public class Player_Drone : MonoBehaviour
{
    public ParticleSystem   boom;
    public CinemachineVirtualCamera droneCam;
    public Transform        target;
    public PlayerMove       player;
    public float            moveSpeed = 5f;



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


            Vector3 direction = (target.position - transform.position).normalized;
                transform.Translate(direction * moveSpeed * Time.deltaTime,Space.World);

            gameObject.transform.LookAt(target.position);
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
            StartCoroutine(Boom());
        }
        StartCoroutine(Boom());
    }

    IEnumerator Boom()
    {
        var _boom = LeanPool.Spawn(boom,transform);
        _boom.Play();
        CityControlData.Instance.safety_Rating -= 0.1f;
        yield return new WaitForSecondsRealtime(0.2f);
        droneCam.Priority = 0;
        player.isDroneAttack = false;
        //player.player_Cinemachine_Control.playercamera.isZoom = false;
        LeanPool.Despawn(this);
        print("드론이 목표와 접촉했습니다 !");
    }

}
