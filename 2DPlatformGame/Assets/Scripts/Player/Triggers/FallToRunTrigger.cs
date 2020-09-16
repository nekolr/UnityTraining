using System;
using FSM;
using UnityEngine;

namespace Player.Triggers
{
    public class FallToRunTrigger : AbstractTrigger
    {
        public FallToRunTrigger(Enum triggerID) : base(triggerID)
        {
        }

        public override bool Predicate(StateMachine stateMachine)
        {
            Rigidbody2D rigidbody2D = stateMachine.GetComponent<Rigidbody2D>();
            return Mathf.Abs(rigidbody2D.velocity.y) < Mathf.Epsilon
                   && Mathf.Abs(Input.GetAxis("Horizontal")) > Mathf.Epsilon;
        }
    }
}