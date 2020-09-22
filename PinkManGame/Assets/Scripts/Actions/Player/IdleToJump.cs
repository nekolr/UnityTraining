using System;
using MyStateMachine;
using UnityEngine;

namespace Actions.Player
{
    public class IdleToJump : AbstractTrigger
    {
        /// <summary>
        /// 哪些层需要碰撞检测
        /// </summary>
        [Tooltip("哪些层需要碰撞检测")] public LayerMask layerMask;

        // 精灵大小（玩家盒子的大小）
        private Vector2 playerSize;

        // 碰撞检测盒子大小
        private Vector2 boxSize;

        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            // 获取精灵边框的大小
            playerSize = GetComponent<SpriteRenderer>().bounds.size;
            // 获取碰撞检测盒子的大小，使用玩家的大小 80%，同时高度设置为 0.5
            boxSize = new Vector2(playerSize.x * 0.8f, PlayerEntry.BoxHeight);

            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public override bool Predicate()
        {
            bool isJump = Input.GetButtonDown("Jump") && Grounded();
            
            // Debug.Log("---------------IdleToJump: " + isJump);

            return isJump;
        }

        /// <summary>
        /// 检测是否落地
        /// </summary>
        /// <returns></returns>
        private bool Grounded()
        {
            // 先将碰撞盒子的位置移动到玩家脚部位置
            // transform.position 的位置是精灵的正中心，如果盒子移到这里是无法进行碰撞检测的，需要移动到脚部
            Vector2 jumpBoxPosition = (Vector2) transform.position + (Vector2.down * playerSize * 0.5f);

            // layerMask 代表碰撞发生时，碰撞盒子与哪些图层进行了交互，比如与 Bound、Foreground
            // 如果条件成立，说明碰撞发生了
            return Physics2D.OverlapBox(jumpBoxPosition, boxSize, 0, layerMask) != null;
        }
    }
}