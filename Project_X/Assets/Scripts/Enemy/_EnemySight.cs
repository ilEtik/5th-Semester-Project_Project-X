using UnityEngine;
using UnityEngine.AI;

public class _EnemySight : MonoBehaviour
{
    public float lookRange = 15f;
    public float fovX = 60f;
    public bool playerInSight;
    public Vector3 lastPlayerSight;
    public Transform eyes;

    private Animator anim;
    private NavMeshAgent agent;
    private Transform player;
    private SphereCollider col;
    private _PlayerMotor playerMotor;
    private _WeaponManager weaponManager;
    private _HashIDs hash;

    private void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag(_TagManager.tag_Player).transform;
        col = GetComponent<SphereCollider>();
        playerMotor = FindObjectOfType<_PlayerMotor>();
        weaponManager = GameObject.FindGameObjectWithTag(_TagManager.tag_MainCamera).GetComponentInChildren<_WeaponManager>();
        hash = FindObjectOfType<_HashIDs>();
        lastPlayerSight = _GameManager._GM.resetPosition;
        col.radius = lookRange;
    }

    private void Update()
    {
        if (!anim.GetBool(hash.e_isReloadingBool) && !playerMotor.pStats.isDead)
            anim.SetBool(hash.e_PlayerInSightBool, playerInSight);
        else
            anim.SetBool(hash.e_PlayerInSightBool, false);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject == player.gameObject)
        {
            playerInSight = false;
            Vector3 playerDir = player.position - transform.position;
            float angle = Vector3.Angle(playerDir, transform.forward);

            if (angle >= -fovX && angle <= fovX)
            {
                RaycastHit hit;
                if (Physics.Raycast(eyes.position, playerDir.normalized * lookRange, out hit))
                {
                    if (hit.transform.gameObject == player.gameObject)
                    {
                        playerInSight = true;
                        lastPlayerSight = player.position;
                    }
                }
            }
            if (CalculatePathLenght(player.position) <= lookRange && !playerMotor.sneaking || weaponManager.isShoot)
                lastPlayerSight = player.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player.gameObject)
            playerInSight = false;
    }

    float CalculatePathLenght(Vector3 targetPosition)
    {
        NavMeshPath path = new NavMeshPath();
        if (agent.enabled)
            agent.CalculatePath(targetPosition, path);
        Vector3[] allCorners = new Vector3[path.corners.Length + 2];
        allCorners[0] = transform.position;
        allCorners[allCorners.Length - 1] = targetPosition;
        for(int i = 0; i< path.corners.Length; i++)
        {
            allCorners[i + 1] = path.corners[i];
        }
        float pathLength = 0f;
        for (int i = 0; i < allCorners.Length-1; i++)
        {
            pathLength += Vector3.Distance(allCorners[i], allCorners[i + 1]);
        }
        return pathLength;
    }
}
