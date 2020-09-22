using MyStateMachine;
using UnityEngine;

namespace States.Player
{
    public class FallState : AbstractState
    {
        private PlayerEntry _playerEntry;
        private Animator _animator;
        private Rigidbody2D _rigidbody2D;

        public FallState(PlayerEntry playerEntry, Animator animator, Rigidbody2D rigidbody2D)
        {
            this._playerEntry = playerEntry;
            this._animator = animator;
            this._rigidbody2D = rigidbody2D;
        }

        public override void OnEnter()
        {
            _playerEntry.IsFallFlag = true;
            _animator.SetBool("isFall", true);
        }

        public override void OnExecute()
        {
            var x = Input.GetAxis("Horizontal");

            // 向右移动
            if (x > 0)
                // 朝向不变，因为默认这是默认朝向
                _rigidbody2D.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            // 向左移动
            if (x < 0)
                _rigidbody2D.transform.eulerAngles = new Vector3(0f, 180f, 0f);

            // TODO: 由于在下降过程中，玩家可能会一直按着奔跑的按键，所以还要调用奔跑的方法
            DoRun(x, 0, 0);
        }

        private void DoRun(float x, float y, float z)
        {
            // 这里使用 Vector3 而不使用 Vector2 是为了方便传值（不需要再进行转换）
            Vector3 movement = new Vector3(x, y, z);
            _rigidbody2D.transform.position += PlayerEntry.Speed * Time.deltaTime * movement;
        }

        public override void OnExit()
        {
            _animator.SetBool("isFall", false);
        }
    }
}