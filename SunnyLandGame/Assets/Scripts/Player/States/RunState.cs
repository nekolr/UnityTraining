using MyStateMachine;
using UnityEngine;

namespace Player.States
{
    public class RunState : AbstractState
    {
        private PlayerEntry _playerEntry;

        public RunState(PlayerEntry playerEntry)
        {
            _playerEntry = playerEntry;
        }

        public override void Enter()
        {
            Animator animator = _playerEntry.GetComponent<Animator>();
            animator.SetBool("isRun", true);
        }

        public override void Execute()
        {
            float x = Input.GetAxis("Horizontal");
            Rigidbody2D rigidbody2D = _playerEntry.GetComponent<Rigidbody2D>();

            // 向右移动
            if (x > 0)
                // 朝向不变，因为默认这是默认朝向
                rigidbody2D.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            // 向左移动
            if (x < 0)
                rigidbody2D.transform.eulerAngles = new Vector3(0f, 180f, 0f);

            DoRun(rigidbody2D, x, rigidbody2D.velocity.y, 0);
        }

        private void DoRun(Rigidbody2D rigidbody2D, float x, float y, float z)
        {
            Vector3 vector = new Vector3(x, y, z);
            rigidbody2D.transform.position += vector * _playerEntry.speed * Time.deltaTime;
        }

        public override void Exit()
        {
            Animator animator = _playerEntry.GetComponent<Animator>();
            animator.SetBool("isRun", false);
        }
    }
}