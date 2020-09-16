namespace Player
{
    /// <summary>
    /// 状态编号
    /// </summary>
    public enum StateID
    {
        /// <summary>
        /// 不存在该状态
        /// </summary>
        None,

        /// <summary>
        /// 默认状态
        /// </summary>
        Default,

        /// <summary>
        /// 奔跑
        /// </summary>
        Run,

        /// <summary>
        /// 跳跃
        /// </summary>
        Jump,

        /// <summary>
        /// 静止
        /// </summary>
        Idle,

        /// <summary>
        /// 下降
        /// </summary>
        Fall,

        /// <summary>
        /// 二次跳跃
        /// </summary>
        DoubleJump,

        /// <summary>
        /// 二次下降
        /// </summary>
        DoubleFall
    }
}