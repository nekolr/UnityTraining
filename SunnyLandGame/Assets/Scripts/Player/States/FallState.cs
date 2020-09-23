using System.Collections.Generic;
using MyStateMachine;
using UnityEngine;

namespace Player.States
{
    public class FallState : AbstractState
    {
        private readonly Animator _animator;
        private readonly StateMachine _stateMachine;
        private readonly Rigidbody2D _rigidbody2D;
        private readonly CapsuleCollider2D _capsuleCollider2D;
        private readonly LayerMask _layerMask;
        private readonly Dictionary<StateID, AbstractState> _stateDictionary;

        public FallState(PlayerEntry playerEntry)
        {
            _animator = playerEntry.GetComponent<Animator>();
            _rigidbody2D = playerEntry.GetComponent<Rigidbody2D>();
            _capsuleCollider2D = playerEntry.GetComponent<CapsuleCollider2D>();
            _layerMask = playerEntry.layerMask;
            _stateMachine = playerEntry.StateMachine;
            _stateDictionary = playerEntry.StateDictionary;
        }

        public override void Enter()
        {
            _animator.SetBool("isFall", true);
        }

        public override void ExecuteByUpdate()
        {
            TransitionTrigger();
            Run();
        }

        private void TransitionTrigger()
        {
            var isOnTheGround = IsOnTheGround();
            var direction = Input.GetAxisRaw("Horizontal");
            
            if (direction == 0f && isOnTheGround)
            {
                _stateMachine.ChangeState(_stateDictionary[StateID.Idle]);
            }

            if (direction != 0f && isOnTheGround)
            {
                _stateMachine.ChangeState(_stateDictionary[StateID.Run]);
            }

            if (Input.GetButtonDown("Jump") && PlayerVariables.JumpCount < 2)
            {
                _stateMachine.ChangeState(_stateDictionary[StateID.Jump]);
            }
        }
        
        private bool IsOnTheGround()
        {
            return _capsuleCollider2D.IsTouchingLayers(_layerMask);
        }

        private void Run()
        {
            float direction = Input.GetAxisRaw("Horizontal");
            // 速度向量
            _rigidbody2D.velocity = new Vector2(direction * PlayerVariables.Speed, _rigidbody2D.velocity.y);
            if (direction != 0)
            {
                // 方向变换
                _rigidbody2D.transform.localScale = new Vector3(direction, 1, 1);
            }
        }

        public override void Exit()
        {
            _animator.SetBool("isFall", false);
        }
    }
}