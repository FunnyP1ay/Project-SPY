using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static Citizen;
using static Police;

public class Citizen : MonoBehaviour
{
    [SerializeField]
    Transform                   navTarget;
    public NavMeshAgent         nav;
    CitizenINFO                 citizenINFO;
    public GameObject           question_Mark;
    public GameObject           surprised_Mark;
    private int                 randNum;
    private int                 randNum2;
    private int                 randNum3;
    private float               checkDistance;
    public string               citizenName;
    public float                currentHP = 100f;
    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        citizenINFO = GetComponent<CitizenINFO>();
    }

    public enum State
    {
        die,
        needNextMove,
        Move,
        Run
    }
    public State state;

    public enum MoveResult
    {
        GetMoney,
        TakeMoney,
        NoneMoney,
        None
    }
    public MoveResult moveResult; 

    public IEnumerator MoveCoroutine()
    {
        while (state != State.die)
        {
            if (state == State.needNextMove)
            {
                SetNextMoveTarget();
            }
            if (state == State.Move)
            {
                CheckTargetPos();
            }
            if (state == State.Run)
            {
                RunAway();
            }
            yield return new WaitForSecondsRealtime(3f);
        }
        yield break;
    }
    // --------- Check Target Position -------------
    private void CheckTargetPos()
    {
        switch (moveResult)
        {
            case MoveResult.GetMoney:
                CheckBuildingTargetPos();
                break;
            case MoveResult.TakeMoney:
                CheckBuildingTargetPos();
                break;
            case MoveResult.NoneMoney:
                CheckBuildingTargetPos();
                break;
            case MoveResult.None:
                CheckRoadTargetPos();
                break;
        }
    }

    private void CheckRoadTargetPos()
    {
        checkDistance = Vector3.Distance(gameObject.transform.position, navTarget.transform.position);
        if (checkDistance < 4f)
        {
            nav.updatePosition = false;
            state = State.needNextMove;
        }

    }
    private void CheckBuildingTargetPos() // Money Cal
    {
        checkDistance = Vector3.Distance(gameObject.transform.position, navTarget.transform.position);
        if (checkDistance < 4f)
        {
            switch (moveResult)
            {
                case MoveResult.GetMoney:
                    citizenINFO.GetMoney(1);
                    break;
                case MoveResult.TakeMoney: 
                    citizenINFO.TakeMoney(1);
                    break;
                case MoveResult.NoneMoney: 
                    break;
            }
            
            // TODO 시민을 빌딩 안으로 이동 시키는 거 구현하기 
            nav.updatePosition = false;
            state = State.needNextMove;
        }

    }
    //------------------Move Target Setting -----------------
    private void SetNextMoveTarget()
    {
        // next move target value setting
        randNum = Random.Range(0, 10);
        switch (randNum)
        {
            case 0:
                moveResult = MoveResult.GetMoney;
                SetNavTarget_Building(); // GetMoney
                break;
            case 1:
                moveResult = MoveResult.TakeMoney;
                SetNavTarget_Building(); // TakeMoney
                break;
            case 2:
                moveResult = MoveResult.NoneMoney;
                SetNavTarget_Building(); // NoneMoney
                break;
            default:
                moveResult = MoveResult.None;
                SetNavTarget_Road();      // Road : None
                break;
        }
    }
    private void SetNavTarget_Building()
    {
        int BuildingLayerMask = LayerMask.GetMask("Building");
        Collider[] colliders = Physics.OverlapSphere(transform.position, 20f, BuildingLayerMask);
        
        if(colliders.Length == 0)
        {
            SetNavTarget_Road();
        }
        else
        {
            randNum = Random.Range(0, colliders.Length);
            if (colliders[randNum].gameObject.TryGetComponent(out Building _building))
            {
                navTarget = _building.building_NavTargetPoint;
                nav.SetDestination(navTarget.position);
                nav.updatePosition = true;
                state = State.Move;
            }
        }
    }
    private void SetNavTarget_Road()
    {
        int roadLayerMask = LayerMask.GetMask("Road");
        Collider[] colliders = Physics.OverlapSphere(transform.position, 20f, roadLayerMask);
        randNum = Random.Range(0, colliders.Length);
        if (colliders.Length == 0)
        {
            nav.SetDestination(MapData.Instance.empty_Building_Block_List[0].transform.position);
            nav.updatePosition = true;
            state = State.Move;
            moveResult = MoveResult.None;
        }
        else
        {
            if (colliders[randNum].gameObject.TryGetComponent(out Road _road))
            {
                randNum = Random.Range(0, _road.navTargetPos_List.Count);
                navTarget = _road.navTargetPos_List[randNum].transform;
                nav.SetDestination(navTarget.position);
                nav.updatePosition = true;
                state = State.Move;
                moveResult = MoveResult.None;
            }
        }

    }

    public void RunAway()
    {
        int spyLayerMask = LayerMask.GetMask("SPY");
        Collider[] colliders = Physics.OverlapSphere(transform.position, 35f, spyLayerMask);
        if (colliders.Length == 0)
        {
            state = State.needNextMove;
            surprised_Mark.SetActive(false);
        }
        else
        {
            state = State.Run;
            nav.speed = 7;
            surprised_Mark.SetActive(true);
        }
      
    }
    private void CrossTheCrosswalk()
    {

    }
    private void TrafficTarget()
    {

    }
    public void SetName()
    {
        randNum = Random.Range(0, GameDB.Instance.nameChar.Count);
        randNum2 = Random.Range(0, GameDB.Instance.nameChar_2.Count);
        randNum3 = Random.Range(0, GameDB.Instance.nameChar_2.Count);
        citizenName = GameDB.Instance.nameChar[randNum].ToString() +
                        GameDB.Instance.nameChar_2[randNum2].ToString() +
                            GameDB.Instance.nameChar_2[randNum3].ToString();

        citizenINFO.nameText.text = citizenName;
        citizenINFO.nameText.fontSize = 0.5f;
    }
    public void GetDamage(float _damage)
    {
        print("데미지를 입었습니다 ! ");
        RunAway();
        currentHP -= _damage;
        if (currentHP < 0)
        {
            Die();
        }
    }

    public void Die()
    {
        // 죽을 때 애니매이션 
        MapData.Instance.currentCitizenCount--;
        LeanPool.Despawn(this);
    }

}
