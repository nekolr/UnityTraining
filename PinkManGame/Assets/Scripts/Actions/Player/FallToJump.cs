using System;
using MyStateMachine;
using UnityEngine;

namespace Actions.Player
{
    public class FallToJump : AbstractTrigger
    {
        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public override bool Predicate()
        {
            bool isJump = _rigidbody2D.velocity.y > 0;
            // Debug.Log("--------------FallToJump: " + isJump);

            return isJump;
        }
    }
}