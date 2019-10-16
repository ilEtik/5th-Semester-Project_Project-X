using UnityEngine;
using UnityEngine.AI;

public class _EnemyAnimator : MonoBehaviour
{
    private float deadZone = 4f;

    private Transform player;
    private _EnemySight sight;
    private NavMeshAgent agent;
    private Animator anim;
    private _HashIDs hash;
    private _AnimatorSetup setup;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag(_TagManager.tag_Player).transform;
        sight = GetComponent<_EnemySight>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        hash = FindObjectOfType<_HashIDs>();
        agent.updateRotation = false;
        setup = new _AnimatorSetup(anim, hash);
        anim.SetLayerWeight(1, 1f);
        anim.SetLayerWeight(2, 1f);
        deadZone *= Mathf.Deg2Rad;
    }

    private void Update()
    {
        NavAnimSetup();
    }

    private void OnAnimatorMove()
    {
        if (Time.timeScale > 0)
        {
            agent.velocity = anim.deltaPosition / (Time.deltaTime + .001f);
            transform.rotation = anim.rootRotation;
        }
    }

    void NavAnimSetup()
    {
        float speed;
        float angle;

        if(sight.playerInSight)
        {
            speed = 0f;
            angle = FindAngle(transform.forward, player.position - transform.position, transform.up);
        }
        else
        {
            speed = Vector3.Project(agent.desiredVelocity, transform.forward).magnitude;
            angle = FindAngle(transform.forward, agent.desiredVelocity, transform.up);

            if(Mathf.Abs(angle) < deadZone)
            {
                transform.LookAt(transform.position + agent.desiredVelocity);
                angle = 0f;
            }
        }
        setup.Setup(speed, angle);
    }

    float FindAngle(Vector3 origin, Vector3 target, Vector3 upVector)
    {
        if (target == Vector3.zero)
            return 0f;

        float angle = Vector3.Angle(origin, target);
        Vector3 normal = Vector3.Cross(origin, target);
        angle *= Mathf.Sign(Vector3.Dot(normal, upVector));
        angle *= Mathf.Deg2Rad;
        return angle;
    }
}
