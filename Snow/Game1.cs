using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GameEngine
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Random rnd = new Random();
        Texture2D snow2 , zima;
        public List<Snow> Snowflakes = new List<Snow>();
        private KeyboardState Start = Keyboard.GetState();
        private KeyboardState Stop;
        public Game1() //This is the constructor, this function is called whenever the game class is created.
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = true;
            AddCreateSnow();
            graphics.ApplyChanges();
        }

        protected void AddCreateSnow()
        {
            for (int i = 0; i < 5000; i++)
            {
                Snowflakes.Add(new Snow
                {
                    X = rnd.Next(graphics.PreferredBackBufferWidth),
                    Y = -rnd.Next(graphics.PreferredBackBufferHeight),
                    Severity = rnd.Next(5, 15),
                });
            }
        }

        /// <summary>
        /// Automatically called when your game launches to load any game assets (graphics, audio etc.)
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            zima = Content.Load<Texture2D>("..\\..\\..\\Content/zima.jpg");
            snow2 = Content.Load<Texture2D>("..\\..\\..\\Content/snow2.png");
        }

        /// <summary>
        /// Called each frame to update the game. Games usually runs 60 frames per second.
        /// Each frame the Update function will run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        protected override void Update(GameTime gameTime)
        {
            Start = Keyboard.GetState();
            Stop = Start;

            foreach (var snowflake in Snowflakes)
            {
                snowflake.Y += snowflake.Severity / 3;
                if (snowflake.Y > graphics.PreferredBackBufferHeight)
                {
                    snowflake.Y = -snowflake.Severity;
                }
            }
            
            if (Stop.IsKeyDown(Keys.Escape))
            {
                Exit();
            }


        }

        /// <summary>
        /// This is called when the game is ready to draw to the screen, it's also called each frame.
        /// </summary>
        protected override void Draw(GameTime gameTime)
        {
                spriteBatch.Begin();
                spriteBatch.Draw(zima,
                    new Rectangle(0, 0,
                    graphics.PreferredBackBufferWidth,
                    graphics.PreferredBackBufferHeight),
                    Color.White);

                foreach (var snowflake in Snowflakes)
                {
                    spriteBatch.Draw(snow2, new Rectangle(snowflake.X, snowflake.Y, snowflake.Severity, snowflake.Severity), Color.LightCyan);                                                                        
                }
                spriteBatch.End();
        }
    }
}
