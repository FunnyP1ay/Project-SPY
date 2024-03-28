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
    public float                currentHP = 100f;
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
    public MoveResult moveResult; 

    public void CitizenCoroutineSetting() // 시민스포너랑 건물안에서 나올 때, 밖으로 나올 때 사용 해야함
    {
        StartCoroutine(MoveCoroutine());
    }
    public IEnumerator MoveCoroutine()
    {
        while (state != State.die)
        {
            animator.SetFloat("isMove", nav.speed);
            if (state == State.needNextMove) // MoveResult과 navtarget을 설정해주고 State를 무조건 move로 바꿈
            {
                SetNextMoveTarget();  
            }
            if (state == State.Move) // 한번 돌때마다 MoveResult 에 따른 타겟확인을 함
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
            case MoveResult.InBuilding:
                Check_Get_In_Building_Move_Pos();
                break;
            case MoveResult.None:
                CheckRoadTargetPos();
                break;
        }
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
            nav.speed = 0.1f;
            nav.updatePosition = false;
            state = State.needNextMove;
        }

    }
    private void CheckBuildingTargetPos() // Money Cal
    {
        checkDistance = Vector3.Distance(gameObject.transform.position, navTarget.transform.position);
        if (checkDistance < 3f)
        {
            switch (moveResult)
            {
                case MoveResult.GetMoney:
                    citizenINFO.GetMoney(1);
                    break;
                case MoveResult.TakeMoney: 
                    citizenINFO.TakeMoney(1);
                    citizen_INOUT_Control.GetInBuilding();
                    break;
                case MoveResult.NoneMoney: 
                    break;
            }
            
            // TODO 시민을 빌딩 안으로 이동 시키는 거 구현하기 
            nav.speed = 0.1f;
            nav.updatePosition = false;
            state = State.needNextMove;
        }

    }

    private void Check_Get_In_Building_Move_Pos()
    {
        checkDistance = Vector3.Distance(gameObject.transform.position, navTarget.transform.position);
        if (checkDistance < 2.5f)
        {
            //TODO 건물 밖으로 나가게 하는거
            InBuildingTargetSetting(); 
        }
    }
    //------------------Move Target Setting -----------------
    private void SetNextMoveTarget()
    {

        // next move target value setting
        nav.speed = 2.5f;
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
            nav.SetDestination(MapData.Instance.NavMesh_Target_Bug_Fix_Pos.position);
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
            nav.speed = 0.1f;
            surprised_Mark.SetActive(false);
        }
        else
        {
            checkDistance = Vector3.Distance(gameObject.transform.position, navTarget.transform.position);
            if (checkDistance < 2f)
            {
                SetNavTarget_Road();
            }
            state = State.Run;
            nav.speed = 7.5f;
            animator.SetFloat("isMove", nav.speed);
            surprised_Mark.SetActive(true);
        }
      
    }
    
    public void InBuildingSetting()
    {
        state = State.Move;
        moveResult = MoveResult.InBuilding;
        InBuildingTargetSetting();
        nav.speed = 3f;
        animator.SetFloat("isMove", nav.speed);
        CitizenCoroutineSetting();
    }

    public void InBuildingTargetSetting()
    {
        int InmovePos = LayerMask.GetMask("InBuildingMovePos");
        Collider[] colliders = Physics.OverlapSphere(transform.position, 20f, InmovePos);
        randNum = Random.Range(0, colliders.Length);
        navTarget = colliders[randNum].transform;
        nav.SetDestination(navTarget.position);
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
        currentPrefab.SetActive(false);
        MapData.Instance.currentCitizenCount--;
        LeanPool.Despawn(this);
    }

}
