using MyStateMachine;
using UnityEngine;

namespace Player.States
{
    public class RunState : AbstractState
    {
        private readonly Animator _animator;
        private readonly Rigidbody2D _rigidbody2D;
        private readonly LayerMask _layerMask;
        private readonly Vector2 _playerSize;
        private readonly Vector2 _boxSize;
        private readonly Transform _transform;

        public RunState(PlayerEntry playerEntry) : base(playerEntry.StateMachine, playerEntry.StateDictionary)
        {
            _animator = playerEntry.GetComponent<Animator>();
            _rigidbody2D = playerEntry.GetComponent<Rigidbody2D>();
            _transform = playerEntry.transform;
            // 玩家精灵边框的大小
            _playerSize = playerEntry.GetComponent<SpriteRenderer>().bounds.size;
            // 碰撞检测盒子的大小，使用玩家的大小 40%，同时高度设置为 0.5
            _boxSize = new Vector2(_playerSize.x * 0.4f, PlayerVariables.BoxHeight);
            _layerMask = playerEntry.layerMask;
        }

        public override void Enter()
        {
            _animator.SetBool("isRun", true);
            // 重置跳跃次数
            PlayerVariables.JumpCount = 0;
        }

        public override void ExecuteByUpdate()
        {
            TransitionTrigger();
            Run();
        }

        private void Run()
        {
            float direction = Input.GetAxisRaw("Horizontal");
            // 速度向量
            _rigidbody2D.velocity = new Vector2(direction * PlayerVariables.Speed, _rigidbody2D.velocity.y);
            if (direction != 0)
            {
                // 方向变换
                _rigidbody2D.transform.localScale = new Vector3(direction, 1, 1);
            }
        }

        private void TransitionTrigger()
        {
            var isOnTheGround = IsOnTheGround();
            
            if (Input.GetAxisRaw("Horizontal") == 0)
            {
                StateMachine.ChangeState(StateDictionary[StateID.Idle]);
            }

            if (Input.GetAxisRaw("Horizontal") != 0 && Input.GetButtonDown("Jump") && isOnTheGround)
            {
                StateMachine.ChangeState(StateDictionary[StateID.Jump]);
            }
            
            if (PlayerVariables.IsHurt)
            {
                StateMachine.ChangeState(StateDictionary[StateID.Hurt]);
            }
            
            // 下坡路段不算下落状态
            if (_rigidbody2D.velocity.y < 0 && !isOnTheGround)
            {
                StateMachine.ChangeState(StateDictionary[StateID.Fall]);
            }
        }
        
        private bool IsOnTheGround()
        {
            // 先将碰撞盒子的位置移动到玩家脚部位置
            // transform.position 的位置是精灵的正中心，如果盒子移到这里是无法进行碰撞检测的，需要移动到脚部
            Vector2 jumpBoxPosition = (Vector2) _transform.position + (Vector2.down * _playerSize * 0.5f);
            return Physics2D.OverlapBox(jumpBoxPosition, _boxSize, 0, _layerMask) != null;
        }
    }
}