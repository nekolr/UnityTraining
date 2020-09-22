using MyStateMachine;
using UnityEngine;

namespace States.Player
{
    public class JumpState : AbstractState
    {
        private Animator _animator;
        private Rigidbody2D _rigidbody2D;
        private PlayerEntry _playerEntry;

        public JumpState(PlayerEntry playerEntry, Animator animator, Rigidbody2D rigidbody2D)
        {
            this._playerEntry = playerEntry;
            this._animator = animator;
            this._rigidbody2D = rigidbody2D;
        }

        public override void OnEnter()
        {
            _animator.SetBool("isJump", true);
            // 设置跳起的标志，然后跳跃方法在 FixedUpdate 中执行
            _playerEntry.IsJumpFlag = true;
        }

        public override void OnExecute()
        {
            // TODO: 由于在跳跃过程中，玩家可能会一直按着奔跑的按键，所以还要调用奔跑的方法
            DoRun(Input.GetAxis("Horizontal"), 0, 0);
        }

        private void DoRun(float x, float y, float z)
        {
            // 这里使用 Vector3 而不使用 Vector2 是为了方便传值（不需要再进行转换）
            Vector3 movement = new Vector3(x, y, z);
            _rigidbody2D.transform.position += PlayerEntry.Speed * Time.deltaTime * movement;
        }

        public override void OnExit()
        {
            _animator.SetBool("isJump", false);
        }
    }
}