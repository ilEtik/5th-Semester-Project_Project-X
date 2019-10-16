using UnityEngine;

public class _Recoil : MonoBehaviour
{
    private float recoil;
    private float recoilPos;
    private float recoilSpeed;
    private float recoilRot;
    private float maxRecoilPos;

    [SerializeField]
    private Transform normalTransform;

    public void StartRecoil(float _recoil, float _recoilPos, float _recoilSpeed, float _recoilRot, float _maxRecoilPos, float _maxRecoilRot)
    {
        FindObjectOfType<_PlayerMotor>().StartRecoil(_recoil, _recoilRot, _recoilSpeed, _maxRecoilRot);
        recoil = _recoil;
        recoilPos = _recoilPos / 100;
        recoilSpeed = _recoilSpeed;
        recoilRot = _recoilRot;
        maxRecoilPos = _maxRecoilPos / 100;
    }

    void Update ()
    {
        if (recoil > 0 && transform.localPosition.z > -maxRecoilPos)
        {
            var maxRecoil = Quaternion.Euler(Random.Range(-recoilRot, recoilRot), Random.Range(-recoilRot, recoilRot), 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, maxRecoil, Time.deltaTime * recoilSpeed);
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z);
            transform.Translate(new Vector3(0, 0, -recoilPos), Space.Self);
            recoil -= Time.deltaTime;
        }
        else
        {
            recoil = 0;
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.identity, Time.deltaTime * recoilSpeed / 2);
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z);
            transform.position = Vector3.Lerp(transform.position, normalTransform.position, Time.deltaTime * recoilSpeed * 2);
        }
    }
}
