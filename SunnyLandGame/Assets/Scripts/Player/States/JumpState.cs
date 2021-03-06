﻿using System;
using MyStateMachine;
using UnityEngine;

namespace Player.States
{
    public class JumpState : AbstractState
    {
        private readonly Animator _animator;
        private readonly Rigidbody2D _rigidbody2D;
        private readonly AudioSource _jumpAudioSource;

        public JumpState(Enum stateID, PlayerEntry playerEntry) : base(stateID, playerEntry.StateMachine)
        {
            _animator = playerEntry.GetComponent<Animator>();
            _rigidbody2D = playerEntry.GetComponent<Rigidbody2D>();
            _jumpAudioSource = playerEntry.jumpAudioSource;
        }

        public override void Enter()
        {
            _animator.SetBool("isJump", true);
            PlayerVariables.IsJump = true;
        }

        public override void ExecuteByUpdate()
        {
            // 二段跳触发检测
            DoubleJumpTrigger();
            // 转换触发检测
            TransitionTrigger();
            // 跳动过程中的左右位移
            Run();
        }

        private void DoubleJumpTrigger()
        {
            // 在跳跃状态下再次按跳跃
            if (Input.GetButtonDown("Jump") && PlayerVariables.JumpCount < 2)
            {
                PlayerVariables.IsJump = true;
            }
        }

        public override void ExecuteByFixedUpdate()
        {
            if (PlayerVariables.IsJump && PlayerVariables.JumpCount < 2)
            {
                Jump();
                PlayerVariables.IsJump = false;
                PlayerVariables.JumpCount++;
            }
        }

        private void Jump()
        {
            _jumpAudioSource.Play();
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, PlayerVariables.JumpVelocity);
        }

        private void TransitionTrigger()
        {
            // 在下落过程中跳跃时，会从下落状态转换到跳跃状态，可能会出现虽然跳跃标记 IsJump 为 true，但是此时刚体的 y 轴速度小于 0
            // 由于一般情况下 Update 的执行频率要高于 FixedUpdate，所以会导致还没有执行 FixedUpdate 中的方法就已经改变了状态为下落 
            if (_rigidbody2D.velocity.y < 0 && !PlayerVariables.IsJump)
            {
                StateMachine.ChangeState(StateMachine.StateDictionary[StateID.Fall]);
            }
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
            _animator.SetBool("isJump", false);
        }
    }
}