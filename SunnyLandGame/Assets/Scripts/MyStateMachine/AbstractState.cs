using System;

namespace MyStateMachine
{
    /// <summary>
    /// 状态接口
    /// </summary>
    public abstract class AbstractState
    {
        // /// <summary>
        // /// 状态 ID
        // /// </summary>
        public Enum ID { get; }

        /// <summary>
        /// 状态机
        /// </summary>
        protected StateMachine StateMachine { get; }

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="stateID">状态 ID</param>
        /// <param name="stateMachine">状态机</param>
        protected AbstractState(Enum stateID, StateMachine stateMachine)
        {
            ID = stateID;
            StateMachine = stateMachine;
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