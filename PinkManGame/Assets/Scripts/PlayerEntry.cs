using System;
using Actions.Player;
using MyStateMachine;
using States.Player;
using UnityEngine;

public class PlayerEntry : MonoBehaviour
{
    /// <summary>
    /// 奔跑速度
    /// </summary>
    [Tooltip("奔跑速度")] public static float Speed = 5f;

    /// <summary>
    /// 跳跃速度
    /// </summary>
    [Tooltip("跳跃速度")] [Range(0, 10)] public static float JumpVelocity = 5f;

    /// <summary>
    /// 下降时速度变化率
    /// </summary>
    [Tooltip("下降时速度变化率")] public static float FallMultiplier = 1.5f;

    /// <summary>
    /// 向上跳时速度变化率
    /// </summary>
    [Tooltip("向上跳时速度变化率")] public static float LowJumpMultiplier = 2f;

    /// <summary>
    /// 落地碰撞检测盒子的高度（落地检测的碰撞只需要脚部那部分的盒子即可）
    /// </summary>
    [Tooltip("落地碰撞检测盒子的高度")] public static float BoxHeight = 0.5f;

    /// <summary>
    /// 哪些层需要碰撞检测
    /// </summary>
    [Tooltip("哪些层需要碰撞检测")] public LayerMask layerMask;

    
    /// <summary>
    /// 
    /// </summary>
    public bool IsJumpFlag { get; set; }
    
    public bool IsFallFlag { get; set; }

    public bool IsGroundFlag { get; set; }

    /// <summary>
    /// 状态机
    /// </summary>
    private StateMachine _stateMachine;

    /// <summary>
    /// 
    /// </summary>
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        // 获取游戏对象或组件
        var idleToRun = GetComponent<IdleToRun>();
        var runToIdle = GetComponent<RunToIdle>();
        var idleToJump = GetComponent<IdleToJump>();
        var jumpToFall = GetComponent<JumpToFall>();
        var runToJump = GetComponent<RunToJump>();
        var fallToIdle = GetComponent<FallToIdle>();
        var fallToRun = GetComponent<FallToRun>();
        var runToFall = GetComponent<RunToFall>();
        var fallToJump = GetComponent<FallToJump>();

        var animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();

        // 创建状态机
        _stateMachine = new StateMachine();

        // 获取状态
        var idleState = new IdleState(animator);
        var runState = new RunState(animator, _rigidbody2D);
        var jumpState = new JumpState(this, animator, _rigidbody2D);
        var fallState = new FallState(this, animator, _rigidbody2D);

        // 添加转换
        At(idleState, runState, () => idleToRun.Predicate());
        At(runState, idleState, () => runToIdle.Predicate());
        At(idleState, jumpState, () => idleToJump.Predicate());
        At(jumpState, fallState, () => jumpToFall.Predicate());
        At(fallState, idleState, () => fallToIdle.Predicate());
        At(runState, jumpState, () => runToJump.Predicate());
        At(fallState, runState, () => fallToRun.Predicate());
        At(runState, fallState, () => runToFall.Predicate());
        At(fallState, jumpState, () => fallToJump.Predicate());

        // 转换状态
        _stateMachine.ChangeState(idleState);

        // 添加转换的方法
        void At(AbstractState from, AbstractState to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);
    }

    private void Update()
    {
        // 入口
        _stateMachine.Tick();
    }

    private void FixedUpdate()
    {
        // 一直按着跳跃键
        if (_rigidbody2D.velocity.y > 0 && Input.GetButton("Jump"))
        {
            // 刚体受重力的影响为原值的 0.5
            _rigidbody2D.gravityScale = 0.5f;
        }
        // 没有一直按着跳跃键
        if (_rigidbody2D.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            // 调整刚体受重力的影响程度
            _rigidbody2D.gravityScale = LowJumpMultiplier;
        }
        // 刚体在下降
        if (_rigidbody2D.velocity.y < 0)
        {
            // 调整刚体受重力的影响程度
            _rigidbody2D.gravityScale = FallMultiplier;
            // IsFallFlag = false;
        }
        
        if (IsJumpFlag)
        {
            _rigidbody2D.AddForce(Vector2.up * JumpVelocity, ForceMode2D.Impulse);
            IsJumpFlag = false;
            IsGroundFlag = false;
        }

        if (IsFallFlag)
        {
            
        }
    }

    private void OnDrawGizmos()
    {
        // 获取精灵边框的大小
        var playerSize = GetComponent<SpriteRenderer>().bounds.size;
        // 获取碰撞检测盒子的大小，使用玩家的大小 80%，同时高度设置为 0.5
        var boxSize = new Vector2(playerSize.x * 0.8f, PlayerEntry.BoxHeight);

        // 先将碰撞盒子的位置移动到玩家脚部位置
        // transform.position 的位置是精灵的正中心，如果盒子移到这里是无法进行碰撞检测的，需要移动到脚部
        Vector2 jumpBoxPosition = (Vector2) transform.position + (Vector2.down * playerSize * 0.5f);

        // layerMask 代表碰撞发生时，碰撞盒子与哪些图层进行了交互，比如与 Bound、Foreground
        // 如果条件成立，说明碰撞发生了
        bool isGrounded = Physics2D.OverlapBox(jumpBoxPosition, boxSize, 0, layerMask) != null;

        if (isGrounded)
        {
            Gizmos.color = Color.red;
        }
        else
        {
            Gizmos.color = Color.green;
        }

        Gizmos.DrawWireCube(jumpBoxPosition, boxSize);
    }
}