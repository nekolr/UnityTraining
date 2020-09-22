using MyStateMachine;
using UnityEngine;

namespace States.Player
{
    public class IdleState : AbstractState
    {
        private Animator _animator;

        public IdleState(Animator animator)
        {
            _animator = animator;
        }

        public override void OnEnter()
        {
            _animator.SetBool("isRun", false);
        }
    }
}