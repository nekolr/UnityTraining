using System.Collections;
using Enemy.Frog;
using MyStateMachine;
using UnityEngine;
using Utils;

namespace Player.States
{
    public class HurtState : AbstractState
    {
        private readonly Animator _animator;
        private readonly Rigidbody2D _rigidbody2D;
        private readonly AudioSource _hurtAudioSource;

        public HurtState(PlayerEntry playerEntry) : base(playerEntry.StateMachine, playerEntry.StateDictionary)
        {
            _animator = playerEntry.GetComponent<Animator>();
            _rigidbody2D = playerEntry.GetComponent<Rigidbody2D>();
            _hurtAudioSource = playerEntry.hurtAudioSource;
        }

        public override void Enter()
        {
            _animator.SetBool("isHurt", true);
        }

        public override void ExecuteByUpdate()
        {
            if (PlayerVariables.IsHurt)
            {
                Hurt();
                PlayerVariables.IsHurt = false;
                // 延时调用
                DelayInvoke.StartCoroutine(TransitionTrigger());
            }
        }

        IEnumerator TransitionTrigger()
        {
            yield return new WaitForSeconds(1f);
            if (!PlayerVariables.IsHurt)
            {
                StateMachine.ChangeState(StateDictionary[StateID.Idle]);
            }
        }

        private void Hurt()
        {
            _hurtAudioSource.Play();
            
            // 受伤时反向弹出一段距离
            var direction = Input.GetAxisRaw("Horizontal");
            
            if (direction == 0)
            {
                _rigidbody2D.velocity = new Vector2(-5f * FrogVariables.Direction, 0);
            }
            else
            {
                _rigidbody2D.velocity = new Vector2(direction * -5f, _rigidbody2D.velocity.y);
            }
        }

        public override void Exit()
        {
            _animator.SetBool("isHurt", false);
        }
    }
}