using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Citizen : MonoBehaviour
{
    [SerializeField]
    Transform                   navTarget;
    public NavMeshAgent         nav;
    CitizenINFO                 citizenINFO;
    Citizen_INOUT_Control       citizen_INOUT_Control;
    public GameObject           question_Mark;
    public GameObject           surprised_Mark;
    private int                 randNum;
    private int                 randNum2;
    private int                 randNum3;
    private float               checkDistance;
    public string               citizenName;
    public float                currentHP = 10f;
    public List<GameObject>     prefab_List;
    public GameObject           currentPrefab;
    public Animator             animator;
    public bool                 isQuestion_MarkOn = false;
    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        citizenINFO = GetComponent<CitizenINFO>();
        animator = GetComponent<Animator>();
        citizen_INOUT_Control = GetComponent<Citizen_INOUT_Control>();
    }

    private void OnEnable()
    {
        nav = GetComponent<NavMeshAgent>();
        currentPrefab.SetActive(false);
        randNum = Random.Range(0, prefab_List.Count);
        currentPrefab = prefab_List[randNum];
        currentPrefab.SetActive(true);
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
        InBuilding,
        None
    }
    public MoveResult moveResult = MoveResult.None; 

    public void CitizenCoroutineSetting() // 시민스포너랑 건물안에서 켜질 때, 밖으로 나올 때 사용 해야함
    {
        StartCoroutine(MoveCoroutine()); 
    }
    public IEnumerator MoveCoroutine()
    {
        while (state != State.die)
        {
            animator.SetFloat("isMove", nav.speed);
   
            if (state == State.Move) // 한번 돌때마다 MoveResult 에 따른 타겟확인을 함
            {
                CheckTargetPos();
            }
            if (state == State.Run)
            {
                RunAway();
            }
            if (state == State.needNextMove) // MoveResult과 navtarget을 설정해주고 State를 무조건 move로 바꿈
            {
                SetNextMoveTarget();
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
            case MoveResult.InBuilding:
                Check_Get_In_Building_Move_Pos();
                break;
            case MoveResult.None:
                CheckRoadTargetPos();
                break;
        }
        state = State.needNextMove;
        animator.SetFloat("isMove", nav.speed);
    }

    private void CheckRoadTargetPos()
    { 
        if (navTarget == null)
        {
            state = State.needNextMove;
            return;
        }
        checkDistance = Vector3.Distance(gameObject.transform.position, navTarget.transform.position);
        if (checkDistance < 3f)
        {
            state = State.needNextMove;
        }

    }
    private void CheckBuildingTargetPos() // Money Cal
    {
        checkDistance = Vector3.Distance(gameObject.transform.position, navTarget.transform.position);
        if (checkDistance < 1.2f)
        {
            switch (moveResult)
            {
                case MoveResult.GetMoney:
                    citizenINFO.GetMoney(1);
                    citizen_INOUT_Control.GetInBuilding();
                    break;
                case MoveResult.TakeMoney: 
                    citizenINFO.TakeMoney(1);
                    citizen_INOUT_Control.GetInBuilding();
                    break;
                case MoveResult.NoneMoney:
                    citizen_INOUT_Control.GetInBuilding();
                    break;
                    default:
                    break;
            }
        }
      
    }

    private void Check_Get_In_Building_Move_Pos()
    {
        checkDistance = Vector3.Distance(gameObject.transform.position, navTarget.transform.position);
        if (checkDistance < 2.5f)
        {
         
            InBuildingTargetSetting(); 
        }
    }
    //------------------Move Target Setting -----------------
    private void SetNextMoveTarget()
    {

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
        animator.SetFloat("isMove", nav.speed);
    }
    private void SetNavTarget_Building()
    {
        int BuildingLayerMask = LayerMask.GetMask("Building");
        Collider[] colliders = Physics.OverlapSphere(transform.position, 25f, BuildingLayerMask); //, BuildingLayerMask
        if (colliders.Length > 0)
        {
            randNum = Random.Range(0, colliders.Length);

            if (colliders[randNum].gameObject.TryGetComponent(out Building _building))
            {
                    navTarget = _building.building_NavTargetPoint;
                    nav.SetDestination(navTarget.position);
                state = State.Move;
            }
        }
        else
        {
            SetNavTarget_Road();
        }
    }
    private void SetNavTarget_Road()
    {
        int roadLayerMask = LayerMask.GetMask("Road");
        Collider[] colliders = Physics.OverlapSphere(transform.position, 30f, roadLayerMask);
        if(colliders.Length > 0)
        {
            randNum = Random.Range(0, colliders.Length);
            if (colliders[randNum].gameObject.TryGetComponent(out Road _road)) 
            {
                randNum = Random.Range(0, _road.navTargetPos_List.Count);
                navTarget = _road.navTargetPos_List[randNum].transform;
                nav.SetDestination(navTarget.position);
                state = State.Move;
            }
        }
        else
        {
            navTarget = MapData.Instance.NavMesh_Target_Bug_Fix_Pos;
            nav.SetDestination(navTarget.position);
        }


    }

    public void RunAway()
    {
        int spyLayerMask = LayerMask.GetMask("SPY");
        Collider[] colliders = Physics.OverlapSphere(transform.position, 35f, spyLayerMask);
        if (colliders.Length == 0)
        {
            state = State.needNextMove;
            nav.speed = 0.1f;
            surprised_Mark.SetActive(false);
        }
        else
        {
            checkDistance = Vector3.Distance(gameObject.transform.position, navTarget.transform.position);

            SetNavTarget_Road();

            state = State.Run;
            nav.speed = 7.5f;
            animator.SetFloat("isMove", nav.speed);
            surprised_Mark.SetActive(true);
            CityControlData.Instance.safety_Rating -= 0.01f;
        }
      
    }
    
    public void InBuildingSetting()
    {
        nav.enabled = true;
        state = State.Move;
        nav.speed = 3f;
        InBuildingTargetSetting();
        animator.SetFloat("isMove", nav.speed);
        CitizenCoroutineSetting();
    }

    public void InBuildingTargetSetting()
    {

        int InmovePos = LayerMask.GetMask("InBuildingMovePos");
        Collider[] colliders = Physics.OverlapSphere(transform.position, 20f, InmovePos);
        randNum = Random.Range(0, colliders.Length);
        navTarget = colliders[randNum].transform;
        moveResult = MoveResult.InBuilding;
        nav.SetDestination(navTarget.position);
        nav.enabled = true;
        nav.updatePosition = true;
    }
    public void OutBuildingSetting()
    {
        state = State.needNextMove;

        CitizenCoroutineSetting();
    }
    public void Question_MarkSet()
    {
        if (isQuestion_MarkOn == false)
        {
            StartCoroutine(Question_MarkTimer());
        }
    }
    private IEnumerator Question_MarkTimer()
    {
        isQuestion_MarkOn = true;
        question_Mark.SetActive(true);
        yield return new WaitForSecondsRealtime(3f);
        isQuestion_MarkOn = false;
        question_Mark.SetActive(false);
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
        if (currentHP <= 0)
        {
            // 죽을 때 애니매이션 으로 실행시키기
            animator.SetTrigger("isDie");
        }
    }

    public void Die()
    {
        print("시민이 사망했습니다 ! ");
        currentPrefab.SetActive(false);
        CityControlData.Instance.safety_Rating -= 0.1f;
        MapData.Instance.currentCitizenCount--;
        LeanPool.Despawn(this);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out DrivingCar _car))
        {
            animator.SetTrigger("isKnockback");
       
        }
    }
}
