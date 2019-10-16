using UnityEngine;

public class _HashIDs : MonoBehaviour
{
    public static _HashIDs hash;

    private void Awake()
    {
        hash = this;
    }

    public int p_isCrouchingBool;
    public int p_isLayingBool;
    public int p_isSlidingBool;
    public int p_DyingTrigger;
    public int e_dyingState;
    public int e_locomotionState;
    public int e_PlayerInSightBool;
    public int e_isReloadingBool;
    public int e_HasGunBool;
    public int e_SpeedFloat;
    public int e_ShotFloat;
    public int e_AngularSpeedFloat;
    public int e_AimWeightFloat;
    public int e_FireRateFloat;
    public int e_ReloadSpeedFloat;
    public int e_DyingTrigger;
    public int w_isReloadingBool;
    public int w_isScopedBool;
    public int w_isSprintBool;
    public int w_SwapTrueTrigger;
    public int w_SwapFalseTrigger;

    private void Start()
    {
        p_isCrouchingBool = Animator.StringToHash("p_isCrouching");
        p_isLayingBool = Animator.StringToHash("p_isLaying");
        p_isSlidingBool = Animator.StringToHash("p_isSliding");
        p_DyingTrigger = Animator.StringToHash("p_Dying");
        e_locomotionState = Animator.StringToHash("Base Layer.Locomotion");
        e_dyingState = Animator.StringToHash("Base Layer.Dying");
        e_PlayerInSightBool = Animator.StringToHash("PlayerInSight");
        e_isReloadingBool = Animator.StringToHash("isReloading");
        e_HasGunBool = Animator.StringToHash("HasGun");
        e_SpeedFloat = Animator.StringToHash("Speed");
        e_ShotFloat = Animator.StringToHash("Shot");
        e_AngularSpeedFloat = Animator.StringToHash("AngularSpeed");
        e_AimWeightFloat = Animator.StringToHash("AimWeight");
        e_FireRateFloat = Animator.StringToHash("FireRate");
        e_ReloadSpeedFloat = Animator.StringToHash("ReloadSpeed");
        e_DyingTrigger = Animator.StringToHash("Dying");
        w_isReloadingBool = Animator.StringToHash("w_isReloading");
        w_isScopedBool = Animator.StringToHash("w_isScoped");
        w_isSprintBool = Animator.StringToHash("w_isSprint");
        w_SwapTrueTrigger = Animator.StringToHash("w_SwapTrue");
        w_SwapFalseTrigger = Animator.StringToHash("w_SwapFalse");
    }
}