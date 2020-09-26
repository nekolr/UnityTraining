namespace Player
{
    /// <summary>
    /// 状态枚举
    /// </summary>
    public enum StateID
    {
        /// <summary>
        /// 静止
        /// </summary>
        Idle,

        /// <summary>
        /// 奔跑
        /// </summary>
        Run,

        /// <summary>
        /// 跳跃
        /// </summary>
        Jump,

        /// <summary>
        /// 下落
        /// </summary>
        Fall,

        /// <summary>
        /// 受伤
        /// </summary>
        Hurt,

        /// <summary>
        /// 下蹲
        /// </summary>
        Crouch,

        /// <summary>
        /// 死亡
        /// </summary>
        Death
    }
}