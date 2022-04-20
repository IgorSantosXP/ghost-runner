using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollAnimationState : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AnimatorClipInfo[] clips = animator.GetNextAnimatorClipInfo(layerIndex);

        //TODO: Vc ta de sacanagem neh?!?
        PlayerController player = animator.transform.parent.GetComponent<PlayerController>();
        if (player != null && clips.Length > 0)
        {
            AnimatorClipInfo clipInfo = clips[0];
            float multiplier = clipInfo.clip.length / player.RollDuration;
            animator.SetFloat(PlayerAnimationConstants.RollMultiplier, multiplier);
        }
    }
}
