using MyStateMachine;
using UnityEngine;

namespace Actions.Player
{
    public class IdleToRun : AbstractTrigger
    {
        public override bool Predicate()
        {
            var x = Input.GetAxis("Horizontal");
            return Mathf.Abs(x) > 0;
        }
    }
}