using DG.Tweening;
using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_SkillAttack : MonoBehaviour
{
    public Player_Drone     skill_Prefab;
    public Player_Jamming   jamming_Prefab;

    public Transform    skill_targetPos;
    public Transform    startPos;
    public PlayerMove   player;
    public Camera       mainCam;
    private void Start()
    {
        player = GetComponent<PlayerMove>();
    }
    private int randNum;
    public void DroneSpawn()
    {
        
        randNum = Random.Range(20, 25);
        var drone = LeanPool.Spawn(skill_Prefab);
        drone.gameObject.transform.position = new Vector3(startPos.position.x + randNum, startPos.position.y + 30f, startPos.position.z + randNum);
        drone.target = skill_targetPos;
        //drone.transform.LookAt(skill_targetPos);
        drone.player = this.player;
    }

    public void JammingAttack()
    {
        randNum = Random.Range(-5, 6);
        var jamming = LeanPool.Spawn(jamming_Prefab);
        jamming.target = skill_targetPos;
        jamming.transform.position = new Vector3(skill_targetPos.position.x + randNum, skill_targetPos.position.y + 20f, skill_targetPos.position.z + randNum);


    }

}
