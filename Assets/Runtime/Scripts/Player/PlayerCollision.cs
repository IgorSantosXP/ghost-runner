using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameMode gamemode;
    private PlayerController player;

    private void Awake()
    {
        player = GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Oi");
        Obstacle obstacle = other.GetComponent<Obstacle>();
        if (obstacle != null)
        {
            Debug.Log("Coé");
            player.enabled = false;
            animator.SetTrigger(PlayerAnimationConstants.DieTrigger);
            gamemode.OnGameOver();
        }
    }
}
