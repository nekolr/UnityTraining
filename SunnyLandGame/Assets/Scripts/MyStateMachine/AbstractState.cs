using System;
using System.Collections.Generic;

namespace MyStateMachine
{
    /// <summary>
    /// 状态接口
    /// </summary>
    public abstract class AbstractState
    {
        /// <summary>
        /// 状态机
        /// </summary>
        protected StateMachine StateMachine { get; }

        /// <summary>
        /// 状态字典
        /// </summary>
        protected Dictionary<Enum, AbstractState> StateDictionary { get; }

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="stateMachine">状态机</param>
        /// <param name="stateDictionary">状态字典</param>
        protected AbstractState(StateMachine stateMachine, Dictionary<Enum, AbstractState> stateDictionary)
        {
            StateMachine = stateMachine;
            StateDictionary = stateDictionary;
        }

        /// <summary>
        /// 在状态中
        /// </summary>
        public virtual void ExecuteByUpdate()
        {
        }

        /// <summary>
        /// 在状态中
        /// </summary>
        public virtual void ExecuteByFixedUpdate()
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