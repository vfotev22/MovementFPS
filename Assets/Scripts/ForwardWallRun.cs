using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ForwardWallRun : MonoBehaviour
{
    [Header("Forward Wall Run")]
    public LayerMask whatIsWall;
    public float wallCheckDistance = 1.5f;
    public float wallRunDuration = 0.5f;
    public float wallRunUpSpeed = 10f;
    public float stickToWallForce = 5f;

    [Header("Input")]
    public KeyCode wallRunKey = KeyCode.Space;

    [Header("Requirements")]
    public Transform orientation;
    public PlayerCam cam;

    [Header("Camera Effects")]
    public float wallRunFov = 90f;
    public float defaultFov = 80f;

    private Rigidbody rb;
    private PlayerMovement pm;

    private bool isWallRunningUp;
    private float wallRunTimer;

    private RaycastHit frontWallHit;
    private bool wallInFront;

    private bool hasUsedWallRunUp;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        CheckForFrontWall();

        if (pm != null && pm.grounded)
        {
            hasUsedWallRunUp = false;

            if (isWallRunningUp)
            {
                StopWallRunUp();
            }
        }

        if (pm != null &&
            !pm.grounded &&
            wallInFront &&
            Input.GetKeyDown(wallRunKey) &&
            !isWallRunningUp &&
            !hasUsedWallRunUp)
        {
            StartWallRunUp();
        }

        // handle timer
        if (isWallRunningUp)
        {
            wallRunTimer -= Time.deltaTime;
            if (wallRunTimer <= 0f)
            {
                StopWallRunUp();
            }
        }
    }

    private void FixedUpdate()
    {
        if (isWallRunningUp)
        {
            DoWallRunUpMovement();
        }
    }

    private void CheckForFrontWall()
    {
        wallInFront = Physics.Raycast(
            transform.position,
            orientation.forward,
            out frontWallHit,
            wallCheckDistance,
            whatIsWall
        );
    }

    private void StartWallRunUp()
    {
        isWallRunningUp = true;
        wallRunTimer = wallRunDuration;
        hasUsedWallRunUp = true;

        if (pm != null)
        {
            pm.wallrunning = true;
        }

        rb.velocity = new UnityEngine.Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (cam != null)
        {
            cam.DoFov(wallRunFov);
        }

        rb.useGravity = false;
    }

    private void DoWallRunUpMovement()
    {
        if (!wallInFront)
        {
            StopWallRunUp();
            return;
        }

        UnityEngine.Vector3 currentVel = rb.velocity;
        currentVel.y = wallRunUpSpeed;
        rb.velocity = currentVel;

        UnityEngine.Vector3 wallNormal = frontWallHit.normal;
        rb.AddForce(-wallNormal * stickToWallForce, ForceMode.Force);
    }

    private void StopWallRunUp()
    {
        isWallRunningUp = false;

        if (pm != null)
        {
            cam.DoFov(defaultFov);
        }

        rb.useGravity = true;

        if (cam != null)
        {
            cam.DoFov(defaultFov);
        }
    }
}
