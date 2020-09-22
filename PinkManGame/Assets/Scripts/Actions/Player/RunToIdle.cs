using MyStateMachine;
using UnityEngine;

namespace Actions.Player
{
    public class RunToIdle : AbstractTrigger
    {
        public override bool Predicate()
        {
            var x = Input.GetAxis("Horizontal");
            // x < 0.001f && x > -0.001f
            // return x < Mathf.Epsilon && x > -Mathf.Epsilon;
            return Mathf.Abs(Input.GetAxis("Horizontal")) < Mathf.Epsilon;
        }
    }
}