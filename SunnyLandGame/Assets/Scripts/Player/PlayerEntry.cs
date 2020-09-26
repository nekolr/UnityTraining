using System;
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
        /// 头部检测点
        /// </summary>
        [Tooltip("头部检测点")] public Transform headPoint;

        /// <summary>
        /// 跳跃的声音
        /// </summary>
        [Tooltip("跳跃的声音")] public AudioSource jumpAudioSource;

        /// <summary>
        /// 跳跃的声音
        /// </summary>
        [Tooltip("受伤的声音")] public AudioSource hurtAudioSource;

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
        public Dictionary<Enum, AbstractState> StateDictionary { get; private set; }

        private void Awake()
        {
            // 创建状态机
            StateMachine = new StateMachine();
            // 创建字典
            StateDictionary = new Dictionary<Enum, AbstractState>();
            // 创建所有的状态
            var idleState = new IdleState(this);
            var runState = new RunState(this);
            var jumpState = new JumpState(this);
            var fallState = new FallState(this);
            var hurtState = new HurtState(this);
            var crouchState = new CrouchState(this);
            var deathState = new DeathState(this);
            // 初始化状态字典
            StateDictionary.Add(StateID.Idle, idleState);
            StateDictionary.Add(StateID.Run, runState);
            StateDictionary.Add(StateID.Jump, jumpState);
            StateDictionary.Add(StateID.Fall, fallState);
            StateDictionary.Add(StateID.Hurt, hurtState);
            StateDictionary.Add(StateID.Crouch, crouchState);
            StateDictionary.Add(StateID.Death, deathState);
            // 初始化状态机
            StateMachine.Initialize(idleState);
        }

        void Update()
        {
            StateMachine.ExecuteByUpdate();
        }

        void FixedUpdate()
        {
            StateMachine.ExecuteByFixedUpdate();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // 任意状态可以触发死亡状态
            if (other.gameObject.CompareTag("DeadLine"))
            {
                StateMachine.ChangeState(StateDictionary[StateID.Death]);
            }
        }
    }
}