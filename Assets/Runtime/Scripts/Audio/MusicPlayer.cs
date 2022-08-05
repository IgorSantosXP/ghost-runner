using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private List<AudioClip> musics = new List<AudioClip>();

    private AudioSource audioSource;

    private AudioSource AudioSource => audioSource == null ? audioSource = GetComponent<AudioSource>() : audioSource;


    public void PlayMusic()
    {
        AudioClip clip = musics[Random.Range(0, musics.Count)];

        AudioSource.clip = clip;
        AudioSource.loop = true;
        AudioSource.Play();
    }

}
