using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static Police;

public class Citizen : MonoBehaviour
{
    [SerializeField]
    Transform                   navTarget;
   
    CitizenINFO                 citizenINFO;
    Citizen_INOUT_Control       citizen_INOUT_Control;
    CitizenDemo                 citizenDemo;
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
    public NavMeshAgent         nav;
    public bool                 isQuestion_MarkOn = false;
    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        citizenINFO = GetComponent<CitizenINFO>();
        citizenDemo = GetComponent<CitizenDemo>();
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
        Run,
        Demo
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

    public void CitizenCoroutineSetting() // �ùν����ʶ� �ǹ��ȿ��� ���� ��, ������ ���� �� ��� �ؾ���
    {
        StartCoroutine(MoveCoroutine()); 
    }
    public IEnumerator MoveCoroutine()
    {
        while (state != State.die)
        {
            animator.SetFloat("isMove", nav.speed);
  
            switch (state)
            {
                case State.Move:
                    CheckTargetPos();       // �ѹ� �������� MoveResult �� ���� Ÿ��Ȯ���� ��
                    break;
                case State.Run:
                    RunAway();              // �÷��̾�Լ� ���� ĥ �� �߻�
                    break;
                case State.needNextMove:
                    SetNextMoveTarget();    // MoveResult�� navtarget�� �������ְ� State�� ������ move�� �ٲ�
                    break;
                case State.Demo:            // ������ �� �� �߻�
                    citizenDemo.Demo();
                    break;
            }

            EmotionCheck(); // �ù��� ���� ��Ʈ��

            yield return new WaitForSecondsRealtime(3f);
        }
        yield break;
    }
    // --------- Check Target Position -------------
    private void CheckTargetPos()
    {
        switch (moveResult)
        {
            
            case MoveResult.InBuilding:
                Check_Get_In_Building_Move_Pos();
                break;
            case MoveResult.None:
                CheckRoadTargetPos();
                break;
            default :
                CheckBuildingTargetPos(); 
                break;
        }
        animator.SetFloat("isMove", nav.speed);
    }

    private void CheckRoadTargetPos()
    { 
        checkDistance = Vector3.Distance(gameObject.transform.position, navTarget.transform.position);
        if (checkDistance < 3f)
        {
            state = State.needNextMove;
        }
        if (navTarget == null)
        {
            state = State.needNextMove;
            return;
        }

    }
    private void CheckBuildingTargetPos() // Money Cal
    {
        if (navTarget == null)
        {
            state = State.needNextMove;
        }
        else
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
                        state = State.needNextMove;
                        break;
                }
            }
        }
    }

    private void Check_Get_In_Building_Move_Pos()
    {
        checkDistance = Vector3.Distance(gameObject.transform.position, navTarget.transform.position);
        if (checkDistance < 1.3f)
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
        nav.speed = 3f;
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
            checkDistance = Vector3.Distance(gameObject.transform.position, navTarget.transform.position);

            SetNavTarget_Road();

            state = State.Run;
            nav.speed = 7.5f;
            animator.SetFloat("isMove", nav.speed);
            surprised_Mark.SetActive(true);
            citizenINFO.emotionPoint -= 0.01f;
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
    public void EmotionCheck()
    {
        citizenINFO.EmotionCheck();
    }
    public void Question_MarkSet()
    {
        if (isQuestion_MarkOn == false)
        {
            CityControlData.Instance.safety_Rating -= 0.01f;
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
    public void SetINFO()
    {
        // Name Setting
        randNum = Random.Range(0, GameDB.Instance.nameChar.Count);
        randNum2 = Random.Range(0, GameDB.Instance.nameChar_2.Count);
        randNum3 = Random.Range(0, GameDB.Instance.nameChar_2.Count);
        citizenName = GameDB.Instance.nameChar[randNum].ToString() +
                        GameDB.Instance.nameChar_2[randNum2].ToString() +
                            GameDB.Instance.nameChar_2[randNum3].ToString();

        citizenINFO.nameText.text = citizenName;
        citizenINFO.nameText.fontSize = 0.5f;

        //emotion Setting
        randNum = Random.Range(5, 10);
        citizenINFO.emotionPoint = randNum;
        citizenINFO.EmotionCheck();

    }
    public void GetDamage(float _damage)
    {
        print("�������� �Ծ����ϴ� ! ");
        RunAway();
        currentHP -= _damage;
        if (currentHP <= 0 && state != State.die)
        {
            // ���� �� �ִϸ��̼� ���� �����Ű��
            state = State.die;
            animator.SetTrigger("isDie");
        }
    }

    public void Die()
    {
        print("�ù��� ����߽��ϴ� ! ");
        currentPrefab.SetActive(false);

        CityControlData.Instance.safety_Rating -= 0.1f;
        MapData.Instance.currentCitizenCount--;
        QuestManager.Instance.player_Mission.mission_Citizen++;
        QuestManager.Instance.player_Mission.MissionCounting();
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
