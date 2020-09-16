using FSM;
using UnityEngine;

namespace Player.Triggers
{
    public class IdleToRunTrigger : AbstractTrigger
    {
        public IdleToRunTrigger(TriggerID triggerID) : base(triggerID)
        {
        }

        public override bool Predicate(FSM.StateMachine stateMachine)
        {
            return Mathf.Abs(Input.GetAxis("Horizontal")) > 0;
        }
    }
}