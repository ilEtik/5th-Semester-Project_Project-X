using UnityEngine;
using InputManagment;
using System.Collections;

[RequireComponent(typeof(_PlayerMotor))]
public class _PlayerController : MonoBehaviour
{
    public float speed;
    public bool isCrouching;
    public bool isLaying;
    public bool isSliding;
    public bool canSlide = false;

    private int holdTime;
    private _PlayerMotor motor;
    private Camera cam;
    private _WeaponManager weapon;
    private CapsuleCollider col;
    private _InputManager input;

    void Start()
    {
        motor = GetComponent<_PlayerMotor>();
        speed = motor.walkSpeed;
        cam = GameObject.FindGameObjectWithTag(_TagManager.tag_MainCamera).GetComponent<Camera>();
        weapon = FindObjectOfType<_WeaponManager>();
        col = GetComponent<CapsuleCollider>();
        input = _InputManager._IM;
    }

    void Update()
    {            
        float movX = Input.GetAxisRaw(input.movementXAxis);
        float movZ = Input.GetAxisRaw(input.movementZAxis);

        #region Cam/Move
        if (motor.canMove)
        {
            Vector3 movHorizontal = transform.right * movX;
            Vector3 movVertical = transform.forward * movZ;
            Vector3 _velocity = (movHorizontal + movVertical);
            Vector3 velocity = new Vector3(Mathf.Clamp(_velocity.x, -Mathf.Abs(_velocity.normalized.x), Mathf.Abs(_velocity.normalized.x)), 0, Mathf.Clamp(_velocity.z, -Mathf.Abs(_velocity.normalized.z), Mathf.Abs(_velocity.normalized.z))) * speed;
            motor.Move(velocity);
        }

        if (motor.canRot)
        {
            float rotY = Input.GetAxis(input.camXAxis);
            Vector3 rotation = new Vector3(0f, rotY, 0f) * motor.sensitivityY;
            motor.Rotate(rotation);

            float rotX = Input.GetAxis(input.camYAxis) * motor.sensitivityX;
            motor.RotateCam(rotX);
        }
        #endregion

        if (motor.IsGrounded() && !isCrouching && !isLaying && !isSliding)
        {
            if (Input.GetKey(input.sprintKey) && movZ > .9f && movX < .75f && movX > -.75f)
            {
                speed = motor.sprintSpeed;
                if(weapon.curWeaponObj == null || !weapon.curWeaponObj.GetComponent<_WeaponShoot>().isScoped && weapon.curWeaponObj != null)    
                    cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, _GameManager._GM.normalFov + 5, .5f);
            }
            else if (movZ < .75f || movX > .75f || movX < -.75f)
            {
                speed = motor.walkSpeed;
                if (weapon.curWeaponObj == null || !weapon.curWeaponObj.GetComponent<_WeaponShoot>().isScoped && weapon.curWeaponObj != null)
                    cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, _GameManager._GM.normalFov, .5f);
            }
            else if (movZ < 0)
            {
                speed = motor.backWalkSpeed;
                if (weapon.curWeaponObj == null || !weapon.curWeaponObj.GetComponent<_WeaponShoot>().isScoped && weapon.curWeaponObj != null)
                    cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, _GameManager._GM.normalFov, .5f);
            }
        }
        else if (!motor.IsGrounded())
        {
            if (Input.GetKey(input.sprintKey) && movZ > .9f && movX < .75f && movX > -.75f)
                speed = motor.sprintJumpSpeed;
            else if (movZ < .75f || movX > .75f || movX < -.75f)
                speed = motor.jumpSpeed;
            else if (movZ < 0)
                speed = motor.backJumpSpeed;
        }
        
        if (input.connectedGamepad == ConnectedGamepad.Mouse_Keyboard)
        {
            if (Input.GetKeyDown(input.layKey) && motor.IsGrounded())
                Lay();
            if (Input.GetKeyDown(input.jumpKey) && motor.IsGrounded() && !isLaying && !isCrouching && motor.canMove)
                motor.Jump();
            if (Input.GetKeyDown(input.crouchKey) && motor.IsGrounded() && speed != motor.sprintSpeed)
                Crouch();
        }
        else
        {
            if (Input.GetKey(input.crouchKey) && motor.IsGrounded())
            {
                holdTime += 1;
                if ((holdTime > 15) && !isLaying)
                    Lay();
            }
            else if (Input.GetKeyUp(input.crouchKey))
                StartCoroutine(PsCrouch());

            if (Input.GetKeyUp(input.crouchKey) && (holdTime < 15) && motor.IsGrounded() && speed != motor.sprintSpeed)
                Crouch();
            else if (Input.GetKeyDown(input.jumpKey) && motor.IsGrounded() && !isLaying && !isCrouching && motor.canMove)
                motor.Jump();
        }

        if (Input.GetKeyDown(input.jumpKey) && motor.IsGrounded() && isCrouching && !isLaying)
            Crouch();
        else if (Input.GetKeyDown(input.jumpKey) && motor.IsGrounded() && !isCrouching && isLaying)
            Lay();
        else if (Input.GetKeyDown(input.sprintKey) && isCrouching)
            Crouch();
        else if (Input.GetKeyDown(input.sprintKey) && isLaying)
            Lay();
        else if (Input.GetKeyDown(input.crouchKey) && speed == motor.sprintSpeed && motor.IsGrounded() && canSlide)
            StartCoroutine(_Slide());
    }

    IEnumerator PsCrouch()
    {
        yield return new WaitForSeconds(0.01f);
        holdTime = 0;
    }

    IEnumerator _Slide()
    {
        isSliding = !isSliding;
        motor.pAnim.Slide();
        CheckSneakState();
        motor.canMove = false;
        yield return new WaitForEndOfFrame();
        SetCollider(1.1f);
        motor._Slide();
        yield return new WaitForSeconds(motor.slideTime);
        motor.rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
        motor.rb.isKinematic = true;
        Crouch();
        motor.rb.isKinematic = false;
        motor.rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        yield return new WaitForEndOfFrame();
        motor.canMove = true;
    }

    void Lay()
    {
        isLaying = !isLaying;
        motor.pAnim.Lay();
        CheckSneakState();

        if (isLaying)
        {
            speed = motor.laySpeed;
            SetCollider(.3f, 2, .3f, 2, -.9f);
        }
        else
        {
            speed = motor.walkSpeed;
            SetCollider(1);
        }
    }

    void Crouch()
    {
        isCrouching = !isCrouching;
        motor.pAnim.Crouch();
        CheckSneakState();

        if (isCrouching)
        {
            speed = motor.crouchSpeed;
            SetCollider(.625f, 1.25f);
        }
        else
        {
            speed = motor.walkSpeed;
            SetCollider(1);
        }
    }

    void CheckSneakState()
    {
        if (isCrouching ||isLaying || isSliding)
            motor.sneaking = true;
        else if (!isCrouching || !isLaying || !isSliding)
            motor.sneaking = false;
    }

    void SetCollider(float yCenter, float height = 2, float radius = .4f, int direction = 1, float zCenter = 0)
    {
        col.height = height;
        col.radius = radius;
        col.direction = direction;
        col.center = new Vector3(0, yCenter, zCenter);
    }
}
