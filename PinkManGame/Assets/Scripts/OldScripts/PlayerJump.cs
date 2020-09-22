using UnityEngine;

namespace OldScripts
{
    public class PlayerJump : MonoBehaviour
    {
        public float jumpVelocity = 5;
        private Rigidbody2D _rigidbody2D;
        private bool isJump = false;
    
        // Start is called before the first frame update
        void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        
        }

        // Update is called once per frame
        void Update()
        {
            // 按下空格键（这里可以在 Unity 的 Edit -> Project Settings 中设置对应的按键映射）
            if (Input.GetButtonDown("Jump"))
            {
                isJump = true;
            }
        }
    
        // 这里不能使用 Update，因为 Update 是逐帧渲染的，而不同的机器每秒渲染的帧数可能不一样，有的
        // 是一秒 100 帧，有的可能一秒只能 60 帧。如果在 Update 中更新跳跃方法可能会导致不同的机器
        // 跳跃的速度和高度不一致
        private void FixedUpdate()
        {
            if (isJump)
            {
                // 添加刚体的变化，ForceMode2D 为刚体的叠加方式，这里为不断叠加
                _rigidbody2D.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
                isJump = false;
            }
        }
    }
}
