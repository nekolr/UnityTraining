using System.Collections.Generic;
using MyStateMachine;
using Player.States;
using UnityEngine;

namespace Player
{
    /// <summary>
    /// 玩家入口
    /// </summary>
    public class PlayerEntry : MonoBehaviour
    {
        /// <summary>
        /// 碰撞层
        /// </summary>
        [Tooltip("碰撞层")] public LayerMask layerMask;

        /// <summary>
        /// 状态机
        /// </summary>
        public StateMachine StateMachine { get; private set; }

        /// <summary>
        /// 状态字典
        /// </summary>
        public Dictionary<StateID, AbstractState> StateDictionary { get; private set; }

        private void Awake()
        {
            // 创建状态机
            StateMachine = new StateMachine();
            // 创建字典
            StateDictionary = new Dictionary<StateID, AbstractState>();
            // 创建所有的状态
            var idleState = new IdleState(this);
            var runState = new RunState(this);
            var jumpState = new JumpState(this);
            var fallState = new FallState(this);
            // 初始化状态字典
            StateDictionary.Add(StateID.Idle, idleState);
            StateDictionary.Add(StateID.Run, runState);
            StateDictionary.Add(StateID.Jump, jumpState);
            StateDictionary.Add(StateID.Fall, fallState);
            // 初始化状态机
            StateMachine.Initialize(idleState);
        }
        
        void Update()
        {
            StateMachine.CurrentState.ExecuteByUpdate();
        }

        void FixedUpdate()
        {
            StateMachine.CurrentState.ExecuteByFixedUpdate();
        }
    }
}