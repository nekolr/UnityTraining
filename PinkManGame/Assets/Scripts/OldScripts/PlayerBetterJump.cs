using UnityEngine;

namespace OldScripts
{
    public class PlayerBetterJump : MonoBehaviour
    {
        // 下降时速度变化率
        public float fallMultiplier = 2.5f;
        // 向上跳时速度变化率
        public float lowJumpMultiplier = 2f;

        private Rigidbody2D _rigidbody2D;

        // Start is called before the first frame update
        void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        void FixedUpdate()
        {
            // 刚体在下降
            if (_rigidbody2D.velocity.y < 0)
            {
                // 调整刚体受重力的影响程度
                _rigidbody2D.gravityScale = fallMultiplier;
            } 
            // 刚体在上升，并且并没有一直按着跳跃键
            else if (_rigidbody2D.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                _rigidbody2D.gravityScale = lowJumpMultiplier;
            }
            // 刚体在上升，并且一直按着跳跃键
            else if (_rigidbody2D.velocity.y > 0 && Input.GetButton("Jump"))
            {
                // 刚体受重力的影响为原值
                _rigidbody2D.gravityScale = 0.5f;
            }
        }
    }
}
