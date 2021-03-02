using System;
using UnityEngine;

namespace SpaceBum
{
    public class GroundedSMB : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.GetComponent<CharacterBrain>().OnGroundedAnimationEnter();
        }
    }
}
