using System.Collections;
using MyStateMachine;
using UnityEngine;
using Utils;

namespace Enemy.Frog.States
{
    public class IdleState : AbstractState
    {
        private readonly CapsuleCollider2D _capsuleCollider2D;
        private readonly LayerMask _layerMask;

        public IdleState(FrogEntry enemyEntry) : base(enemyEntry.StateMachine, enemyEntry.StateDictionary)
        {
            _capsuleCollider2D = enemyEntry.GetComponent<CapsuleCollider2D>();
            _layerMask = enemyEntry.layerMask;
        }

        public override void ExecuteByUpdate()
        {
            DelayInvoke.StartCoroutine(TransitionTrigger());
        }

        IEnumerator TransitionTrigger()
        {
            yield return new WaitForSeconds(0.5f);
            if (IsOnTheGround())
            {
                StateMachine.ChangeState(StateDictionary[StateID.Jump]);
            }
        }

        private bool IsOnTheGround()
        {
            return _capsuleCollider2D.IsTouchingLayers(_layerMask);
        }
    }
}