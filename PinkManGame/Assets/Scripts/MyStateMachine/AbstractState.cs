namespace MyStateMachine
{
    /// <summary>
    /// 状态接口
    /// </summary>
    public abstract class AbstractState
    {
        /// <summary>
        /// 状态中
        /// </summary>
        public virtual void OnExecute()
        {
        }

        /// <summary>
        /// 进入状态
        /// </summary>
        public virtual void OnEnter()
        {
        }

        /// <summary>
        /// 退出状态
        /// </summary>
        public virtual void OnExit()
        {
        }
    }
}