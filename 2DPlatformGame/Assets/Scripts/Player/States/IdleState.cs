using FSM;

namespace Player.States
{
    public class IdleState : AbstractState
    {
        public IdleState(StateID stateID) : base(stateID)
        {
        }

        public override void OnEnter(FSM.StateMachine stateMachine)
        {
            // 触发 Idle 动画
            stateMachine.Animator.SetBool(PlayerVariable.IsRun, false);
        }
    }
}