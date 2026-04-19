using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Arkanoid.Logic.Core;
using Arkanoid.Logic;
using Arkanoid.Logic.Models;
using System.Collections.Generic;

namespace ArkanoidFNA
{
    public class Arkanoid : Game
    {
        private GameEngine engine;
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private GameMode currentMode = GameMode.Menu;

        Texture2D backgroundTexture;
        Texture2D platformTexture;
        Texture2D ballTexture;
        Texture2D heartTexture;
        Texture2D[] blockTexture;
        Dictionary<PowerUpType, Texture2D> powerUpTexture;

        public Arkanoid()
        {
            graphics = new GraphicsDeviceManager(this);

            graphics.PreferredBackBufferHeight = GameConstants.GameHeight;
            graphics.PreferredBackBufferWidth = GameConstants.GameWidth;

            graphics.ApplyChanges();

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            engine = new GameEngine();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            backgroundTexture = Content.Load<Texture2D>("Bkg");
            platformTexture = Content.Load<Texture2D>("Platform");
            ballTexture = Content.Load<Texture2D>("ball");
            heartTexture = Content.Load<Texture2D>("Heart");

            blockTexture = new Texture2D[3];
            blockTexture[0] = Content.Load<Texture2D>("Block1");
            blockTexture[1] = Content.Load<Texture2D>("Block2");
            blockTexture[2] = Content.Load<Texture2D>("Block3");

            powerUpTexture = new Dictionary<PowerUpType, Texture2D>();
            powerUpTexture[PowerUpType.WidePlatform] = Content.Load<Texture2D>("PowerUp1");
            powerUpTexture[PowerUpType.FireBall] = Content.Load<Texture2D>("PowerUp2");
            powerUpTexture[PowerUpType.FastBall] = Content.Load<Texture2D>("PowerUp3");
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            spriteBatch.Draw(backgroundTexture, Vector2.Zero, Color.White);

            spriteBatch.Draw(
                platformTexture,
                new Rectangle(
                    (int)engine.PlatformX,
                    (int)engine.PlatformY,
                    (int)engine.GetCurrentPlatformWidth(),
                    (int)GameConstants.PlatformSizeY
                ),
                Color.White
            );

            spriteBatch.Draw(
                ballTexture,
                new Rectangle(
                    (int)engine.BallX,
                    (int)engine.BallY,
                    GameConstants.BallSize,
                    GameConstants.BallSize
                ),
                Color.White
            );

            foreach (var block in engine.Blocks)
            {
                if (block.IsActive)
                {
                    int textureIndex = block.Strength - 1;

                    if (textureIndex >= 0 && textureIndex < blockTexture.Length)
                    {
                        spriteBatch.Draw(
                            blockTexture[textureIndex],
                            new Rectangle(
                                (int)block.X,
                                (int)block.Y,
                                (int)block.Width,
                                (int)block.Height
                            ),
                            Color.White
                        );
                    }
                }
            }

            int heartX = GameConstants.GameWidth - FNAConstants.LivesPaddingRight -
                (engine.Lives * (FNAConstants.HeartSize + FNAConstants.HeartSpacing));
            int heartY = FNAConstants.LivesPaddingTop;

            for (int i = 0; i < engine.Lives; i++)
            {
                spriteBatch.Draw(
                    heartTexture,
                    new Rectangle(
                        heartX + i * (FNAConstants.HeartSize + FNAConstants.HeartSpacing),
                        heartY,
                        FNAConstants.HeartSize,
                        FNAConstants.HeartSize
                    ),
                    Color.White
                );
            }

            foreach (var powerUp in engine.ActivePowerUps)
            {
                if (powerUp.IsActive && powerUpTexture.TryGetValue(powerUp.Type, out var texture))
                {
                    spriteBatch.Draw(
                        texture,
                        new Rectangle(
                            (int)powerUp.Rect.X,
                            (int)powerUp.Rect.Y,
                            (int)powerUp.Rect.Width,
                            (int)powerUp.Rect.Height
                        ),
                        Color.White
                     );
                }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        protected override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();

            switch (currentMode)
            {
                case GameMode.Menu:
                    if (keyboardState.IsKeyDown(Keys.Enter))
                    {
                        currentMode = GameMode.Playing;
                        engine.Start();
                    }
                    return;

                case GameMode.GameOver:
                case GameMode.Victory:
                    if (keyboardState.IsKeyDown(Keys.R))
                    {
                        currentMode = GameMode.Menu;
                        engine.RestartGame();
                    }
                    return;

                case GameMode.Playing:
                    if (engine.IsGameOver)
                    {
                        currentMode = GameMode.GameOver;
                        return;
                    }
                    if (engine.IsGameWon)
                    {
                        currentMode = GameMode.Victory;
                        return;
                    }
                    break;
            }

            var deltaTime = gameTime.ElapsedGameTime.TotalSeconds;

            engine.Update((float)deltaTime);

            var mouseState = Mouse.GetState();
            engine.MovePlatform(mouseState.X);

            base.Update(gameTime);
        }

        private void DrawMenuScreen(GameTime gameTime)
        {
            var whitePixel = new Texture2D(GraphicsDevice, 1, 1);
            whitePixel.SetData(new[] { Color.White });

            spriteBatch.Draw(
                whitePixel,
                new Rectangle(0, 0, GameConstants.GameWidth, GameConstants.GameHeight),
                Color.Black * 0.7f
                );

            spriteBatch.DrawString(
                gameFont,
                "Arkanoid",
                new Vector2(100, 100),
                Color.White
                );
        }

        private void DrawGameOverScreen(GameTime gameTime)
        {
            var whitePixel = new Texture2D(GraphicsDevice, 1, 1);
            whitePixel.SetData(new[] { Color.White });

            spriteBatch.Draw(
                whitePixel,
                new Rectangle(0, 0, GameConstants.GameWidth, GameConstants.GameHeight),
                Color.Black * 0.7f
            );

            spriteBatch.DrawString(
                gameFont,
                "Game Over!",
                new Vector2(200, 200),
                Color.Red
            );

            spriteBatch.DrawString(
                gameFont,
                "Press R to Restart",
                new Vector2(180, 280),
                Color.White
            );
        }

        private void DrawVictoryScreen(GameTime gameTime)
        {
            var whitePixel = new Texture2D(GraphicsDevice, 1, 1);
            whitePixel.SetData(new[] { Color.White });

            spriteBatch.Draw(
                whitePixel,
                new Rectangle(0, 0, GameConstants.GameWidth, GameConstants.GameHeight),
                Color.Black * 0.7f
            );

            spriteBatch.DrawString(
                gameFont,
                "Victory!",
                new Vector2(220, 200),
                Color.Green
            );

            spriteBatch.DrawString(
                gameFont,
                "Press R to Restart",
                new Vector2(180, 280),
                Color.White
            );
        }
    }
}

