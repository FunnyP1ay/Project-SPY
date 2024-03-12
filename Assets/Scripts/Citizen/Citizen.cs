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
    // Start is called before the first frame update
    private void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        nav.speed = 1.0f;
        nav.updatePosition = true;

        nav.SetDestination(navTarget.position);
    }

    private void SetNavTarget()
    {

    }
    private void CrossTheCrosswalk()
    {

    }
}
