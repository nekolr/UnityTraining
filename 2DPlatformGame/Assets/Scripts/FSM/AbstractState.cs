using System;
using System.Collections.Generic;

namespace FSM
{
    /// <summary>
    /// 状态
    /// </summary>
    public abstract class AbstractState
    {
        /// <summary>
        /// 状态编号
        /// </summary>
        public Enum StateID { get; }

        /// <summary>
        /// 条件列表
        /// </summary>
        private List<AbstractTrigger> triggerList;

        /// <summary>
        /// 条件与状态的映射
        /// </summary>
        private Dictionary<Enum, Enum> transitions;

        /// <summary>
        /// 脚本所在包名
        /// </summary>
        public string ScriptsPackageName { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="stateID">状态编号</param>
        protected AbstractState(Enum stateID)
        {
            this.StateID = stateID;
            this.transitions = new Dictionary<Enum, Enum>();
            this.triggerList = new List<AbstractTrigger>();
        }

        /// <summary>
        /// 添加映射关系
        /// </summary>
        /// <param name="triggerID">条件编号</param>
        /// <param name="stateID">状态编号</param>
        public void AddTransition(Enum triggerID, Enum stateID)
        {
            // 添加映射
            this.transitions.Add(triggerID, stateID);
            // 创建条件对象
            this.CreateTrigger(triggerID);
        }


        /// <summary>
        /// 检查条件是否满足
        /// </summary>
        public void CheckState(StateMachine stateMachine)
        {
            foreach (var trigger in triggerList)
            {
                // 判断条件是否满足
                if (trigger.Predicate(stateMachine))
                {
                    // 从映射表中取出状态编号
                    Enum stateID = transitions[trigger.TriggerID];
                    // 切换状态
                    stateMachine.ChangeState(stateID);
                    return;
                }
            }
        }

        /// <summary>
        /// 创建并添加条件对象
        /// </summary>
        /// <param name="triggerID">条件编号</param>
        private void CreateTrigger(Enum triggerID)
        {
            // 反射加载类
            // 命名规范为：${scriptsPackageName}.Triggers.条件枚举 + Trigger
            Type type = Type.GetType(this.ScriptsPackageName + ".Triggers." + triggerID + "Trigger");
            // 构造函数参数
            Object[] constructParams = {triggerID};
            // 创建条件对象
            AbstractTrigger trigger = Activator.CreateInstance(type, constructParams) as AbstractTrigger;
            // 放入条件集合中
            this.triggerList.Add(trigger);
        }

        /// <summary>
        /// 进入状态时
        /// <param name="stateMachine">状态机</param>
        /// </summary>
        public virtual void OnEnter(StateMachine stateMachine)
        {
        }

        /// <summary>
        /// 状态中
        /// <param name="stateMachine">状态机</param>
        /// </summary>
        public virtual void OnExecute(StateMachine stateMachine)
        {
        }

        /// <summary>
        /// 离开状态时
        /// <param name="stateMachine">状态机</param>
        /// </summary>
        public virtual void OnExit(StateMachine stateMachine)
        {
        }
    }
}