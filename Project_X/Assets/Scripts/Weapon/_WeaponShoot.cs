using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class _WeaponShoot : MonoBehaviour
{
    public ScriptableWeapon weapon;
    public int currentMunition, magazineCapacity;
    public float fireRate, reloadTime;
    public bool shootable = true, isReloading = false, isScoped = false;
    public FireMode firemode;

    private Animator anim;
    private ParticleSystem muzzleFlash;
    [SerializeField]
    private Transform bulletSpawn, bulletParent;
    [SerializeField]
    private GameObject mesh;
    private GameObject sniperScope, crosshair;
    private _Recoil recoil;
    private _WeaponManager manager;
    private AudioSource _audio;
    private Camera cam;
    private _HashIDs hash;
    private _ObjectPooler pooler;

    void Start()
    {
        anim = GetComponent<Animator>();
        muzzleFlash = GetComponentInChildren<ParticleSystem>();
        sniperScope = GameObject.Find("SniperScope");
        crosshair = GameObject.Find("Crosshair");
        recoil = GetComponentInParent<_Recoil>();
        manager = GetComponentInParent<_WeaponManager>();
        _audio = GetComponent<AudioSource>();
        cam = GameObject.FindGameObjectWithTag(_TagManager.tag_MainCamera).GetComponent<Camera>();
        hash = FindObjectOfType<_HashIDs>();
        pooler = _ObjectPooler.pooler;

        GetWeaponVars();
        currentMunition = magazineCapacity;
        _HUD.hud.UpdateAmmo(currentMunition);
    }

    private void OnEnable()
    {
        isReloading = false;
        if(anim != null)
            anim.SetBool(hash.w_isReloadingBool, false);
    }


    public void Shoot()
    {
        _audio.clip = weapon.shootSound;
        _audio.Play();
        muzzleFlash.Play();
        pooler.SpawnFromPool(_TagManager.pool_bullet, bulletSpawn.position, bulletSpawn.rotation, weapon.fireRange, weapon.damage);
        recoil.StartRecoil(weapon.recoil, weapon.recoilPos, weapon.recoilSpeed, weapon.recoilRot, weapon.maxRecoilPos, weapon.maxRecoilRot);
        currentMunition--;
        _HUD.hud.UpdateAmmo(currentMunition);
    }

    public IEnumerator BurstShoot()
    {
        Shoot();
        yield return new WaitForSeconds(.1f);
        if(currentMunition > 0)
            Shoot();
        yield return new WaitForSeconds(.1f);
        if (currentMunition > 0)
            Shoot();
    }

    public IEnumerator Reload()
    {
        if (weapon.sniperScope)
        {
            sniperScope.GetComponent<UnityEngine.UI.Image>().enabled = false;
            mesh.SetActive(true);
        }
        _audio.clip = weapon.reloadSound;
        isReloading = true;
        anim.SetBool(hash.w_isReloadingBool, true);
        _audio.Play();
        yield return new WaitForSeconds(reloadTime);
        currentMunition = magazineCapacity;
        _HUD.hud.UpdateAmmo(currentMunition);
        anim.SetBool(hash.w_isReloadingBool, false);
        yield return new WaitForSeconds(.5f);
        isReloading = false;
        if (weapon.sniperScope)
        {
            sniperScope.GetComponent<UnityEngine.UI.Image>().enabled = isScoped;
            mesh.SetActive(!isScoped);
        }
        StopCoroutine(Reload());
    }

    public void Aim()
    {
        isScoped = !isScoped;
        crosshair.SetActive(!isScoped);
        anim.SetBool(hash.w_isScopedBool, isScoped);

        if(isScoped)
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, weapon.scopeFovMultiplikator, 1f);
        else
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, _GameManager._GM.normalFov, .25f);

        if (weapon.sniperScope)
        {
            sniperScope.GetComponent<UnityEngine.UI.Image>().enabled = isScoped;
            mesh.SetActive(!isScoped);
        }
    }

    void GetWeaponVars() //Don't use the vars from "ScriptableWeapon" for changes in the variables(Or just get a copy from the "ScriptableWeapon" so you can edit this just like you did with the Items.
    {
        firemode = weapon.firemode;
        magazineCapacity = weapon.magazineCapacity;
        fireRate = weapon.fireRate;
        reloadTime = weapon.reloadTime;
    }
}
