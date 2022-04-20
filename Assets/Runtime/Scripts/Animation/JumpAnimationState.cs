using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAnimationState : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //olha a duracao da animacao de pulo 
        AnimatorClipInfo[] clips = animator.GetNextAnimatorClipInfo(layerIndex);
        if (clips.Length > 0)
        {
            AnimatorClipInfo jumpClipInfo = clips[0];
            //olha a duracao do pulo do gameplay
            //TODO: Assumindo que o PlayerController esta no objeto pai. Resolver isso.
            PlayerController player = animator.transform.parent.GetComponent<PlayerController>();

            //setar o JumpMultiplier para que a duracao final da animacao de pulo seja = a duracao do pulo no gameplay
            float multiplier = jumpClipInfo.clip.length / player.JumpDuration;
            animator.SetFloat(PlayerAnimationConstants.JumpMultiplier, multiplier);
        }
    }
}
