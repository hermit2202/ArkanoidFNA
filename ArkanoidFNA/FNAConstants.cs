namespace ArkanoidFNA
{
    /// <summary>
    /// Статический класс, содержащий константы для настройки UI-элементов игры.
    /// </summary>
    public static class FNAConstants
    {
        /// <summary>
        /// Размер иконки сердца (жизни) в пикселях.
        /// </summary>
        public const int HeartSize = 24;

        /// <summary>
        /// Отступ между иконками сердец при отображении жизней.
        /// </summary>
        public const int HeartSpacing = 8;

        /// <summary>
        /// Отступ от правого края экрана до начала группы жизней.
        /// </summary>
        public const int LivesPaddingRight = 20;

        /// <summary>
        /// Отступ от верхнего края экрана до строки жизней.
        /// </summary>
        public const int LivesPaddingTop = 3;

        /// <summary>
        /// Толщина линии (не используется в текущей реализации, зарезервировано).
        /// </summary>
        public const int LivesLineThickness = 2;

        /// <summary>
        /// Масштаб логотипа на главном экране меню (относительно исходного размера).
        /// </summary>
        public const float MenuLogoScale = 0.6f;

        /// <summary>
        /// Координата Y для позиционирования логотипа в меню.
        /// </summary>
        public const float MenuLogoY = 100;

        /// <summary>
        /// Координата Y для текста-подсказки "нажмите Enter" в меню.
        /// </summary>
        public const float MenuTextY = 400;

        /// <summary>
        /// Масштаб изображений на экранах окончания игры (Game Over / Victory).
        /// </summary>
        public const float EndScreenScale = 0.5f;

        /// <summary>
        /// Координата Y для изображения на экранах Game Over и Victory.
        /// </summary>
        public const float EndScreenLogoY = 150;

        /// <summary>
        /// Координата Y для текста "нажмите R" на экранах окончания игры.
        /// </summary>
        public const float EndScreenTextY = 350;


        /// <summary>
        /// Коэффициент прозрачности для затемняющего оверлея на экранах меню и окончания игры.
        /// </summary>
        public const float OverlayOpacity = 0.7f;
    }

    /// <summary>
    /// Перечисление, определяющее текущее состояние игрового цикла.
    /// Используется в классе Arkanoid для переключения между экранами:
    /// главное меню, активная игра, экран поражения и экран победы.
    /// Каждое состояние определяет, какие методы отрисовки и обработки ввода активны.
    /// </summary>
    enum GameMode
    {
        /// <summary>
        /// Главное меню.
        /// </summary>
        Menu,

        /// <summary>
        /// Активная игра.
        /// </summary>
        Playing,

        /// <summary>
        /// Экран поражения.
        /// </summary>
        GameOver,

        /// <summary>
        /// Экран победы.
        /// </summary>
        Victory
    }
}
