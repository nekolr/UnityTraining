using System;
using System.Collections.Generic;
using FSM.Common;
using UnityEngine;

namespace FSM
{
    /// <summary>
    /// 有限状态机
    /// </summary>
    public class StateMachine : MonoBehaviour
    {
        /// <summary>
        /// Animator
        /// </summary>
        public Animator Animator { get; private set; }

        /// <summary>
        /// 当前状态
        /// </summary>
        private AbstractState currentState;

        /// <summary>
        /// 默认状态编号，通过编译器配置
        /// </summary>
        [Tooltip("默认状态编号")] public string defaultStateName;

        /// <summary>
        /// 脚本所在包名
        /// </summary>
        [Tooltip("脚本所在包名")] public string scriptsPackageName;

        /// <summary>
        /// 当前状态机使用的配置文件，通过编译器配置
        /// </summary>
        [Tooltip("当前状态机使用的配置文件")] public string configFilename = "Transition_001";

        /// <summary>
        /// 状态列表
        /// </summary>
        private List<AbstractState> stateList;

        /// <summary>
        /// 默认状态
        /// </summary>
        private AbstractState defaultState;

        /// <summary>
        /// 状态枚举类型
        /// </summary>
        private Type stateEnumType;

        /// <summary>
        /// 条件枚举类型
        /// </summary>
        private Type triggerEnumType;

        /// <summary>
        /// 切换状态
        /// </summary>
        /// <param name="stateID"></param>
        public void ChangeState(Enum stateID)
        {
            // 离开上一个状态
            this.currentState.OnExit(this);
            // 如果需要切换到默认状态，则直接赋值，否则从状态列表中获取并赋值
            this.currentState = Equals(stateID, Enum.Parse(stateEnumType, "Default"))
                ? defaultState
                : stateList.Find(s => Equals(s.StateID, stateID));
            // 进入下一个状态
            this.currentState.OnEnter(this);
        }

        /// <summary>
        /// 初始化方法
        /// </summary>
        public void Init()
        {
            // 初始化状态机
            this.InitStateMachine();
            // 初始化默认的状态
            this.InitDefaultState();
        }

        /// <summary>
        /// 初始化状态机
        /// </summary>
        private void InitStateMachine()
        {
            // 初始化状态列表
            this.stateList = new List<AbstractState>();
            this.Animator = GetComponentInChildren<Animator>();
            // 根据编译器配置的枚举类型名称反射得到枚举的类型
            stateEnumType = Type.GetType(scriptsPackageName + ".StateID");
            triggerEnumType = Type.GetType(scriptsPackageName + ".TriggerID");
            // 获取配置信息
            Dictionary<string, Dictionary<string, string>> configMapper =
                ResourceManagerFactory.GetConfigMapper(configFilename);

            foreach (var config in configMapper)
            {
                // 将字符串转换为枚举
                object stateID = Enum.Parse(stateEnumType, config.Key);
                // 创建状态对象
                AbstractState state = this.CreateState((Enum) stateID);
                foreach (var mapper in config.Value)
                {
                    // 将包名传递
                    state.ScriptsPackageName = this.scriptsPackageName;
                    // 给状态对象添加 {条件: 状态} 映射
                    state.AddTransition((Enum) Enum.Parse(triggerEnumType, mapper.Key),
                        (Enum) Enum.Parse(stateEnumType, mapper.Value));
                }

                // 将状态加入状态机
                this.stateList.Add(state);
            }
        }

        /// <summary>
        /// 创建状态对象
        /// </summary>
        /// <param name="stateID">状态编号</param>
        /// <returns>状态对象</returns>
        private AbstractState CreateState(Enum stateID)
        {
            // 反射加载类
            // 命名规范为：${scriptsPackageName}.States.状态枚举 + State
            Type type = Type.GetType(this.scriptsPackageName + ".States." + stateID + "State");
            // 构造函数参数
            System.Object[] constructParams = {stateID};
            // 创建条件对象
            AbstractState state = Activator.CreateInstance(type, constructParams) as AbstractState;

            return state;
        }

        /// <summary>
        /// 初始化默认的状态
        /// </summary>
        private void InitDefaultState()
        {
            // 从状态列表中查找配置的默认状态编号
            this.defaultState =
                this.stateList.Find(s => Equals(s.StateID, Enum.Parse(stateEnumType, defaultStateName)));
            // 将当前状态设置为默认状态
            this.currentState = defaultState;
            // 进入状态
            this.currentState.OnEnter(this);
        }

        /// <summary>
        /// 状态机工作入口
        /// </summary>
        public void Tick()
        {
            // 判断当前状态的条件是否满足
            this.currentState.CheckState(this);
            // 执行当前状态的逻辑
            this.currentState.OnExecute(this);
        }
    }
}