using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PlayerController : MonoBehaviour
{ 
    [SerializeField] private float forwardSpeed = 10f;
    [SerializeField] private float laneDistanceX = 1.5f;
    [SerializeField] private float horizontalSpeed = 10f;

    [Header("Jump")]
    [SerializeField] private float jumpDistanceZ = 5;
    [SerializeField] private float jumpHeightY = 1;
    [SerializeField] private float jumpLerpSpeed = 20;

    [Header("Roll")]
    [SerializeField] private float rollDistanceZ = 5;
    [SerializeField] private Collider regularCollider;
    [SerializeField] private Collider rollCollider;

    public Vector3 initialPosition { get; private set; }
    public bool IsJumping { get; private set; }
    public bool IsRolling { get; private set; }
    public float JumpPercent { get; private set; }
    public float JumpDuration => jumpDistanceZ / forwardSpeed;
    public float RollDuration => rollDistanceZ / forwardSpeed;
    private float LeftLaneX => initialPosition.x - laneDistanceX;
    private float RightLaneX => initialPosition.x + laneDistanceX;
    private float targetPositionX;
    private float jumpStartZ;
    private float rollStartZ;

    private bool canJump => !IsJumping;
    private bool canRoll => !IsRolling;

    private bool isDead = false;

    private void Awake()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        if (!isDead)
        {
            ProcessInput();
        }

        Vector3 position = transform.position;

        position.z += forwardSpeed*Time.deltaTime;
        position.x = ProcessLaneMovement();
        position.y = ProcessJump();
        ProcessRoll();

        transform.position = position;
    }

    private void ProcessInput()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            targetPositionX -= laneDistanceX;
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            targetPositionX += laneDistanceX;
        }
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && canJump)
        {
            StartJump();
        }
        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && canRoll)
        {
            StartRoll();
        }

        targetPositionX = Mathf.Clamp(targetPositionX, LeftLaneX, RightLaneX);
    }

    private void StartJump()
    {
        IsJumping = true;
        jumpStartZ = transform.position.z;
        StopRoll();
    }

    private void StopJump()
    {
        IsJumping = false;
        JumpPercent = 0;
    }

    private void StartRoll()
    {
        IsRolling = true;
        rollStartZ = transform.position.z;
        regularCollider.enabled = false;
        rollCollider.enabled = true;
        StopJump();
    }

    private void StopRoll()
    {
        IsRolling = false;
        rollCollider.enabled = false;
        regularCollider.enabled = true;
    }

    private float ProcessLaneMovement()
    {
        return Mathf.Lerp(transform.position.x, targetPositionX, horizontalSpeed*Time.deltaTime);
    }

    private float ProcessJump()
    {
        float deltaY = 0;
        if (IsJumping)
        {
            float jumpCurrentProgress = transform.position.z - jumpStartZ;
            JumpPercent = jumpCurrentProgress / jumpDistanceZ;
            if (JumpPercent >= 1)
            {
                StopJump();
            }
            else
            {
                deltaY = Mathf.Sin(Mathf.PI * JumpPercent) * jumpHeightY;
            }
        }
        float targetPositionY = initialPosition.y + deltaY;
        return Mathf.Lerp(transform.position.y, targetPositionY, Time.deltaTime * jumpLerpSpeed);
    }

    private void ProcessRoll()
    {
        if (IsRolling)
        {
            float rollPercent = (transform.position.z - rollStartZ) / rollDistanceZ;
            if (rollPercent >= 1)
            {
                StopRoll();
            }
        }
    }

    public void Die()
    {
        forwardSpeed = 0;
        horizontalSpeed = 0;
        targetPositionX = transform.position.x;
        StopRoll();
        StopJump();
        isDead = true;
        regularCollider.enabled = false;
        rollCollider.enabled = false;
    }
}
