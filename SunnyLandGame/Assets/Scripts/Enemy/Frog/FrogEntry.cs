using System;
using System.Collections.Generic;
using Enemy.Frog.States;
using MyStateMachine;
using Player;
using UnityEngine;

namespace Enemy.Frog
{
    public class FrogEntry : MonoBehaviour
    {
        /// <summary>
        /// 头部监测点
        /// </summary>
        public Transform headPoint;

        /// <summary>
        /// 碰撞检测点
        /// </summary>
        public Transform leftPoint;

        /// <summary>
        /// 碰撞检测点
        /// </summary>
        public Transform rightPoint;

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
            // 存储两个碰撞检测点的位置
            FrogVariables.LeftX = leftPoint.position.x;
            FrogVariables.RightX = rightPoint.position.x;
            // 创建状态机
            StateMachine = new StateMachine();
            // 创建字典
            StateDictionary = new Dictionary<Enum, AbstractState>();
            // 创建所有的状态
            var idleState = new IdleState(this);
            var jumpState = new JumpState(this);
            var fallState = new FallState(this);
            var deathState = new DeathState(this);
            // 初始化状态字典
            StateDictionary.Add(StateID.Idle, idleState);
            StateDictionary.Add(StateID.Jump, jumpState);
            StateDictionary.Add(StateID.Fall, fallState);
            StateDictionary.Add(StateID.Death, deathState);
            // 初始化状态机
            StateMachine.Initialize(idleState);
        }

        private void Start()
        {
            // 销毁检测点
            Destroy(leftPoint.gameObject);
            Destroy(rightPoint.gameObject);
        }

        void Update()
        {
            StateMachine.ExecuteByUpdate();
        }

        void FixedUpdate()
        {
            StateMachine.ExecuteByFixedUpdate();
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                float height = other.GetContact(0).point.y - headPoint.position.y;

                if (height > 0)
                {
                    // 玩家弹起
                    other.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 13.0f, ForceMode2D.Impulse);
                    // 敌人死亡
                    StateMachine.ChangeState(StateDictionary[StateID.Death]);
                }
                else
                {
                    // 玩家受伤
                    PlayerVariables.IsHurt = true;
                }
            }
        }
    }
}