using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackGenerator : MonoBehaviour
{
    [SerializeField] private GameObject track;
    [SerializeField] private int initialTrackNumber = 5;
    private List<GameObject> tracks = new List<GameObject>();

    private void Start()
    {
        GameObject firstTrack = Instantiate(track, transform.position, Quaternion.identity);

        tracks.Add(firstTrack);

        for (int i = 0; i < initialTrackNumber; i++)
        {
            GameObject lastTrack = tracks[tracks.Count -1];
            Track lastTrackComponent = lastTrack.GetComponent<Track>();
            Vector3 nextTrackPosition = lastTrack.transform.position;
            nextTrackPosition.z += (lastTrackComponent.EndPoint.position.z - lastTrackComponent.StartPoint.position.z)/2;

            Debug.Log(nextTrackPosition);

            GameObject nextTrack = Instantiate(track, nextTrackPosition, Quaternion.identity);
            tracks.Add(nextTrack);
        }
    }

}
