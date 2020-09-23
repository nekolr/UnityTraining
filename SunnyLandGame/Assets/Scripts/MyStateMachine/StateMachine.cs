using Player;
using UnityEngine;

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
        public AbstractState CurrentState { get; private set; }

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