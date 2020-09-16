using FSM;
using UnityEngine;

namespace Player.States
{
    public class RunState : AbstractState
    {
        public RunState(StateID stateID) : base(stateID)
        {
        }

        public override void OnEnter(FSM.StateMachine stateMachine)
        {
            stateMachine.Animator.SetBool(PlayerVariable.IsRun, true);
        }

        public override void OnExecute(FSM.StateMachine stateMachine)
        {
            float x = Input.GetAxis("Horizontal");
            Rigidbody2D rigidbody2D = stateMachine.GetComponent<Rigidbody2D>();

            if (x > 0)
            {
                // 方向不变
                rigidbody2D.transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else if (x < 0)
            {
                rigidbody2D.transform.eulerAngles = new Vector3(0, 180, 0);
            }

            // 移动的基本向量
            Vector3 runVector = new Vector3(x, 0, 0);
            rigidbody2D.transform.position += PlayerVariable.RunVelocity * Time.deltaTime * runVector;
        }
    }
}