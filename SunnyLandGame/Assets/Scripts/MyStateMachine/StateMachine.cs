using System;
using System.Collections.Generic;

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
        /// 状态字典
        /// </summary>
        public Dictionary<Enum, AbstractState> StateDictionary { get; private set; }

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

        /// <summary>
        /// 添加状态
        /// </summary>
        /// <param name="state">状态类</param>
        public void AddState(AbstractState state)
        {
            if (StateDictionary == null)
                StateDictionary = new Dictionary<Enum, AbstractState>();
            StateDictionary.Add(state.ID, state);
        }
    }
}