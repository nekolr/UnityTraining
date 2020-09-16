using System;
using FSM;
using UnityEngine;

namespace Player.Triggers
{
    public class RunToJumpTrigger : AbstractTrigger
    {
        public RunToJumpTrigger(Enum triggerID) : base(triggerID)
        {
        }

        public override bool Predicate(StateMachine stateMachine)
        {
            float x = Input.GetAxis("Horizontal");
            BoxCollider2D boxCollider = stateMachine.GetComponent<BoxCollider2D>();
            return Mathf.Abs(x) > 0 && Input.GetButtonDown("Jump") && IsGrounded(boxCollider);
        }
        
        private bool IsGrounded(BoxCollider2D boxCollider)
        {
            return boxCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
        }
    }
}