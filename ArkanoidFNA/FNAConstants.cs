namespace ArkanoidFNA
{
    public static class FNAConstants
    {
        public const int HeartSize = 24;
        public const int HeartSpacing = 8;
        public const int LivesPaddingRight = 20;
        public const int LivesPaddingTop = 3;
        public const int LivesLineThickness = 2;
    }

    enum GameMode
    {
        Menu,
        Playing,
        GameOver,
        Victory
    }
}
