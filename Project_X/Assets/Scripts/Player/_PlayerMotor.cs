using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(_PlayerController))]
[RequireComponent(typeof(_PlayerAnimator))]
[RequireComponent(typeof(_PlayerMenus))]
[RequireComponent(typeof(_PlayerSaveLoad))]
[RequireComponent(typeof(_PlayerStats))]
public class _PlayerMotor : MonoBehaviour
{
    #region Player Components
    [HideInInspector]
    public _PlayerController pController;
    [HideInInspector]
    public _PlayerStats pStats;
    [HideInInspector]
    public _PlayerAnimator pAnim;
    [HideInInspector]
    public _PlayerMenus pMenus;
    [HideInInspector]
    public _PlayerSaveLoad pSaveAndLoad;
    #endregion

    #region Variables
    [Header("Speed")]
    public float walkSpeed = 5.0f;
    public float backWalkSpeed = 4.0f;
    public float sprintSpeed = 9.0f;
    public float crouchSpeed = 2.5f;
    public float laySpeed = 1.0f;
    public float slideSpeed = 9.5f;
    public float slideTime = .65f;
    [Space(5)]
    public float jumpStrengh = 20f;
    public float jumpSpeed = 2.0f;
    public float backJumpSpeed = 1.0f;
    public float sprintJumpSpeed = 7.0f;
    [Space(5)]
    [Header("Camera")]
    public float sensitivityX = 10f;
    public float sensitivityY = 10f;
    [Space(5)]
    public bool invertX = false;
    public bool invertY = false;
    [HideInInspector] public float maxAngleY = 85f;
    [Space(5)]
    public bool canMove = true;
    public bool canRot = true;
    public bool sneaking;

    private GameObject cam;
    private Vector3 velocity = Vector3.zero;
    [HideInInspector] public Vector3 rotation = Vector3.zero;
    private float camRotationX = 0f;
    private float curCamRotX = 0f;
    [HideInInspector] public Rigidbody rb;
    private float recoil, recoilRot, recoilSpeed, maxRecoilRot, curRecoil;
    #endregion

    void Start()
    {
        pController = GetComponent<_PlayerController>();
        pStats = GetComponent<_PlayerStats>();
        pAnim = GetComponent<_PlayerAnimator>();
        pMenus = GetComponent<_PlayerMenus>();
        pSaveAndLoad = GetComponent<_PlayerSaveLoad>();
        rb = GetComponent<Rigidbody>();
        cam = GameObject.FindGameObjectWithTag(_TagManager.tag_MainCamera);
    }

    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position + Vector3.up, -Vector3.up, 1.1f);
    }

    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    public void RotateCam(float _camRotationX)
    {
        camRotationX = _camRotationX;
    }

    public void Jump()
    {
        rb.AddForce(Vector3.up * Time.fixedDeltaTime * jumpStrengh * 10, ForceMode.Impulse);
    }

    public void _Slide()
    {
        rb.AddForce(velocity * slideSpeed / 5, ForceMode.Impulse);
    }

    public void StartRecoil(float _recoil, float _recoilRot, float _recoilSpeed, float _maxRecoilRot)
    {
        recoil += _recoil;
        recoilRot += _recoilRot;
        recoilSpeed = _recoilSpeed;
        maxRecoilRot = _maxRecoilRot;
    }

    void FixedUpdate()
    {
        if(canMove)
            PerformMovement();

        PerformRotation();
    }

    void PerformMovement()
    {
        if (velocity != Vector3.zero)
            rb.MovePosition(transform.position + velocity * Time.fixedDeltaTime * pStats.speed);
    }

    void PerformRotation()
    {
        if (!invertX)
            rb.MoveRotation(transform.rotation * Quaternion.Euler(rotation));
        else
            rb.MoveRotation(transform.rotation * Quaternion.Euler(-rotation));

        if (cam != null)
        {
            curCamRotX -= camRotationX;
            curCamRotX = Mathf.Clamp(curCamRotX, -maxAngleY, maxAngleY);
            Recoil();

            if (!invertY)
                cam.transform.localEulerAngles = new Vector3(curCamRotX + -curRecoil, 0f, 0f);
            else
                cam.transform.localEulerAngles = new Vector3(-curCamRotX + -curRecoil, 0f, 0f);
        }
    }

    void Recoil()
    {
        if (recoil > 0)
        {
            if (curRecoil < recoilRot * .8f)
                curRecoil = Mathf.Lerp(curRecoil, recoilRot, Time.deltaTime * recoilSpeed *.1f);
            else if (curRecoil < recoilRot * .2f)
                curRecoil = Mathf.Lerp(curRecoil, recoilRot, Time.deltaTime * recoilSpeed * .01f);
            else
                curRecoil = Mathf.Lerp(curRecoil, curRecoil + 1, Time.deltaTime * recoilSpeed * .1f);

            curRecoil = Mathf.Clamp(curRecoil, 0, maxRecoilRot);
            recoil -= Time.deltaTime * .7f;
        }
        else
        {
            recoilRot = 0;
            recoil = 0;
            curRecoil = Mathf.Lerp(curRecoil, 0, Time.deltaTime * 3 * .5f);
        }
    }
}
