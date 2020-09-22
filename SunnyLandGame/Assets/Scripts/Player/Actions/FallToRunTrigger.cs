using MyStateMachine;
using UnityEngine;

namespace Player.Actions
{
    public class FallToRunTrigger : AbstractTrigger
    {
        private BoxCollider2D _boxCollider2D;
        private LayerMask _layerMask;

        private void Awake()
        {
            _boxCollider2D = GetComponent<BoxCollider2D>();
            _layerMask = GetComponent<PlayerEntry>().layerMask;
        }


        public override bool Predicate()
        {
            return Mathf.Abs(Input.GetAxis("Horizontal")) > Mathf.Epsilon && IsOnTheGround();
        }

        private bool IsOnTheGround()
        {
            return _boxCollider2D.IsTouchingLayers(_layerMask);
        }
    }
}