using System.Collections.Generic;
using MyStateMachine;
using UnityEngine;

namespace Player.States
{
    public class IdleState : AbstractState
    {
        private readonly Animator _animator;
        private readonly CapsuleCollider2D _capsuleCollider2D;
        private readonly StateMachine _stateMachine;
        private readonly LayerMask _layerMask;
        private readonly Dictionary<StateID, AbstractState> _stateDictionary;

        public IdleState(PlayerEntry playerEntry)
        {
            _animator = playerEntry.GetComponent<Animator>();
            _capsuleCollider2D = playerEntry.GetComponent<CapsuleCollider2D>();
            _layerMask = playerEntry.layerMask;
            _stateMachine = playerEntry.StateMachine;
            _stateDictionary = playerEntry.StateDictionary;
        }

        public override void Enter()
        {
            _animator.SetBool("isRun", false);
            // 重置跳跃次数
            PlayerVariables.JumpCount = 0;
        }

        public override void ExecuteByUpdate()
        {
            TransitionTrigger();
        }

        private void TransitionTrigger()
        {
            if (Input.GetAxisRaw("Horizontal") != 0f)
            {
                _stateMachine.ChangeState(_stateDictionary[StateID.Run]);
            }

            if (Input.GetButtonDown("Jump") && IsOnTheGround())
            {
                _stateMachine.ChangeState(_stateDictionary[StateID.Jump]);
            }
        }
        
        private bool IsOnTheGround()
        {
            return _capsuleCollider2D.IsTouchingLayers(_layerMask);
        }
    }
}