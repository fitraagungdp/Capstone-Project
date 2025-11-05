using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerControl : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [HideInInspector] public float walkSpeed;
    [HideInInspector] public float sprintSpeed;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public GameObject motorcycle;
    public GameObject mainCamera;
    public Transform orientation;
    public Transform playerOrientation;
    public Transform motorcycleOrientation;

    float horizontalInput;
    float verticalInput;

    public bool isMounted;
    public GameObject playerPackageBox;
    public GameObject playerObject;
    public GameObject playerSit;
    public GameObject motorcyclePackageBox;
    public bool isCarryingPackage = false;
    public Animator playerAnimator;
    public GameObject mountUI;

    [Header("SFX")]
    public AudioSource idleSfx;
    public AudioSource gasSfx;
    public AudioSource footstepsSfx;

    Vector3 moveDirection;

    Rigidbody rb;
    CapsuleCollider col;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        rb.freezeRotation = true;
        motorcycle.GetComponent<MotorcycleControl>().enabled = false;
        readyToJump = true;
        playerOrientation = orientation;
        isCarryingPackage = false;
        footstepsSfx.enabled = false;
        gasSfx.enabled = false;
        idleSfx.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isMounted)
            {
                DismountMotorcycle();
                orientation = playerOrientation;
            }
            else if (CanMountMotorcycle())
            {
                MountMotorcycle();
                orientation = motorcycleOrientation;
            }
        }

        if (!isMounted)
        {
            // ground check
            grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);
            MyInput();
            SpeedControl();

            bool isRunning = horizontalInput != 0 || verticalInput != 0;

            if (playerAnimator.GetBool("carryCheck"))
            {
                playerAnimator.SetBool("carryRunningCheck", isRunning);
            }
            else
            {
                playerAnimator.SetBool("runningCheck", isRunning);
            }

            footstepsSfx.enabled = isRunning;
            gasSfx.enabled = false;

            // handle drag
            if (grounded)
                rb.drag = groundDrag;
            else
                rb.drag = 0;
        }

        else
        {
            transform.localPosition = Vector3.zero;

            MyInput();
            SpeedControl();

            bool isMotorcycleRunning = horizontalInput != 0 || verticalInput != 0;

            gasSfx.enabled = isMotorcycleRunning;
            idleSfx.enabled = !isMotorcycleRunning;
            footstepsSfx.enabled = false;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");


        // when to jump
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {

        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on ground
        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // in air
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }

    private bool CanMountMotorcycle()
    {
        // Calculate the distance between the player and the motorcycle
        float distanceToMotorcycle = Vector3.Distance(transform.position, motorcycle.transform.position);

        // Set a maximum mounting distance (you can adjust this value)
        float maxMountingDistance = 2.0f;

        // Check if the player is close enough to the motorcycle to mount
        if (distanceToMotorcycle <= maxMountingDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void MountMotorcycle()
    {
        // Disable the player's movement and physics
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        col.enabled = false;
        if (isCarryingPackage)
        {
            // Deactivate the player's package box
            playerPackageBox.SetActive(false);

            // Activate the motorcycle's package box
            motorcyclePackageBox.SetActive(true);
        }

        // Set the player as a child of the motorcycle and position them correctly
        transform.parent = motorcycle.transform.Find("MountPoint");

        // Enable motorcycle control and set the player as mounted
        isMounted = true;
        motorcycle.GetComponent<MotorcycleControl>().enabled = true;
        playerObject.SetActive(false);
        playerSit.SetActive(true);
        mountUI.SetActive(false);
    }

    private void DismountMotorcycle()
    {
        // Unparent the player from the motorcycle
        transform.parent = null;

        // Enable the player's movement and physics
        rb.isKinematic = false;
        playerSit.SetActive(false);
        playerObject.SetActive(true);

        if (isCarryingPackage)
        {
            // Deactivate the motorcycle's package box
            motorcyclePackageBox.SetActive(false);

            // Activate the player's package box
            playerPackageBox.SetActive(true);
            playerAnimator.SetBool("carryCheck", true);
        }

        // Reset the current speed of the motorcycle before disabling the script
        motorcycle.GetComponent<MotorcycleControl>().ResetCurrentSpeed();


        // Disable motorcycle control and set the player as not mounted
        isMounted = false;
        motorcycle.GetComponent<MotorcycleControl>().enabled = false;
        //mainCamera.GetComponent<ThirdPersonCamMotorcycle>().enabled = false;
        col.enabled = true;
        orientation = playerOrientation;
        mountUI.SetActive(true);
    }

    public bool IsMounted
    {
        get { return isMounted; }
    }

    public void SetCarryingPackage(bool carrying)
    {
        footstepsSfx.enabled = false;
        isCarryingPackage = carrying;
        playerPackageBox.SetActive(carrying);
        playerAnimator.SetBool("carryCheck", true);
    }

    public void GetBoolCarryAnimationFalse()
    {
        playerAnimator.SetBool("carryCheck", false);
        playerAnimator.SetBool("carryRunningCheck", false);
    }

    public void GetBoolRunningAnimationFalse()
    {
        playerAnimator.SetBool("runningCheck", false);
    }
}