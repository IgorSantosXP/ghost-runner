using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private float armZ;
    private void LateUpdate()
    {
        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = player.transform.position;

        currentPosition.z = targetPosition.z + armZ;

        transform.position = currentPosition;
    }
}
