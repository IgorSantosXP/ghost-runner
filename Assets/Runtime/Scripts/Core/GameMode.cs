using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private Animator animator;

    private void Awake()
    {
        player.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("StartGameTrigger");
            player.enabled = true;
        }
    }
}
