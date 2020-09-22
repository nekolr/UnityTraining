using System;
using MyStateMachine;
using Player.Actions;
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
        /// 移动速度
        /// </summary>
        [Tooltip("移动速度")] public float speed = 5;

        /// <summary>
        /// 跳跃速度
        /// </summary>
        [Tooltip("跳跃速度")] public float jumVelocity = 5;


        /// <summary>
        /// 碰撞层
        /// </summary>
        [Tooltip("碰撞层")] public LayerMask layerMask;

        /// <summary>
        /// 是否跳起
        /// </summary>
        [HideInInspector] public bool isJump;

        /// <summary>
        /// 跳跃次数
        /// </summary>
        [HideInInspector] public int jumpCount;

        /// <summary>
        /// 状态机
        /// </summary>
        private StateMachine _stateMachine;

        private void Awake()
        {
            // 获取组件
            var idleToRun = GetComponent<IdleToRunTrigger>();
            var runToIdle = GetComponent<RunToIdleTrigger>();
            var idleToJump = GetComponent<IdleToJumpTrigger>();
            var jumpToFall = GetComponent<JumpToFallTrigger>();
            var fallToIdle = GetComponent<FallToIdleTrigger>();
            var runToJump = GetComponent<RunToJumpTrigger>();
            var fallToRun = GetComponent<FallToRunTrigger>();
            var fallToJump = GetComponent<FallToJumpTrigger>();

            // 创建状态机
            _stateMachine = new StateMachine();

            // 获取状态
            var runState = new RunState(this);
            var idleState = new IdleState(this);
            var jumpState = new JumpState(this);
            var fallState = new FallState(this);

            // 添加转换
            At(idleState, runState, () => idleToRun.Predicate());
            At(runState, idleState, () => runToIdle.Predicate());
            At(idleState, jumpState, () => idleToJump.Predicate());
            At(jumpState, fallState, () => jumpToFall.Predicate());
            At(fallState, idleState, () => fallToIdle.Predicate());
            At(runState, jumpState, () => runToJump.Predicate());
            At(fallState, runState, () => fallToRun.Predicate());
            At(fallState, jumpState, () => fallToJump.Predicate());

            // 切换状态到默认状态
            _stateMachine.ChangeState(idleState);

            void At(AbstractState from, AbstractState to, Func<bool> condition) =>
                _stateMachine.AddTransition(from, to, condition);
        }
        
        void Update()
        {
            _stateMachine.Tick();
        }

        void FixedUpdate()
        {
            if (isJump && jumpCount < 2)
            {
                Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
                rigidbody2D.AddForce(Vector2.up * jumVelocity, ForceMode2D.Impulse);
                isJump = false;
                jumpCount++;
            }
        }
    }
}