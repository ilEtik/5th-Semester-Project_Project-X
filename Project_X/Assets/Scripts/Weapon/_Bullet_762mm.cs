using UnityEngine;

public class _Bullet_762mm : MonoBehaviour, IPooledObject
{
    public float fireRange;
    public int dmg;
    private int stepsPerFrame = 6;
    private Vector3 velocity;
    private _ObjectPooler pooler;

    void Start()
    {
        pooler = _ObjectPooler.pooler;
    }

    void IPooledObject.OnObjectSpawn()
    {
        velocity = transform.forward * fireRange;
    }


    private void Update()
    {
        Vector3 point1 = transform.position;
        float stepSize = 1f / stepsPerFrame;

        for (float step = 0; step < 1; step += stepSize)
        {
            velocity += Physics.gravity * stepSize * Time.deltaTime;
            Vector3 point2 = point1 + velocity * stepSize * Time.deltaTime;
            Ray ray = new Ray(point1, point2 - point1);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, (point2 - point1).magnitude) && !hit.collider.isTrigger)
            {
                if (hit.collider.tag == _TagManager.tag_Enemy)
                {
                    GameObject hole = pooler.SpawnFromPool(_TagManager.pool_bulletHoleBlood1, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                    hit.collider.GetComponentInParent<_EnemeyStats>().holes.Add(hole);
                    hit.collider.GetComponentInParent<_EnemeyStats>().GetDamage(dmg);
                }
                else if (hit.collider.tag == _TagManager.tag_Player)
                    hit.collider.GetComponentInParent<_PlayerStats>().GetDamage(dmg);
                else
                    pooler.SpawnFromPool(_TagManager.pool_bulletHoleMetal1, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                gameObject.SetActive(false);
            }
            point1 = point2;
        }
        transform.position = point1;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 point1 = transform.position;
        Vector3 predictedBullletVelocity = transform.forward * fireRange;
        float stepSize = 1f / stepsPerFrame;
        for (float step = 0; step < 1; step += stepSize)
        {
            predictedBullletVelocity += Physics.gravity * stepSize;
            Vector3 point2 = point1 + predictedBullletVelocity * stepSize;
            Gizmos.DrawLine(point1, point2);
            point1 = point2;
        }
    }
}
