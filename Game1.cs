using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Star_Wars_Sepertist_Attck;

namespace Star_Wars_Sepertist_Attck__Real
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Rectangle[] spriteSheetCoordinates;
        int currentSprite;
        SoundEffectInstance menuMusic;
        SoundEffect buttonClicked;
        Texture2D menuImage, spriteSheet, logo;
        ButtonClass[] menuButtons;
        float timer;
        Screen screen;
        enum Screen
        {
            Menu,
            TheGame,
            Options
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
            spriteSheet = Content.Load<Texture2D>("Clone Trooper Better Sprite Sheet");
            logo = Content.Load<Texture2D>("RealProjectLogo");
            menuImage = Content.Load<Texture2D>("Main Menu Picture");
            menuMusic = Content.Load<SoundEffect>("sounds/CloneGame Menu Theme").CreateInstance();
            buttonClicked = Content.Load<SoundEffect>("Menu Button Clicked");
            // TODO: use this.Content to load your game content here
            Texture2D rectTex = Content.Load<Texture2D>("rectangle");
            menuButtons = new ButtonClass[]
            {
                new(rectTex, new Rectangle(220, 150, 235, 40), "PLAY CAMPAIGN", Content.Load<SoundEffect>("sounds/Menu Button Hover"), Content.Load<SpriteFont>("File")),
                new(rectTex, new Rectangle(275, 225, 125, 40), "OPTIONS", Content.Load<SoundEffect>("sounds/Menu Button Hover"), Content.Load<SpriteFont>("File")),
                new(rectTex, new Rectangle(220, 300, 235, 40), "EXIT TO WINDOWS", Content.Load<SoundEffect>("sounds/Menu Button Hover"), Content.Load<SpriteFont>("File"))

            };
        }

        protected override void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (screen == Screen.Menu)
            {
                menuMusic.Play();
                var mouse = Mouse.GetState();
                if (menuButtons[0].Update(mouse))
                {
                    screen = Screen.TheGame;
                    menuMusic.Stop();
                    buttonClicked.Play();
                }
                else if (menuButtons[1].Update(mouse))
                {
                    screen = Screen.Options;
                    buttonClicked.Play();
                }
                else if (menuButtons[2].Update(mouse))
                {
                    Exit();
                    buttonClicked.Play();
                }
            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // TODO: Add your drawing code here

            _spriteBatch.Begin();
            //_spriteBatch.Draw(spriteSheet, new Vector2(), spriteSheetCoordinates[currentSprite], Color.White);
            if (screen == Screen.Menu)
            {

                _spriteBatch.Draw(menuImage, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
                _spriteBatch.Draw(logo, new Rectangle(179, 10, 330, 130), Color.White);
                foreach (ButtonClass b in menuButtons)
                    b.Draw(_spriteBatch);
            }
            else if (screen == Screen.TheGame)
            {
                _spriteBatch.Draw(spriteSheet, new Rectangle(179, 10, 330, 130), spriteSheetCoordinates[0], Color.White);
            }
            else if (screen == Screen.Options)
            {

            }


            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}