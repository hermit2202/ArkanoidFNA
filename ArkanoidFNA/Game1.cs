using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ArkanoidFNA
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Добавьте вашу логику инициализации здесь
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // TODO: Загрузите текстуры и другие ресурсы здесь
        }

        protected override void Update(GameTime gameTime)
        {
            // TODO: Добавьте логику обновления здесь

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Добавьте отрисовку объектов здесь

            base.Draw(gameTime);
        }
    }
}