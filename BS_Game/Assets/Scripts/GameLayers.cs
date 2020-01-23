namespace BSGame
{
    public static class GameLayers
    {
        public const int
            Wall = 8,
            Bullet = 9,
            Player = 10,
            Monster = 11;

        public const int 
            WallMask = 1 << Wall,
            BulletMask = 1 << Bullet,
            PlayerMask = 1 << Player,
            MonsterMask = 1 << Monster;
    }
}