using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    private Transform target { get { return PlayerController2D.instance.transform; } }

    public bool IsMoving { get { return agent.remainingDistance > 0; } }
    private void Start()
    {
        agent.updateUpAxis = false;
        agent.updateRotation = false;
    }
    private void Update()
    {
        if (target == null) return;
        agent.SetDestination(target.position);
        transform.up = -(target.position - transform.position);
    }
}
