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
    private int randNum;
    public void DroneSpawn()
    {
        
        randNum = Random.Range(0, 5);
        var drone = LeanPool.Spawn(skill_Prefab);
        drone.gameObject.transform.position = new Vector3(startPos.position.x + randNum, startPos.position.y + 20f, startPos.position.z + randNum);
        drone.target = skill_targetPos;
    }
   
}
