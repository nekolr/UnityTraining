using UnityEngine;

namespace OldScripts
{
    public class FallingPlatform : MonoBehaviour
    {

        // 检测到物体时经过多少秒平台掉落
        public float fallingTime = 1;

        private TargetJoint2D _targetJoint2D;
        private BoxCollider2D _boxCollider2D;

        // Start is called before the first frame update
        void Start()
        {
            _targetJoint2D = GetComponent<TargetJoint2D>();
            _boxCollider2D = GetComponent<BoxCollider2D>();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag == "Player")
            {
                // 开启一个线程调用方法，可以设置延迟调用的时间
                Invoke("Falling", fallingTime);
            }

            if (other.gameObject.tag == "Spike")
            {
                Destroy(this);
            }
        }

        private void Falling()
        {
            _targetJoint2D.enabled = false;
            // 设置成 Trigger 后，由于角色是刚体，会随着重力下落
            _boxCollider2D.isTrigger = true;
        }
    }
}
