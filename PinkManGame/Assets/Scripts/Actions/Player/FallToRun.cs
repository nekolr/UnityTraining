using MyStateMachine;
using UnityEngine;

namespace Actions.Player
{
    public class FallToRun : AbstractTrigger
    {
        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public override bool Predicate()
        {
            return Mathf.Abs(_rigidbody2D.velocity.y) < Mathf.Epsilon
                   && Mathf.Abs(Input.GetAxis("Horizontal")) > Mathf.Epsilon;
        }
    }
}