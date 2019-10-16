using UnityEngine;
using UnityEngine.AI;

public class _EnemyWalk : MonoBehaviour
{
    public float walkSpeed = 3.5f;
    public float sprintSpeed = 5f;
    public float patrolWaitTime = 2f;
    public float chaseWaitTime = 5f;
    public _Path path;

    private NavMeshAgent agent;
    private _EnemySight sight;
    public float chaseTimer;
    public float patrolTimer;
    private int waypointInd;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        sight = GetComponent<_EnemySight>();
        Patrol();
        agent.speed = walkSpeed;
    }

    void Update()
    {
        if (sight.playerInSight)
            Shoot();
        else if (sight.lastPlayerSight != _GameManager._GM.resetPosition) 
            ChasePlayer();
        else
            Patrol();
    }

    void Shoot()
    {
        agent.isStopped = true;
    }

    void ChasePlayer() //Chasing The Player
    {
        agent.isStopped = false;
        agent.speed = sprintSpeed;
        Vector3 sightDeltaPos = sight.lastPlayerSight - transform.position;

        if (sightDeltaPos.sqrMagnitude > 4f)
            agent.destination = sight.lastPlayerSight;

        if (agent.remainingDistance < agent.stoppingDistance)
        {
            chaseTimer += Time.deltaTime;

            if(chaseWaitTime > chaseTimer)
            {
                sight.lastPlayerSight = _GameManager._GM.resetPosition;
                chaseTimer = 0f;
            }
        }
        else
            chaseTimer = 0f;
    }

    void Patrol() //Go back to the walkPath
    {
        agent.isStopped = false;
        if (path == null || path.waypoints.Length == 0)
        {
            Idle();
            return;
        }
        agent.speed = walkSpeed;

        if (agent.remainingDistance < agent.stoppingDistance || agent.destination == _GameManager._GM.resetPosition)
        {
            patrolTimer += Time.deltaTime;

            if (patrolTimer > patrolWaitTime)
            {
                waypointInd = (waypointInd + 1) % path.waypoints.Length;
                patrolTimer = 0f;
            }
        }
        else
            patrolTimer = 0f;

        agent.destination = path.waypoints[waypointInd].position;
    }

    void Idle() //dont move and just do Idle things
    {
        agent.isStopped = true;
    }
}