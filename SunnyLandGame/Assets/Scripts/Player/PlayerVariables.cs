namespace Player
{
    /// <summary>
    /// 玩家变量
    /// </summary>
    public static class PlayerVariables
    {
        /// <summary>
        /// 奔跑速度
        /// </summary>
        public const float Speed = 5;

        /// <summary>
        /// 跳跃速度
        /// </summary>
        public const float JumpVelocity = 10f;

        /// <summary>
        /// 跳跃次数
        /// </summary>
        public static int JumpCount = 0;

        /// <summary>
        /// 是否为跳跃
        /// </summary>
        public static bool IsJump = false;

        /// <summary>
        /// 是否受伤
        /// </summary>
        public static bool IsHurt = false;

        /// <summary>
        /// 碰撞检测盒子的高度
        /// </summary>
        public const float BoxHeight = 0.5f;
    }
}