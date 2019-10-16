using UnityEngine;

[CreateAssetMenu(fileName = "new Weapon", menuName = "SciptiableObject/Weapon", order = 1)]
public class ScriptableWeapon : ScriptableObject
{
    public WeaponType weaponType;
    public FireMode firemode;
    public string weaponName = "new Weapon";
    public int magazineCapacity = 30, damage = 20, fireRange = 500, bulletsToSpawn = 1;
    public float fireRate = 5f, recoil = .1f, recoilPos = 1f, recoilRot = 10f, maxRecoilPos = 5f, maxRecoilRot = 10f, recoilSpeed = 2f, reloadTime = 2f, scopeFovMultiplikator = 1f;
    public bool sniperScope, playAnimAfterShoot = false;
    public GameObject pickUpPre;
    public Sprite weaponIcon;
    public AudioClip shootSound, reloadSound;
    public RuntimeAnimatorController animController;
}

public enum WeaponType
{
    AssaultRifle,
    SubMachineGun,
    MachineGun,
    MarksmanRifle,
    SniperRifle,
    Shotgun,
    Handgun
}

public enum FireMode
{
    SingleShot,
    BurstMode,
    Automatic
}
