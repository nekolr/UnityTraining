using FSM;
using UnityEngine;

namespace Player.Triggers
{
    public class RunToIdleTrigger : AbstractTrigger
    {
        public RunToIdleTrigger(TriggerID triggerID) : base(triggerID)
        {
        }

        public override bool Predicate(FSM.StateMachine stateMachine)
        {
            return Mathf.Abs(Input.GetAxis("Horizontal")) < 0.01f;
        }
    }
}