using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private Animator animator;

    private void Update()
    {
        animator.SetBool("IsJumping", player.IsJumping);
        if (player.IsJumping)
        {
            animator.SetFloat("JumpPercent", player.JumpPercent);
        }
    }
}
