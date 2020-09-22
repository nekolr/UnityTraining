using System;
using MyStateMachine;
using UnityEngine;

namespace Player.Actions
{
    public class FallToJumpTrigger : AbstractTrigger
    {
        private PlayerEntry _playerEntry;

        private void Awake()
        {
            _playerEntry = GetComponent<PlayerEntry>();
        }

        public override bool Predicate()
        {
            return Input.GetButtonDown("Jump") && _playerEntry.jumpCount < 2;
        }
    }
}