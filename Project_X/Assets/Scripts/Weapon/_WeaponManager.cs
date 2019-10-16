using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputManagment;

public class _WeaponManager : MonoBehaviour
{
    private List<ScriptableWeapon> allWeapon = new List<ScriptableWeapon>();
    public ScriptableWeapon primaryWeapon;
    public ScriptableWeapon secondaryWeapon;
    [HideInInspector]public int curSlotInd;
    public GameObject curWeaponObj;
    public GameObject pickUpImage;
    public bool isShoot;
    private bool wheelActive = false;
    [SerializeField] private _WeaponWheelSlot[] weaponSlot;
    [SerializeField] public ScriptableWeapon[] pickedWeapons = new ScriptableWeapon[4];
    private float nextTimeToFire = 0f;
    private Animator anim;
    [SerializeField] private GameObject weaponWheel;
    private _PlayerPickUp pickUp;
    private _PlayerMotor motor;
    private _WeaponShoot shoot;
    private _HashIDs hash;
    private Transform player;

    void Start ()
    {
        pickUp = FindObjectOfType<_PlayerPickUp>();
        motor = FindObjectOfType<_PlayerMotor>();
        hash = FindObjectOfType<_HashIDs>();
        player = GameObject.FindGameObjectWithTag(_TagManager.tag_Player).transform;
        EquipWeapon();
        UpdateWeaponWheel();
	}
	
	void Update ()
    {
        if (shoot != null)
        {
            if (Input.GetKey(_InputManager._IM.shootKey) && shoot.currentMunition > 0 && !shoot.isReloading && Time.time >= nextTimeToFire && shoot.shootable)
            {
                StartCoroutine(IsShooingFalse());
                if (shoot.firemode == FireMode.Automatic)
                {
                    nextTimeToFire = Time.time + 1f / shoot.fireRate;
                    shoot.Shoot();
                }
                else if (Input.GetKeyDown(_InputManager._IM.shootKey))
                {
                    if (shoot.firemode == FireMode.BurstMode)
                        StartCoroutine(shoot.BurstShoot());
                    else if (shoot.firemode == FireMode.SingleShot)
                        shoot.Shoot();
                }
            }

            if (Input.GetKey(_InputManager._IM.reloadKey) && !shoot.isReloading && shoot.currentMunition != shoot.magazineCapacity)
                StartCoroutine(shoot.Reload());
            else if (Input.GetKey(_InputManager._IM.shootKey) && shoot.currentMunition < 1 && !shoot.isReloading)
                StartCoroutine(shoot.Reload());

            if (Input.GetKeyDown(_InputManager._IM.aimKey))
                shoot.Aim();

            if (motor.pController.speed == motor.sprintSpeed)
            {
                if (shoot.isScoped)
                    shoot.Aim();

                anim.SetBool(hash.w_isSprintBool, true);
                shoot.shootable = false;
            }
            else if (motor.pController.speed != motor.sprintSpeed && !motor.pMenus.isPaused)
            {
                anim.SetBool(hash.w_isSprintBool, false);
                shoot.shootable = true;
            }
        }

        if (Input.GetAxisRaw(_InputManager._IM.swapWeaponAxis) != 0 && _InputManager._IM.connectedGamepad == ConnectedGamepad.Mouse_Keyboard && secondaryWeapon != null || Input.GetKeyDown(_InputManager._IM.swapWeaponKey) && secondaryWeapon != null)
        {
            ScriptableWeapon w = primaryWeapon;
            primaryWeapon = secondaryWeapon;
            secondaryWeapon = w;
            StartCoroutine(SwapWeapon());
        }

        if (Input.GetKeyDown(_InputManager._IM.weaponWheelKey))
            StartCoroutine(WeaponWheelActive());
        else if (Input.GetKeyUp(_InputManager._IM.weaponWheelKey))
            StartCoroutine(WeaponWheelActive());

        if(Input.GetKey(_InputManager._IM.dropWeaponKey))
            DropWeapon();
	}

    public void ChangeWeapon()
    {
        if(secondaryWeapon == null)
        {
            if (pickedWeapons[0] == primaryWeapon || pickedWeapons[1] == primaryWeapon || pickedWeapons[2] == primaryWeapon || pickedWeapons[2] == primaryWeapon)
                secondaryWeapon = primaryWeapon;
        }
        else
        {
            if (pickedWeapons[0] == primaryWeapon || pickedWeapons[1] == primaryWeapon || pickedWeapons[2] == primaryWeapon || pickedWeapons[2] == primaryWeapon)
                secondaryWeapon = primaryWeapon;
        }
        primaryWeapon = weaponSlot[pickUp.targetSlotInd].weapon;
        StartCoroutine(SwapWeapon());
    }

    void EquipWeapon()
    {
        int i = 0;
        foreach(Transform weapon in transform)
        {
            if(!allWeapon.Contains(weapon.GetComponent<_WeaponShoot>().weapon))
                allWeapon.Add(weapon.GetComponent<_WeaponShoot>().weapon);

            if (primaryWeapon == allWeapon[i])
            {
                curWeaponObj = weapon.gameObject;
                weapon.gameObject.SetActive(true);
                anim = weapon.GetComponent<Animator>();
                shoot = weapon.GetComponent<_WeaponShoot>();
                _HUD.hud.UpdateAmmo(shoot.currentMunition);
            }
            else
                weapon.gameObject.SetActive(false);
            i++;
        }

        UpdateWeaponUI();

        if (weaponSlot[0].weapon == primaryWeapon)
            curSlotInd = 0;
        else if (weaponSlot[1].weapon == primaryWeapon)
            curSlotInd = 1;
        else if (weaponSlot[2].weapon == primaryWeapon)
            curSlotInd = 2;
        else if (weaponSlot[3].weapon == primaryWeapon)
            curSlotInd = 3;
        else
            curSlotInd = 0;
    }

    /// <summary>
    /// Select the weapon from the weaponwheel defined by the current "arrayInd" Integer.
    /// </summary>
    /// <param name="targetSlotInd">Set the value of the Target Slot</param>
    public void SelectWeapon(int targetSlotInd)
    {
        if (weaponSlot[targetSlotInd].weapon != null)
        {
            if (weaponSlot[targetSlotInd].weapon != primaryWeapon)
            {
                secondaryWeapon = primaryWeapon;
                primaryWeapon = weaponSlot[targetSlotInd].weapon;
                StartCoroutine(SwapWeapon());
            }
        }
    }

    IEnumerator SwapWeapon()
    {
        if (anim != null)
            anim.SetTrigger(hash.w_SwapTrueTrigger);
        yield return new WaitForSeconds(.5f);
        EquipWeapon();
        if (anim != null)
            anim.SetTrigger(hash.w_SwapFalseTrigger);
    }

    public void DropWeapon()
    {
        if (primaryWeapon == null)
            return;

        var droppedWeapon = Instantiate(primaryWeapon.pickUpPre, player.position + player.forward, player.rotation * primaryWeapon.pickUpPre.transform.rotation);
        droppedWeapon.name = primaryWeapon.weaponName;
        primaryWeapon = null;
        pickedWeapons[curSlotInd] = null;
        weaponSlot[curSlotInd].weapon = null;
        weaponSlot[curSlotInd].UpdateUI();
        EquipWeapon();
    }

    /// <summary>
    /// Updates the weapon icons in the Hud.
    public void UpdateWeaponUI()
    {
        if (primaryWeapon != null && secondaryWeapon == null)
            _HUD.hud.UpdateWeapon(primaryWeapon.weaponIcon, null);
        else if (primaryWeapon != null && secondaryWeapon != null)
            _HUD.hud.UpdateWeapon(primaryWeapon.weaponIcon, secondaryWeapon.weaponIcon);
        else if (primaryWeapon == null && secondaryWeapon != null)
            _HUD.hud.UpdateWeapon(null, secondaryWeapon.weaponIcon);
        else if (primaryWeapon == null && secondaryWeapon == null)
            _HUD.hud.UpdateWeapon(null, null);
    }

    /// <summary>
    /// Updates the icons inside the weaponwheel.
    public void UpdateWeaponWheel()
    {
        weaponSlot[0].weapon = pickedWeapons[0];
        weaponSlot[0].UpdateUI();
        weaponSlot[1].weapon = pickedWeapons[1];
        weaponSlot[1].UpdateUI();
        weaponSlot[2].weapon = pickedWeapons[2];
        weaponSlot[2].UpdateUI();
        weaponSlot[3].weapon = pickedWeapons[3];
        weaponSlot[3].UpdateUI();
    }

    IEnumerator WeaponWheelActive()
    {
        wheelActive = !wheelActive;
        yield return new WaitForEndOfFrame();
        if (!wheelActive)
            Time.timeScale = 1f;
        else
            Time.timeScale = .25f;
        weaponWheel.SetActive(wheelActive);
        _UiManager._UI.CursorDisplayMode(wheelActive);
        motor.canRot = !wheelActive;
    }

    IEnumerator IsShooingFalse()
    {
        isShoot = true;
        yield return new WaitForSeconds(.1f);
        isShoot = false;
    }
}
