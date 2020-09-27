using System;
using MyStateMachine;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Enemy.Frog.States
{
    public class DeathState : AbstractState
    {
        private readonly Animator _animator;
        private readonly GameObject _enemy;
        private readonly AudioSource _audioSource;
        private readonly CapsuleCollider2D _capsuleCollider2D;
        private readonly Rigidbody2D _rigidbody2D;

        public DeathState(Enum stateID, FrogEntry enemyEntry) : base(stateID, enemyEntry.StateMachine)
        {
            _animator = enemyEntry.GetComponent<Animator>();
            _audioSource = enemyEntry.GetComponent<AudioSource>();
            _capsuleCollider2D = enemyEntry.GetComponent<CapsuleCollider2D>();
            _rigidbody2D = enemyEntry.GetComponent<Rigidbody2D>();
            _enemy = enemyEntry.gameObject;
        }

        public override void Enter()
        {
            _animator.SetTrigger("isDeath");
            // 碰撞检测器失效
            _capsuleCollider2D.enabled = false;
            // 取消重力作用，不然由于碰撞检测器失效而掉出场景
            _rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
            // 播放声音
            _audioSource.Play();
            // 0.5 秒后销毁
            Object.Destroy(_enemy, 0.5f);
        }
    }
}