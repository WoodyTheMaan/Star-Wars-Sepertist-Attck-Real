﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Star_Wars_Sepertist_Attck__Real
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Rectangle[] spriteSheetCoordinates;
        int currentSprite;
        SoundEffectInstance menuMusic;
        SoundEffect menuMusicMenu;
        Texture2D menuImage;
        Texture2D texture;
        float timer;
        Screen screen;
        enum Screen
        {
            Menu
        }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferHeight = 500;
            _graphics.PreferredBackBufferWidth = 700;
            _graphics.ApplyChanges();
            spriteSheetCoordinates = new Rectangle[]
            {

                new Rectangle(16, 23, 23,42), //StandStill
                new Rectangle(445, 51, 22, 42), //Walk1
                new Rectangle(167, 150, 25, 42), //walk2
            };
            screen = Screen.Menu;
            base.Initialize();

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            texture = Content.Load<Texture2D>("Clone Trooper Better Sprite Sheet");
            menuImage = Content.Load<Texture2D>("Main Menu Picture");
            menuMusicMenu = Content.Load<SoundEffect>("CloneGame Menu Theme");
            menuMusic = menuMusicMenu.CreateInstance();
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timer > 250)
            {
                timer = 0;
                if (currentSprite == 1)
                {
                    currentSprite = 2;
                }
                else
                    currentSprite = 1;
            }
            if (screen == Screen.Menu)
            {
                menuMusic.Play();
            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();
            _spriteBatch.Draw(texture, new Vector2(), spriteSheetCoordinates[currentSprite], Color.White);
            if (screen == Screen.Menu)
            {
                _spriteBatch.Draw(menuImage, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
               
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}