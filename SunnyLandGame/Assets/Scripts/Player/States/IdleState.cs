using System;
using MyStateMachine;
using UnityEngine;

namespace Player.States
{
    public class IdleState : AbstractState
    {
        private readonly Animator _animator;
        private readonly Vector2 _playerSize;
        private readonly Vector2 _boxSize;
        private readonly Transform _transform;
        private readonly LayerMask _layerMask;

        public IdleState(Enum stateID, PlayerEntry playerEntry) : base(stateID, playerEntry.StateMachine)
        {
            _animator = playerEntry.GetComponent<Animator>();
            _transform = playerEntry.transform;
            // 玩家精灵边框的大小
            _playerSize = playerEntry.GetComponent<SpriteRenderer>().bounds.size;
            // 碰撞检测盒子的大小，使用玩家的大小 40%，同时高度设置为 0.5
            _boxSize = new Vector2(_playerSize.x * 0.4f, PlayerVariables.BoxHeight);
            _layerMask = playerEntry.layerMask;
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
                StateMachine.ChangeState(StateMachine.StateDictionary[StateID.Run]);
            }

            if (Input.GetButtonDown("Jump") && IsOnTheGround())
            {
                StateMachine.ChangeState(StateMachine.StateDictionary[StateID.Jump]);
            }
            
            if (PlayerVariables.IsHurt)
            {
                StateMachine.ChangeState(StateMachine.StateDictionary[StateID.Hurt]);
            }

            if (Input.GetButton("Crouch"))
            {
                StateMachine.ChangeState(StateMachine.StateDictionary[StateID.Crouch]);
            }
        }
        
        private bool IsOnTheGround()
        {
            // 先将碰撞盒子的位置移动到玩家脚部位置
            // transform.position 的位置是精灵的正中心，如果盒子移到这里是无法进行碰撞检测的，需要移动到脚部
            Vector2 jumpBoxPosition = (Vector2) _transform.position + (Vector2.down * _playerSize * 0.5f);
            return Physics2D.OverlapBox(jumpBoxPosition, _boxSize, 0, _layerMask) != null;
        }
    }
}