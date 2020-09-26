using MyStateMachine;
using UnityEngine;

namespace Enemy.Frog.States
{
    public class DeathState : AbstractState
    {

        private readonly Animator _animator;
        private readonly GameObject _enemy;
        private readonly AudioSource _audioSource;

        public DeathState(FrogEntry enemyEntry) : base(enemyEntry.StateMachine, enemyEntry.StateDictionary)
        {
            _animator = enemyEntry.GetComponent<Animator>();
            _audioSource = enemyEntry.GetComponent<AudioSource>();
            _enemy = enemyEntry.gameObject;
        }

        public override void Enter()
        {
            _animator.SetTrigger("isDeath");
            _audioSource.Play();
            Object.Destroy(_enemy, 0.5f);
        }
    }
}