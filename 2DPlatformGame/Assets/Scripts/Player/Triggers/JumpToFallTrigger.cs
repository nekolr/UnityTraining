using System;
using FSM;
using UnityEngine;

namespace Player.Triggers
{
    public class JumpToFallTrigger : AbstractTrigger
    {
        public JumpToFallTrigger(Enum triggerID) : base(triggerID)
        {
        }

        public override bool Predicate(StateMachine stateMachine)
        {
            Rigidbody2D rigidbody2D = stateMachine.GetComponent<Rigidbody2D>();
            return rigidbody2D.velocity.y < 0;
        }
    }
}