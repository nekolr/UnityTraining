using System;
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

        private void Awake()
        {
            // 存储两个碰撞检测点的位置
            FrogVariables.LeftX = leftPoint.position.x;
            FrogVariables.RightX = rightPoint.position.x;
            // 创建状态机
            StateMachine = new StateMachine();
            // 创建所有的状态
            var idleState = new IdleState(FrogStateID.Idle, this);
            var jumpState = new JumpState(FrogStateID.Jump, this);
            var fallState = new FallState(FrogStateID.Fall, this);
            var deathState = new DeathState(FrogStateID.Death, this);
            // 添加状态字典
            StateMachine.AddState(idleState);
            StateMachine.AddState(jumpState);
            StateMachine.AddState(fallState);
            StateMachine.AddState(deathState);
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
                    StateMachine.ChangeState(StateMachine.StateDictionary[FrogStateID.Death]);
                }
                else
                {
                    if (!Equals(StateMachine.CurrentState.ID, StateID.Death))
                    {
                        // 玩家受伤
                        PlayerVariables.IsHurt = true;
                    }
                }
            }
        }
    }
}