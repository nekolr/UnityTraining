using System;
using FSM;
using UnityEngine;

namespace Player.States
{
    public class JumpState : AbstractState
    {
        public JumpState(Enum stateID) : base(stateID)
        {
        }

        public override void OnEnter(StateMachine stateMachine)
        {
            stateMachine.Animator.SetBool(PlayerVariable.IsJump, true);

            // TODO: 这里需要设置一下标记，表示进入了跳跃状态，然后在 FixedUpdate 中判断标记并执行跳跃方法
            stateMachine.GetComponent<PlayerEntry>().IsJumpFlag = true;
        }

        public override void OnExecute(StateMachine stateMachine)
        {
            var x = Input.GetAxis("Horizontal");
            Rigidbody2D rigidbody2D = stateMachine.GetComponent<Rigidbody2D>();
            // 向右移动
            if (x > 0)
                // 朝向不变，因为默认这是默认朝向
                rigidbody2D.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            // 向左移动
            if (x < 0)
                rigidbody2D.transform.eulerAngles = new Vector3(0f, 180f, 0f);
            
            // TODO: 由于在跳跃过程中，玩家可能会一直按着奔跑的按键，所以还要调用奔跑的方法
            DoRun(rigidbody2D, x, 0, 0);
        }

        private void DoRun(Rigidbody2D rigidbody2D, float x, float y, float z)
        {
            // 这里使用 Vector3 而不使用 Vector2 是为了方便传值（不需要再进行转换）
            Vector3 movement = new Vector3(x, y, z);
            rigidbody2D.transform.position += PlayerVariable.RunVelocity * Time.deltaTime * movement;
        }

        public override void OnExit(StateMachine stateMachine)
        {
            stateMachine.Animator.SetBool(PlayerVariable.IsJump, false);
        }
    }
}