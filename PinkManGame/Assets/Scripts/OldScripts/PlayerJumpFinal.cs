using UnityEngine;

namespace OldScripts
{
    public class PlayerJumpFinal : MonoBehaviour
    {
        // 下降时速度变化率
        public float fallMultiplier = 2.5f;

        // 向上跳时速度变化率
        public float lowJumpMultiplier = 2f;

        [Range(0, 10)] public float jumpVelocity = 5f;

        public LayerMask mask;

        // 落地碰撞检测盒子的高度（落地检测的碰撞只需要脚部那部分的盒子即可）
        public float boxHeight = 0.5f;

        // 精灵大小（玩家盒子的大小）
        private Vector2 playerSize;

        // 碰撞检测盒子大小
        private Vector2 boxSize;

        // 是否跳跃
        private bool isJump = false;

        // 是否落地
        private bool isGrounded = false;

        private Rigidbody2D _rigidbody2D;

        private Animator _animator;

        // Start is called before the first frame update
        void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            // 获取精灵边框的大小
            playerSize = GetComponent<SpriteRenderer>().bounds.size;
            // 获取碰撞检测盒子的大小，使用玩家的大小 80%，同时高度设置为 0.5
            boxSize = new Vector2(playerSize.x * 0.8f, boxHeight);
        }

        // Update is called once per frame
        void Update()
        {
            // 已经落地的情况下，按下了跳跃键
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                isJump = true;
            }
        }

        private void FixedUpdate()
        {
            // 刚体在下降
            if (_rigidbody2D.velocity.y < 0)
            {
                // 调整刚体受重力的影响程度
                _rigidbody2D.gravityScale = fallMultiplier;
                _animator.SetBool("isFall", true);
                _animator.SetBool("isJump", false);
            }
            // 刚体在上升，并且并没有一直按着跳跃键
            else if (_rigidbody2D.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                _rigidbody2D.gravityScale = lowJumpMultiplier;
                _animator.SetBool("isJump", true);
                _animator.SetBool("isFall", false);
            }
            // 刚体在上升，并且一直按着跳跃键
            else if (_rigidbody2D.velocity.y > 0 && Input.GetButton("Jump"))
            {
                // 刚体受重力的影响为原值
                _rigidbody2D.gravityScale = 0.5f;
                _animator.SetBool("isJump", true);
                _animator.SetBool("isFall", false);
            }

            if (isJump)
            {
                // 点击一下跳跃键，叠加一次向上的变化
                _rigidbody2D.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
                isJump = false;
                isGrounded = false;
                _animator.SetBool("isJump", true);
                _animator.SetBool("isFall", false);
            }
            else
            {
                // 先将碰撞盒子的位置移动到玩家脚部位置
                // transform.position 的位置是精灵的正中心，如果盒子移到这里是无法进行碰撞检测的，需要移动到脚部
                Vector2 jumpBoxPosition = (Vector2) transform.position + (Vector2.down * playerSize * 0.5f);

                /*
             * 碰撞检测
             *
             * mask 代表碰撞发生时，碰撞盒子与哪些图层进行了交互，比如与 Bound、Foreground
             *
             * 如果条件成立，说明碰撞发生了
             */
                if (Physics2D.OverlapBox(jumpBoxPosition, boxSize, 0, mask) != null)
                {
                    isGrounded = true;

                    _animator.SetBool("isJump", false);
                    _animator.SetBool("isFall", false);

                    if (_rigidbody2D.velocity.y > -0.001 && _rigidbody2D.velocity.y < 0.001)
                    {
                        _animator.SetBool("isJump", false);
                        _animator.SetBool("isFall", true);
                    }
                }
                else
                {
                    isGrounded = false;
                }
            }
        }

        private void OnDrawGizmos()
        {
            if (isGrounded)
            {
                Gizmos.color = Color.red;
            }
            else
            {
                Gizmos.color = Color.green;
            }
            Vector2 jumpBoxPosition = (Vector2) transform.position + (Vector2.down * playerSize * 0.5f);
            Gizmos.DrawWireCube(jumpBoxPosition, boxSize);
        }
    }
}