using MyStateMachine;
using UnityEngine;

namespace Player.States
{
    public class IdleState : AbstractState
    {
        private PlayerEntry _playerEntry;
        
        public IdleState(PlayerEntry playerEntry)
        {
            _playerEntry = playerEntry;
        }

        public override void Enter()
        {
            Animator animator = _playerEntry.GetComponent<Animator>();
            animator.SetBool("isRun", false);
        }
    }
}