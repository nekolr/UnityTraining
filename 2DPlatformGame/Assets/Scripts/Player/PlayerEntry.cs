using FSM;
using UnityEngine;

namespace Player
{
    public class PlayerEntry : MonoBehaviour
    {
        /// <summary>
        /// 状态机
        /// </summary>
        private StateMachine stateMachine;

        /// <summary>
        /// 跳跃标记
        /// </summary>
        public bool IsJumpFlag;

        /// <summary>
        /// 下降标记
        /// </summary>
        public bool IsFallFlag;

        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            stateMachine = GetComponent<StateMachine>();
            // 启动状态机
            stateMachine.Init();
        }

        private void Update()
        {
            // 状态机工作入口
            stateMachine.Tick();
        }

        private void FixedUpdate()
        {
            // 一直按着跳跃键
            if (_rigidbody2D.velocity.y > 0 && Input.GetButton("Jump"))
            {
                // 刚体受重力的影响为原值的 0.5
                _rigidbody2D.gravityScale = 0.5f;
            }

            // 没有一直按着跳跃键
            if (_rigidbody2D.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                // 调整刚体受重力的影响程度
                _rigidbody2D.gravityScale = PlayerVariable.LowJumpMultiplier;
            }

            if (IsJumpFlag)
            {
                _rigidbody2D.AddForce(Vector2.up * PlayerVariable.JumpVelocity, ForceMode2D.Impulse);
                IsJumpFlag = false;
            }

            if (IsFallFlag)
            {
                if (_rigidbody2D.velocity.y < 0)
                {
                    // 调整刚体受重力的影响程度
                    _rigidbody2D.gravityScale = PlayerVariable.FallMultiplier;
                    IsFallFlag = false;
                }
            }
        }
    }
}