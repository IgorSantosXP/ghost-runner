using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackGenerator : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private GameObject baseTrack;
    [SerializeField] private List<GameObject> tracks = new List<GameObject>();
    [SerializeField] private int tracksToSpawn = 5;
    private List<GameObject> spawnedTracks = new List<GameObject>();
    private float distanceToDestroy = 40f;

    private void Start()
    {
        GameObject firstTrack = Instantiate(baseTrack, transform.position, Quaternion.identity);
        spawnedTracks.Add(firstTrack);

        SpawnTracks();
    }

    private void Update()
    {
        if (spawnedTracks.Count > 0)
        {
            DestroyTracks();
        }


        if (spawnedTracks.Count <= 5)
        {
            SpawnTracks();
        }
    }

    private void SpawnTracks()
    {
        for (int i = 0; i < tracksToSpawn; i++)
        {
            GameObject lastTrack = spawnedTracks[spawnedTracks.Count -1];
            Track lastTrackComponent = lastTrack.GetComponent<Track>();
            Vector3 nextTrackPosition = lastTrack.transform.position;
            nextTrackPosition.z += lastTrackComponent.EndPoint.position.z - lastTrackComponent.StartPoint.position.z;
            GameObject track = tracks[Random.Range(0, tracks.Count)];

            GameObject nextTrack = Instantiate(track, nextTrackPosition, Quaternion.identity);
            spawnedTracks.Add(nextTrack);
        }
    }

    private void DestroyTracks()
    {
        if(player.transform.position.z >= spawnedTracks[0].transform.position.z + distanceToDestroy)
        {
            GameObject track = spawnedTracks[0];
            spawnedTracks.RemoveAt(0);
            Destroy(track);
        }
    }

}
