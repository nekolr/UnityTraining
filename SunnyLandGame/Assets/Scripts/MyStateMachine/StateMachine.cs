namespace MyStateMachine
{
    /// <summary>
    /// 有限状态机
    /// </summary>
    public class StateMachine
    {
        /// <summary>
        /// 当前状态
        /// </summary>
        private AbstractState CurrentState { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="startState">初始状态</param>
        public void Initialize(AbstractState startState)
        {
            CurrentState = startState;
            CurrentState.Enter();
        }

        /// <summary>
        /// 在 Update 方法中执行
        /// </summary>
        public void ExecuteByUpdate()
        {
            CurrentState.ExecuteByUpdate();
        }

        /// <summary>
        /// 在 FixedUpdate 方法中执行
        /// </summary>
        public void ExecuteByFixedUpdate()
        {
            CurrentState.ExecuteByFixedUpdate();
        }

        /// <summary>
        /// 改变状态
        /// </summary>
        /// <param name="state">目标状态</param>
        public void ChangeState(AbstractState state)
        {
            if (CurrentState == state)
                return;
            // 离开当前状态前调用
            CurrentState?.Exit();
            // 改变状态
            CurrentState = state;
            // 进入状态时调用
            CurrentState.Enter();
        }
    }
}