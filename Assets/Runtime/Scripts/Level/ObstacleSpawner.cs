using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> obstacles = new List<GameObject>();
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();

    private void Start()
    {
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            Vector3 spawnPoint = spawnPoints[i].position;
            int obstacleIndex = Random.Range(0, obstacles.Count);

            Instantiate(obstacles[obstacleIndex], spawnPoint, Quaternion.identity, transform);
        }
    }
}
