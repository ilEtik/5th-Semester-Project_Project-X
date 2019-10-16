using UnityEngine;

public class _AnimatorSetup
{
    public float speedDampTime = .1f;
    public float angularSpeedDampTime = .7f;
    public float angleResponseTime = .6f;

    private Animator anim;
    private _HashIDs hash;

    public _AnimatorSetup(Animator _anim, _HashIDs _hash)
    {
        anim = _anim;
        hash = _hash;
    }

    public void Setup(float speed, float angle)
    {
        float angularSpeed = angle / angleResponseTime;
        anim.SetFloat(hash.e_SpeedFloat, speed, speedDampTime, Time.deltaTime);
        anim.SetFloat(hash.e_AngularSpeedFloat, angularSpeed, angularSpeedDampTime, Time.deltaTime);
    }
}
