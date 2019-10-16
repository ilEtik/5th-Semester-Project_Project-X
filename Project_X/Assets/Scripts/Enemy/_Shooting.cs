using System.Collections;
using UnityEngine;

public class _Shooting : MonoBehaviour
{
    public bool shootable = true;
    public int currentMunition;
    public ScriptableWeapon weapon;
    public Transform bulletSpawn;

    private _EnemyShootManager enemy;
    private AudioSource _audio;
    private ParticleSystem muzzleFlash;
    private _HashIDs hash;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        enemy = GetComponentInParent<_EnemyShootManager>();
        muzzleFlash = GetComponentInChildren<ParticleSystem>();
        hash = FindObjectOfType<_HashIDs>();
        currentMunition = weapon.magazineCapacity;
    }

    public void Shoot()
    {
        _audio.clip = weapon.shootSound;
        _audio.Play();
        muzzleFlash.Play();
        _ObjectPooler.pooler.SpawnFromPool(_TagManager.pool_bullet, bulletSpawn.position, bulletSpawn.rotation, weapon.fireRange, weapon.damage);
        currentMunition--;
        if (currentMunition == 0)
            StartCoroutine(Reload());
    }

    public IEnumerator Reload()
    {
        shootable = false;
        _audio.clip = weapon.reloadSound;
        enemy.anim.SetFloat(hash.e_ReloadSpeedFloat, weapon.reloadTime);
        enemy.anim.SetBool(hash.e_isReloadingBool, true);
        _audio.Play();
        yield return new WaitForSeconds(weapon.reloadTime);
        currentMunition = weapon.magazineCapacity;
        enemy.anim.SetBool(hash.e_isReloadingBool, false);
        shootable = true;
    }
}
