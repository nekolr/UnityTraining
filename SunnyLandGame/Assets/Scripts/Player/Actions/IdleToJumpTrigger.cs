using System;
using MyStateMachine;
using UnityEngine;

namespace Player.Actions
{
    public class IdleToJumpTrigger : AbstractTrigger
    {
        public override bool Predicate()
        {
            return Input.GetButtonDown("Jump");
        }
    }
}