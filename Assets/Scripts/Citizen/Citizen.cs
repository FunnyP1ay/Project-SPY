using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Citizen : MonoBehaviour
{
    [SerializeField]
    Transform           navTarget;
    NavMeshAgent        nav;
    CitizenINFO         citizenINFO;
    private int         randNum;
    private int         randNum2;
    private int         randNum3;
    private float       checkDistance;
    public string       citizenName;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        citizenINFO = GetComponent<CitizenINFO>();
    }
    // Start is called before the first frame update
    private void Start()
    {
      
        nav.speed = 3.0f;
        state = State.needNextMove;
        StartCoroutine(MoveCoroutine());
        SetName();


    }
    private void OnEnable()
    {
    }

    public enum State
    {
        die,
        freeMove,
        needNextMove
    }
    public State state;


    IEnumerator MoveCoroutine()
    {
        while(state != State.die)
        {
            
            if (state == State.freeMove)
            {
                CheckTargetPos();
            }
            if(state == State.needNextMove)
            {
                SetNavTarget();
            }
            yield return new WaitForSecondsRealtime(3f);
        }
        yield break;
    }

    private void SetNavTarget()
    {
        int roadLayerMask = LayerMask.GetMask("Road");
        Collider[] colliders = Physics.OverlapSphere(transform.position, 15f, roadLayerMask);
        randNum = Random.Range(0, colliders.Length);
        if (colliders[randNum].gameObject.TryGetComponent(out Road _road))
        {
            randNum = Random.Range(0,_road.navTargetPos_List.Count);
            navTarget =  _road.navTargetPos_List[randNum].transform;
            nav.SetDestination(navTarget.position);
            nav.updatePosition = true;
            state = State.freeMove;
        }

    }
    private void CheckTargetPos()
    {
        checkDistance = Vector3.Distance(gameObject.transform.position, navTarget.transform.position);
        if (checkDistance < 2f)
        {
            nav.updatePosition = false;
            state = State.needNextMove;
        }

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
        citizenINFO.nameText.fontSize = 1;
    }
    private void CrossTheCrosswalk()
    {

    }
    private void TrafficTarget()
    {

    }

}
