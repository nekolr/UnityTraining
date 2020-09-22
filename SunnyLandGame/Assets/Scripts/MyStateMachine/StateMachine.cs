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
        private AbstractState _currentState;

        /// <summary>
        /// 存储所有的转换
        /// </summary>
        private Dictionary<Type, List<Transition>> _transitions = new Dictionary<Type, List<Transition>>();

        /// <summary>
        /// 任意状态对应的转换列表
        /// </summary>
        private List<Transition> _anyTransitions = new List<Transition>();

        /// <summary>
        /// 当前状态对应的转换列表
        /// </summary>
        private List<Transition> _currentTransitions = new List<Transition>();

        /// <summary>
        /// 一个空的转换列表
        /// </summary>
        private static readonly List<Transition> EmptyTransitions = new List<Transition>(0);

        /// <summary>
        /// 状态机的主要工作方法
        /// </summary>
        public void Tick()
        {
            // 获取满足触发条件的转换
            var transition = GetTransition();
            // 状态转换
            if (transition != null)
                ChangeState(transition.To);

            // 执行状态中的方法
            _currentState?.Execute();
        }

        /// <summary>
        /// 添加转换
        /// </summary>
        /// <param name="from">起始状态</param>
        /// <param name="to">下一个状态</param>
        /// <param name="predicate">状态转换的条件函数</param>
        public void AddTransition(AbstractState from, AbstractState to, Func<bool> predicate)
        {
            // 尝试通过状态获取对应的转换列表
            if (!_transitions.TryGetValue(from.GetType(), out var transitions))
            {
                // 如果没有就先初始化
                transitions = new List<Transition>();
                _transitions[from.GetType()] = transitions;
            }
            // 向转换列表中添加转换
            transitions.Add(new Transition(to, predicate));
        }

        /// <summary>
        /// 添加从任意状态到另一个状态的转换
        /// </summary>
        /// <param name="to">下一个状态</param>
        /// <param name="predicate">状态转换的条件函数</param>
        public void AddAnyTransition(AbstractState to, Func<bool> predicate)
        {
            _anyTransitions.Add(new Transition(to, predicate));
        }

        /// <summary>
        /// 改变状态
        /// </summary>
        /// <param name="state">目标状态</param>
        public void ChangeState(AbstractState state)
        {
            if (_currentState == state)
                return;
            // 离开当前状态前调用
            _currentState?.Exit();
            // 改变状态
            _currentState = state;
            // 尝试获取状态对应的转换列表
            _transitions.TryGetValue(_currentState.GetType(), out _currentTransitions);
            // 如果没有对应的转换列表，则给一个默认的空列表
            if (_transitions == null)
                _currentTransitions = EmptyTransitions;
            // 进入状态时调用
            _currentState.Enter();
        }

        /// <summary>
        /// 获取满足触发条件的转换
        /// </summary>
        /// <returns>转换</returns>
        private Transition GetTransition()
        {
            // 先获取任意状态的转换
            foreach (var transition in _anyTransitions)
            {
                // 判断是否满足转换的条件
                if (transition.Condition())
                    return transition;
            }

            // 再获取当前状态的转换
            foreach (var transition in _currentTransitions)
            {
                // 判断是否满足转换的条件
                if (transition.Condition())
                    return transition;
            }

            return null;
        }

        /// <summary>
        /// 转换，从一个状态转换到另一个状态
        /// </summary>
        private class Transition
        {
            /// <summary>
            /// 状态转换的条件函数
            /// </summary>
            public Func<bool> Condition { get; }

            /// <summary>
            /// 下一个状态
            /// </summary>
            public AbstractState To { get; }

            /// <summary>
            /// 构造器
            /// </summary>
            /// <param name="to">下一个状态</param>
            /// <param name="condition">状态转换的条件函数</param>
            public Transition(AbstractState to, Func<bool> condition)
            {
                this.To = to;
                this.Condition = condition;
            }
        }
    }
}