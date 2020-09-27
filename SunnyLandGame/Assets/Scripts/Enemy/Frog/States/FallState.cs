using System;
using MyStateMachine;
using UnityEngine;

namespace Enemy.Frog.States
{
    public class FallState : AbstractState
    {
        private readonly Animator _animator;
        private readonly CapsuleCollider2D _capsuleCollider2D;
        private readonly LayerMask _layerMask;

        public FallState(Enum stateID, FrogEntry enemyEntry) : base(stateID, enemyEntry.StateMachine)
        {
            _animator = enemyEntry.GetComponent<Animator>();
            _capsuleCollider2D = enemyEntry.GetComponent<CapsuleCollider2D>();
            _layerMask = enemyEntry.layerMask;
        }

        public override void Enter()
        {
            _animator.SetBool("isFall", true);
        }

        public override void ExecuteByUpdate()
        {
            TransitionTrigger();
        }

        private void TransitionTrigger()
        {
            if (IsOnTheGround())
            {
                StateMachine.ChangeState(StateMachine.StateDictionary[FrogStateID.Idle]);
            }
        }

        private bool IsOnTheGround()
        {
            return _capsuleCollider2D.IsTouchingLayers(_layerMask);
        }

        public override void Exit()
        {
            _animator.SetBool("isFall", false);
        }
    }
}