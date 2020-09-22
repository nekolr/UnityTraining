﻿using MyStateMachine;
using UnityEngine;

namespace Player.Actions
{
    public class RunToIdleTrigger : AbstractTrigger
    {
        public override bool Predicate()
        {
            return Mathf.Abs(Input.GetAxis("Horizontal")) < 0.01f;
        }
    }
}