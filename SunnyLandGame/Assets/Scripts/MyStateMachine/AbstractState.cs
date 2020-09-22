namespace MyStateMachine
{
    /// <summary>
    /// 状态接口
    /// </summary>
    public abstract class AbstractState
    {
        /// <summary>
        /// 在状态中
        /// </summary>
        public virtual void Execute()
        {
        }

        /// <summary>
        /// 进入状态
        /// </summary>
        public virtual void Enter()
        {
        }

        /// <summary>
        /// 退出状态
        /// </summary>
        public virtual void Exit()
        {
        }
    }
}