using States.Player;
using UnityEngine;

namespace OldScripts
{
    public class Trampoline : MonoBehaviour
    {
        // 弹力值
        public float jumpForce = 22;

        private Animator _animator;

        // Start is called before the first frame update
        void Start()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag == "Player")
            {
                _animator.SetTrigger("jump");
                other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
        }
    }
}
