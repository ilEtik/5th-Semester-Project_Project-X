using UnityEngine;

public class _EnemyShootManager : MonoBehaviour
{
    [HideInInspector] public SphereCollider col;
    [HideInInspector] public Animator anim;
    [HideInInspector] public _EnemySight sight;
    private _Shooting weapon;
    private _HashIDs hash;
    private Transform player;
    private _PlayerController playerCon;

    private void Start()
    {
        weapon = GetComponentInChildren<_Shooting>();        
        anim = GetComponent<Animator>();
        hash = _HashIDs.hash;
        sight = GetComponent<_EnemySight>();
        player = GameObject.FindGameObjectWithTag(_TagManager.tag_Player).transform;
        playerCon = player.GetComponent<_PlayerController>();
    }

    void Update()
    {        
        anim.SetFloat(hash.e_FireRateFloat, weapon.weapon.fireRate * .5f);
        float shot = anim.GetFloat(hash.e_ShotFloat);

        if (shot > .35f)
        {
            if (weapon.currentMunition <= 0)
                StartCoroutine(weapon.Reload());
            else if (weapon.shootable)
            {
                weapon.shootable = false;
                weapon.Shoot();
            }
        }
        else
            weapon.shootable = true;
    }

    private void OnAnimatorIK(int layerIndex)
    {
        float aimWeight = anim.GetFloat(hash.e_AimWeightFloat);
        if(!playerCon.isCrouching && !playerCon.isLaying && !playerCon.isSliding)
            anim.SetIKPosition(AvatarIKGoal.RightHand, player.position + Vector3.up * 3f);
        else if (playerCon.isCrouching && !playerCon.isLaying && !playerCon.isSliding)
            anim.SetIKPosition(AvatarIKGoal.RightHand, player.position + Vector3.up * 2.25f);
        else if (!playerCon.isCrouching && playerCon.isLaying && !playerCon.isSliding)
            anim.SetIKPosition(AvatarIKGoal.RightHand, player.position + Vector3.up * 1.5f);
        else if (!playerCon.isCrouching && !playerCon.isLaying && playerCon.isSliding)
            anim.SetIKPosition(AvatarIKGoal.RightHand, player.position + Vector3.up * 2.25f);
        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, aimWeight);
    }

    public void Die()
    {
        weapon.gameObject.AddComponent<_PickUp>();
        weapon.GetComponent<_PickUp>().weapon = weapon.weapon;
        weapon.gameObject.layer = 10;
        Destroy(weapon);
    }
}
