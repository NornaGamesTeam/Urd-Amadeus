using UnityEngine;
using Urd.Game.SkillTrees;

public class AnimatorSkillBehavior : StateMachineBehaviour
{
    [SerializeReference] 
    private SkillConfig _skillConfig;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        float duration = _skillConfig.Model.SkillAnimationModel.Duration <= 0
            ? _skillConfig.Model.Duration
            : _skillConfig.Model.SkillAnimationModel.Duration;
        
        var speed = stateInfo.length / (duration);
        speed *= (float)_skillConfig.Model.SkillAnimationModel.AnimationLoops;
        animator.speed = speed;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.speed = 1;        
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
