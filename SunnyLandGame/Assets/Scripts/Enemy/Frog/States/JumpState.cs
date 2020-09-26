using MyStateMachine;
using UnityEngine;

namespace Enemy.Frog.States
{
    public class JumpState : AbstractState
    {
        private readonly Animator _animator;
        private readonly Transform _transform;
        private readonly Rigidbody2D _rigidbody2D;

        /// <summary>
        /// 是否发生碰撞
        /// </summary>
        private bool _isCollided;

        /// <summary>
        /// 碰撞检测点
        /// </summary>
        private readonly float _rightX;

        /// <summary>
        /// 碰撞检测点
        /// </summary>
        private readonly float _leftX;


        private bool _isJump;

        public JumpState(FrogEntry enemyEntry) : base(enemyEntry.StateMachine, enemyEntry.StateDictionary)
        {
            _animator = enemyEntry.GetComponent<Animator>();
            _rigidbody2D = enemyEntry.GetComponent<Rigidbody2D>();
            _transform = enemyEntry.transform;
            _leftX = FrogVariables.LeftX;
            _rightX = FrogVariables.RightX;
        }

        public override void Enter()
        {
            _animator.SetBool("isJump", true);
            _isJump = true;
        }

        public override void ExecuteByUpdate()
        {
            TransitionTrigger();
            if (_isJump)
            {
                Movement();
                _isJump = false;
            }
        }

        private void Movement()
        {
            // 发生碰撞
            if (_transform.position.x > _rightX)
            {
                _isCollided = true;
            }

            // 发生碰撞
            if (_transform.position.x < _leftX)
            {
                _isCollided = true;
            }

            // 发生碰撞时角度和速度变换
            if (_isCollided)
            {
                var localScale = _transform.localScale;
                _transform.localScale = new Vector3(localScale.x * -1f, localScale.y, 0);
                FrogVariables.Speed *= -1;
                // 重置
                _isCollided = false;
            }

            // 移动
            _rigidbody2D.velocity = new Vector2(FrogVariables.Speed, FrogVariables.JumpVelocity);

            FrogVariables.Direction = _transform.localScale.x;
        }

        private void TransitionTrigger()
        {
            if (_rigidbody2D.velocity.y < 0)
            {
                StateMachine.ChangeState(StateDictionary[StateID.Fall]);
            }
        }

        public override void Exit()
        {
            _animator.SetBool("isJump", false);
        }
    }
}