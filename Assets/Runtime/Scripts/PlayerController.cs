using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PlayerController : MonoBehaviour
{ 
    [SerializeField] private float forwardSpeed = 5f;
    [SerializeField] private float laneDistanceX = 1.5f;
    [SerializeField] private float horizontalSpeed = 10f;

    public Vector3 initialPosition { get; private set; }
    private float LeftLaneX => initialPosition.x - laneDistanceX;
    private float RightLaneX => initialPosition.x + laneDistanceX;
    private float targetPositionX;

    private void Awake()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        ProcessInput();

        Vector3 position = transform.position;

        position.z += forwardSpeed*Time.deltaTime;
        position.x = ProcessLaneMovement();

        transform.position = position;
    }

    private void ProcessInput()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            targetPositionX -= laneDistanceX;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            targetPositionX += laneDistanceX;
        }

        targetPositionX = Mathf.Clamp(targetPositionX, LeftLaneX, RightLaneX);
    }

    private float ProcessLaneMovement()
    {
        return Mathf.Lerp(transform.position.x, targetPositionX, horizontalSpeed*Time.deltaTime);
    }
}
