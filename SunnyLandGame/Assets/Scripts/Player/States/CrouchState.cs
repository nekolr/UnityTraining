using MyStateMachine;
using UnityEngine;

namespace Player.States
{
    public class CrouchState : AbstractState
    {
        private readonly Animator _animator;
        // 可以选择关闭的碰撞体
        private readonly BoxCollider2D _optionBoxCollider2D;
        private readonly Rigidbody2D _rigidbody2D;
        private readonly Transform _headPoint;
        private readonly LayerMask _layerMask;

        public CrouchState(PlayerEntry playerEntry) : base(playerEntry.StateMachine, playerEntry.StateDictionary)
        {
            _animator = playerEntry.GetComponent<Animator>();
            _optionBoxCollider2D = playerEntry.GetComponent<BoxCollider2D>();
            _rigidbody2D = playerEntry.GetComponent<Rigidbody2D>();
            _headPoint = playerEntry.headPoint;
            _layerMask = playerEntry.layerMask;
        }

        public override void Enter()
        {
            _animator.SetBool("isCrouch", true);
            _optionBoxCollider2D.enabled = false;
        }
        
        public override void ExecuteByUpdate()
        {
            // 蹲下时可以左右移动
            Run();
            // 以头部检测点为圆心，画一个半径为 0.2 像素的圆，只要 **指定图层上** 的碰撞体在圆内就为 true
            if (!Input.GetButton("Crouch") && !Physics2D.OverlapCircle(_headPoint.position, 0.2f, _layerMask))
            {
                StateMachine.ChangeState(StateDictionary[StateID.Idle]);
            }
        }
        
        private void Run()
        {
            float direction = Input.GetAxisRaw("Horizontal");
            // 速度向量
            _rigidbody2D.velocity = new Vector2(direction * PlayerVariables.Speed, _rigidbody2D.velocity.y);
            if (direction != 0)
            {
                // 方向变换
                _rigidbody2D.transform.localScale = new Vector3(direction, 1, 1);
            }
        }

        public override void Exit()
        {
            _optionBoxCollider2D.enabled = true;
            _animator.SetBool("isCrouch", false);
        }
    }
}