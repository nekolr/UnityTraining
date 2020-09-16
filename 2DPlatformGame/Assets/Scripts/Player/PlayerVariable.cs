namespace Player
{
    public static class PlayerVariable
    {
        // 移动速度
        public static float RunVelocity = 5f;

        // 跳跃速度
        public static float JumpVelocity = 5;

        // 下降时速度变化率
        public static float FallMultiplier = 2.5f;

        // 向上跳时速度变化率
        public static float LowJumpMultiplier = 2f;

        
        public static string IsRun = "isRun";
        public static string IsJump = "isJump";
        public static string IsFall = "isFall";
    }
}