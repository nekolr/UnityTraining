using UnityEngine;

namespace Enemy
{
    public class EagleController : MonoBehaviour
    {

        public float speed = 2f;
        public Transform headPoint;
        public Transform bottomPoint;

        private Rigidbody2D _rigidbody2D;
        private float _headY;
        private float _bottomY;
        private bool _isCollided;

        // Start is called before the first frame update
        void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _headY = headPoint.position.y;
            _bottomY = bottomPoint.position.y;
            Destroy(headPoint.gameObject);
            Destroy(bottomPoint.gameObject);
        }

        // Update is called once per frame
        void Update()
        {
            Movement();
        }

        private void Movement()
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, speed);
            
            // 移动是通过不断设置 Y 轴速度实现的，每帧调用一次该方法
            // 如果只判断当前位置是否在检查点之上，那么会有可能出现已经大于 0，然后修改了速度的方向
            // 但是经过一帧的调用之后，当前位置还在检查点之上（物体是匀速移动的），此时又再次改变移动方向
            // 就会造成在检查点上下晃动的情况
            
            // 加入速度方向的判断，是为了使当检测到碰撞后，如果没有改变过方向，那么才会改变方向
            if (transform.position.y > _headY && speed > 0)
            {
                _isCollided = true;
            }

            if (transform.position.y < _bottomY && speed < 0)
            {
                _isCollided = true;
            }

            if (_isCollided)
            {
                speed *= -1;
                _isCollided = false;
            }
        }
    }
}
