using System;
using FSM;
using UnityEngine;

namespace Player.Triggers
{
    public class IdleToJumpTrigger : AbstractTrigger
    {
        public IdleToJumpTrigger(Enum triggerID) : base(triggerID)
        {
        }

        public override bool Predicate(StateMachine stateMachine)
        {
            return Input.GetButtonDown("Jump") && IsGrounded(stateMachine.GetComponent<BoxCollider2D>());
        }

        private bool IsGrounded(BoxCollider2D boxCollider)
        {
            return boxCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
        }
    }
}