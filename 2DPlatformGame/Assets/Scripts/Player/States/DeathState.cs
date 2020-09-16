using FSM;

namespace Player.States
{
    public class DeathState : AbstractState
    {
        public DeathState(StateID stateID) : base(stateID)
        {
        }

        public override void OnEnter(FSM.StateMachine stateMachine)
        {
            // 死亡则禁用状态机
            stateMachine.enabled = false;
        }
    }
}