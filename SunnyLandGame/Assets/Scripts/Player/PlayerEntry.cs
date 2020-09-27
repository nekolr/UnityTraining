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

        private void Awake()
        {
            // 创建状态机
            StateMachine = new StateMachine();
            // 创建所有的状态
            var idleState = new IdleState(StateID.Idle, this);
            var runState = new RunState(StateID.Run, this);
            var jumpState = new JumpState(StateID.Jump, this);
            var fallState = new FallState(StateID.Fall, this);
            var hurtState = new HurtState(StateID.Hurt, this);
            var crouchState = new CrouchState(StateID.Crouch, this);
            var deathState = new DeathState(StateID.Death, this);
            // 初始化状态字典
            StateMachine.AddState(idleState);
            StateMachine.AddState(runState);
            StateMachine.AddState(jumpState);
            StateMachine.AddState(fallState);
            StateMachine.AddState(hurtState);
            StateMachine.AddState(crouchState);
            StateMachine.AddState(deathState);
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
                StateMachine.ChangeState(StateMachine.StateDictionary[StateID.Death]);
            }
        }
    }
}