using System;

namespace FSM
{
    /// <summary>
    /// 触发条件
    /// </summary>
    public abstract class AbstractTrigger
    {
        /// <summary>
        /// 条件编号
        /// </summary>
        public Enum TriggerID { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="triggerID">条件编号</param>
        protected AbstractTrigger(Enum triggerID)
        {
            // 要求子类必须初始化条件编号
            this.TriggerID = triggerID;
        }

        /// <summary>
        /// 判断是否满足条件
        /// </summary>
        /// <param name="stateMachine">状态机</param>
        /// <returns>是否满足条件</returns>
        public abstract bool Predicate(StateMachine stateMachine);
    }
}