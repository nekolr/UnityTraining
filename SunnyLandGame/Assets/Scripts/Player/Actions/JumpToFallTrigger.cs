﻿using System;
using MyStateMachine;
using UnityEngine;

namespace Player.Actions
{
    public class JumpToFallTrigger : AbstractTrigger
    {
        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public override bool Predicate()
        {
            return _rigidbody2D.velocity.y < 0;
        }
    }
}