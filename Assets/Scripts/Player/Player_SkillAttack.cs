using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_SkillAttack : MonoBehaviour
{
    public Player_Drone skill_Prefab;
    public Transform    skill_targetPos;
    public Transform    startPos;
    public PlayerMove   player;
    private void Start()
    {
        player = GetComponent<PlayerMove>();
    }
    private int randNum;
    public void DroneSpawn()
    {
        
        randNum = Random.Range(20, 50);
        var drone = LeanPool.Spawn(skill_Prefab);
        drone.gameObject.transform.position = new Vector3(startPos.position.x + randNum, startPos.position.y + 30f, startPos.position.z + randNum);
        drone.target = skill_targetPos;
        drone.transform.LookAt(skill_targetPos);
        drone.player = this.player;
    }
   
}
