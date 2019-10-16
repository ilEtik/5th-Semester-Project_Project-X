using UnityEngine;

[RequireComponent(typeof(_PlayerMotor))]
public class _PlayerAnimator : MonoBehaviour
{
    private _PlayerMotor motor;
    private Animator anim;
    private _HashIDs hash;

    void Start()
    {
        motor = GetComponent<_PlayerMotor>();
        anim = GetComponent<Animator>();
        hash = FindObjectOfType<_HashIDs>();
    }

    public void Crouch()
    {
        motor.pController.isLaying = false;
        motor.pController.isSliding = false;
        anim.SetBool(hash.p_isCrouchingBool, motor.pController.isCrouching);
        anim.SetBool(hash.p_isLayingBool, false);
        anim.SetBool(hash.p_isSlidingBool, false);
    }

    public void Lay()
    {
        motor.pController.isCrouching = false;
        motor.pController.isSliding = false;
        anim.SetBool(hash.p_isLayingBool, motor.pController.isLaying);
        anim.SetBool(hash.p_isCrouchingBool, false);
        anim.SetBool(hash.p_isSlidingBool, false);
    }

    public void Slide()
    {
        motor.pController.isLaying = false;
        motor.pController.isCrouching = false;
        anim.SetBool(hash.p_isSlidingBool, motor.pController.isSliding);
        anim.SetBool(hash.p_isCrouchingBool, false);
        anim.SetBool(hash.p_isLayingBool, false);
    }
}
