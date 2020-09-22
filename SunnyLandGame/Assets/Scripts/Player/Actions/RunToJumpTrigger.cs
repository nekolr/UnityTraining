using MyStateMachine;
using UnityEngine;

namespace Player.Actions
{
    public class RunToJumpTrigger : AbstractTrigger
    {
        public override bool Predicate()
        {
            return Mathf.Abs(Input.GetAxis("Horizontal")) > Mathf.Epsilon && Input.GetButtonDown("Jump");
        }
    }
}