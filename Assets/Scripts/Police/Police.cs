using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Police : MonoBehaviour
{
   
    [SerializeField]
    Transform navTarget;
    NavMeshAgent nav;
    CitizenINFO citizenINFO;
    private int randNum;
    private int randNum2;
    private int randNum3;
    private float checkDistance;
    public string citizenName;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        citizenINFO = GetComponent<CitizenINFO>();
    }
    // Start is called before the first frame update
    private void Start()
    {

        nav.speed = 3.0f;
        state = MoveState.needNextMove;
        StartCoroutine(MoveCoroutine());
        SetName();

    }
    private void OnEnable()
    {
    }

    public enum MoveState
    {
        die,
        needNextMove,
        Move
    }
    public MoveState state;

    public enum MoveTarget
    {
        building,
        store,
        house,
        road,
        spy
    }
    public MoveTarget moveTarget;

    public enum Weapon
    {
        none,
        equip
        
    }
    public Weapon weapon;
    IEnumerator MoveCoroutine()
    {
        while (state != MoveState.die)
        {
            if (state == MoveState.needNextMove)
            {
                SetNextMoveTarget();
            }
            if (state == MoveState.Move)
            {
                CheckTargetPos();
            }
            yield return new WaitForSecondsRealtime(3f);
        }
        yield break;
    }
    // --------- Check Target Position -----------
    private void CheckTargetPos()
    {
        if (moveTarget == MoveTarget.road)
        {
            CheckRoadTargetPos();
        }
        else if (moveTarget == MoveTarget.building)
        {
            CheckBuildingTargetPos();
        }
        else if (moveTarget == MoveTarget.store)
        {
            CheckBuildingTargetPos();
        }
        else if (moveTarget == MoveTarget.house)
        {
            CheckBuildingTargetPos();
        }
    }

    private void CheckRoadTargetPos()
    {
        checkDistance = Vector3.Distance(gameObject.transform.position, navTarget.transform.position);
        if (checkDistance < 2f)
        {
            nav.updatePosition = false;
            state = MoveState.needNextMove;
        }

    }
    private void CheckBuildingTargetPos()
    {
        checkDistance = Vector3.Distance(gameObject.transform.position, navTarget.transform.position);
        if (checkDistance < 2f)
        {
            // TODO �ù��� ���� ������ �̵� ��Ű�� �� �����ϱ� 
            nav.updatePosition = false;
            state = MoveState.needNextMove;
        }
    }
    //------------------Move Target Setting -----------------
    private void SetNextMoveTarget()
    {
        // next move target value setting
        randNum = Random.Range(0, 10);
        if (randNum == 0)
        {
            SetNavTarget_Building(0); // Building
        }
        if (randNum == 1)
        {
            SetNavTarget_Building(1); // Store
        }
        if (randNum == 2)
        {
            SetNavTarget_Building(2); // House
        }
        if (randNum > 2)
        {
            SetNavTarget_Road();      // Road
        }
    }
    private void SetNavTarget_Building(int _value)
    {
        int BuildingLayerMask = LayerMask.GetMask("Building");
        Collider[] colliders = Physics.OverlapSphere(transform.position, 30f, BuildingLayerMask);

        if (colliders.Length == 0)
        {
            SetNavTarget_Road();
            print("�ǹ��� �����ϴ� !");
        }
        else
        {
            switch (_value) // TODO �� �ǹ� �� �ൿ ���� ����
            {
                case 0:
                    moveTarget = MoveTarget.building;
                    print("Ÿ���� �������� ��ҽ��ϴ� ! ");
                    break;
                case 1:
                    moveTarget = MoveTarget.store;
                    print("Ÿ���� �������� ��ҽ��ϴ� ! ");
                    break;
                case 2:
                    moveTarget = MoveTarget.house;
                    print("Ÿ���� �������� ��ҽ��ϴ� ! ");
                    break;
            }
            randNum = Random.Range(0, colliders.Length);
            if (colliders[randNum].gameObject.TryGetComponent(out Building _building))
            {
                randNum = Random.Range(0, _building.building_NavTargetPoint.Count);
                navTarget = _building.building_NavTargetPoint[randNum].transform;
                nav.SetDestination(navTarget.position);
                nav.updatePosition = true;
                state = MoveState.Move;
            }
        }
    }
    private void SetNavTarget_Road()
    {
        int roadLayerMask = LayerMask.GetMask("Road");
        Collider[] colliders = Physics.OverlapSphere(transform.position, 15f, roadLayerMask);
        randNum = Random.Range(0, colliders.Length);
        if (colliders[randNum].gameObject.TryGetComponent(out Road _road))
        {
            randNum = Random.Range(0, _road.navTargetPos_List.Count);
            navTarget = _road.navTargetPos_List[randNum].transform;
            nav.SetDestination(navTarget.position);
            nav.updatePosition = true;
            state = MoveState.Move;
            moveTarget = MoveTarget.road;
        }
    }
    public void ChaseSpy(Transform _target)
    {
        weapon = Weapon.equip;
        moveTarget = MoveTarget.spy;
        nav.SetDestination(_target.position);

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
}
