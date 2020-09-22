using UnityEngine;

namespace OldScripts
{
    public class NinjaFrog : MonoBehaviour
    {
        public float speed = 2;
        public LayerMask layer;

        // 头部监测点
        public Transform headPoint;
        // 右侧监测点-上
        public Transform rightUp;
        // 右侧监测点-下
        public Transform rightDown;

        // 发生碰撞
        private bool _isCollided;

        private Rigidbody2D _rigidbody2D;
        private CapsuleCollider2D _capsuleCollider2D;
        private Animator _animator;

        // Start is called before the first frame update
        void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
            _animator = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            // 刚体移动
            Vector3 movement = new Vector3(speed, _rigidbody2D.velocity.y, 0);
            transform.position += movement * Time.deltaTime;

            // 碰撞检测
            _isCollided = Physics2D.Linecast(rightUp.position, rightDown.position, layer);

            if (_isCollided)
            {
                Debug.DrawLine(rightUp.position, rightDown.position, Color.red);
                // 此处改变角色朝向，也可以使用欧拉角
                transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, 0);
                // 方向变化
                speed *= -1f;
            }
            else
            {
                Debug.DrawLine(rightUp.position, rightDown.position, Color.green);
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag == "Player")
            {
                float height = other.contacts[0].point.y - headPoint.position.y;

                if (height > 0)
                {
                    // 玩家弹起
                    other.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 8.0f, ForceMode2D.Impulse);
                    // 刚体停止移动
                    speed = 0f;
                    // 激活动画
                    _animator.SetTrigger("hit");
                    // 碰撞检测器失效
                    _capsuleCollider2D.enabled = false;
                    // 取消重力作用，不然由于碰撞检测器失效而掉出场景
                    _rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
                    // 销毁
                    Destroy(gameObject, 1f);
                }
                else
                {
                    // 刚体停止移动
                    speed = 0f;
                    PlayerDie.Instance.isDie = true;
                }
            }
        }
    }
}