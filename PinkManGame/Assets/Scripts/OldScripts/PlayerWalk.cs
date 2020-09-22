using UnityEngine;

namespace OldScripts
{
    public class PlayerWalk : MonoBehaviour
    {

        public float speed = 5;
        private Rigidbody2D _rigidbody2D;
        private Animator _animator;

        private float x;
        private float y;
    
        // Start is called before the first frame update
        void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            x = Input.GetAxis("Horizontal");
            y = Input.GetAxis("Vertical");
        
            // 正向
            if (x > 0)
            {
                // 朝向不变，因为默认这是默认朝向
                _rigidbody2D.transform.eulerAngles = new Vector3(0f, 0f, 0f);
                _animator.SetBool("isRun", true);
            }

            if (x < 0)
            {
                _rigidbody2D.transform.eulerAngles = new Vector3(0f, 180f, 0f);
                _animator.SetBool("isRun", true);
            }

            // x < 0.001f && x > -0.001
            if (x < Mathf.Epsilon && x > -Mathf.Epsilon)
            {
                _animator.SetBool("isRun", false);
            }

            Run();
        }

        private void Run()
        {
            // 这里使用 Vector3 而不使用 Vector2 是为了方便传值（不需要再进行转换）
            Vector3 movement = new Vector3(x, y, 0);
            _rigidbody2D.transform.position += speed * Time.deltaTime * movement;
        }
    }
}
