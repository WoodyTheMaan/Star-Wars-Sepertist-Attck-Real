using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Star_Wars_Sepertist_Attck;
using System.Collections.Generic;
using System.Dynamic;

namespace Star_Wars_Sepertist_Attck__Real
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        SoundEffectInstance menuMusic, droidAttackTheme;
        SpriteFont copyrightFont;
        SoundEffect buttonClicked;
        Texture2D menuImage, logo;
        ButtonClass[] menuButtons;
        float timer;
        CloneTrooperClass josiah;
        List<Droid> enemies;
        Texture2D droidSpriteSheet, rectTex;
        Rectangle[] droidspriteSheetCoordinates;

        Screen screen;
        KeyboardState keyboard;
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
            
          
            screen = Screen.Menu;
            base.Initialize();

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            rectTex = Content.Load<Texture2D>("rectangle");

            var spriteSheet = Content.Load<Texture2D>("Clone Trooper Better Sprite Sheet");
            droidSpriteSheet = Content.Load<Texture2D>("BattleDroid SpreadSheet");
            droidAttackTheme = Content.Load<SoundEffect>("sounds/DroidAttackTheme").CreateInstance();
            var spriteSheetCoordinates = new Rectangle[]
            {
                new Rectangle(16, 23, 23,42), //StandStill
                new Rectangle(16, 23, 23,42), //StandStill  
                new Rectangle(16, 23, 23,42), //StandStill
                new Rectangle(167, 150, 25, 42), //walk2  
                new Rectangle (190, 165, 21, 69)
            }; 
            droidspriteSheetCoordinates = new Rectangle[]
            {
                new Rectangle(16, 23, 23,42), //StandStill
                new Rectangle(16, 23, 23,42), //StandStill  
                new Rectangle(16, 23, 23,42), //StandStill
                new Rectangle(167, 150, 25, 42), //walk2  
                new Rectangle (190, 165, 21, 69)
            };
            josiah = new CloneTrooperClass(spriteSheet, spriteSheetCoordinates, 0, new Rectangle(300, 200, 50, 110), 5, Content.Load<Texture2D>("CloneBlast"), Content.Load<SoundEffect>("sounds/Clone BlastShotfIX"));
            enemies = new()
            {
                //new Droid(droidSpriteSheet, droidspriteSheetCoordinates, 0, new Rectangle(300, 200, 80, 110), 1, rectTex),

            };
            
            logo = Content.Load<Texture2D>("RealProjectLogo");
            menuImage = Content.Load<Texture2D>("Main Menu Picture");
            menuMusic = Content.Load<SoundEffect>("sounds/CloneGame Menu Theme").CreateInstance();
            buttonClicked = Content.Load<SoundEffect>("Menu Button Clicked");
            //copyrightFont = Content.Load<SpriteFont>("Death Star");
            // TODO: use this.Content to load your game content here
            menuButtons = new ButtonClass[]
            {
                new(rectTex, new Rectangle(220, 150, 235, 40), "PLAY CAMPAIGN", Content.Load<SoundEffect>("sounds/Menu Button Hover"), Content.Load<SpriteFont>("File")),
                new(rectTex, new Rectangle(275, 225, 125, 40), "OPTIONS", Content.Load<SoundEffect>("sounds/Menu Button Hover"), Content.Load<SpriteFont>("File")),
                new(rectTex, new Rectangle(220, 300, 235, 40), "EXIT TO WINDOWS", Content.Load<SoundEffect>("sounds/Menu Button Hover"), Content.Load<SpriteFont>("File"))
                
            };
        }

        protected override void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            keyboard = Keyboard.GetState();    
            if (screen == Screen.Menu)
            {
                menuMusic.Play();
                var mouse = Mouse.GetState();
                if (menuButtons[0].Update(mouse))
                {
                    screen = Screen.TheGame;
                    menuMusic.Stop();
                    buttonClicked.Play();
                    enemies.Clear();
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
            else if (screen == Screen.TheGame)
            {
                droidAttackTheme.Play();
                if (josiah.Update(keyboard, _graphics, enemies))
                {
                    screen = Screen.Menu;
                }
                
                if (timer > 3)
                {
                    enemies.Add(new Droid(droidSpriteSheet, droidspriteSheetCoordinates, 0, new Rectangle(0, 200, 80, 110), 1, rectTex));
                    timer = 0; 
                    
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
               // _spriteBatch.DrawString(copyrightFont, "Hello", new Vector2(120, 120), Color.White );
            }
            else if (screen == Screen.TheGame)
            {
                josiah.Draw(_spriteBatch);
                foreach (Droid e in enemies)
                    e.Draw(_spriteBatch);
            }
            else if (screen == Screen.Options)
            {

            }


            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}