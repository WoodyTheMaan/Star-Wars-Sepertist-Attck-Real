using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Star_Wars_Sepertist_Attck;
using System;
using System.Collections.Generic;

namespace Star_Wars_Sepertist_Attck__Real
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        SoundEffectInstance menuMusic, droidAttackTheme, optionTheme;
        SpriteFont copyrightFont, cloneStory;
        SoundEffect buttonClicked;
        Texture2D menuImage, logo, theGrass;
        ButtonClass[] menuButtons;
        ButtonClass[] optionButtons;
        float timer;
        Random generator = new Random();

        int speedOfDroid = 1, playerWinningSoFar = 0, whereFirstDroidSpawns, whereSecondDroidSpawns;
        CloneTrooperClass theCloneTrooper;
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
            optionTheme = Content.Load<SoundEffect>("Option Theme").CreateInstance();
            theGrass = Content.Load<Texture2D>("TheGameGrass");
            cloneStory = Content.Load<SpriteFont>("File");
            var spriteSheetCoordinates = new Rectangle[]
            {
                new Rectangle(7, 445, 50, 58), //StandStill
                new Rectangle(57, 443, 46, 61), //StandStill  
                new Rectangle(106, 442, 49, 62), //StandStill
                new Rectangle(157, 444, 47, 60), //walk2  
                new Rectangle(206, 445, 46, 59),
                new Rectangle(254, 444, 48, 61),
                new Rectangle(303, 443, 50, 62),
                new Rectangle(356, 445, 48, 60),
            };
            droidspriteSheetCoordinates = new Rectangle[]
            {
                new Rectangle(76, 59, 36, 50), //StandStill
                new Rectangle(115, 58, 36, 50), //StandStill  
                new Rectangle(153, 59, 36, 50), //StandStill
                new Rectangle(191, 60, 36, 50), //walk2  
                new Rectangle(229, 59, 36, 50),
                new Rectangle(267, 58, 36, 50),
                new Rectangle(309, 59, 36, 50),
                new Rectangle(343, 60, 36, 50),
                //deez nuts
            };
            theCloneTrooper = new CloneTrooperClass(spriteSheet, spriteSheetCoordinates, 0, new Rectangle(300, 200, 50, 110), 7, Content.Load<Texture2D>("CloneBlast"), Content.Load<SoundEffect>("sounds/Clone BlastShotfIX"));
            enemies = new()
            {
                new Droid(droidSpriteSheet, droidspriteSheetCoordinates, 0, new Rectangle(0, 0, 80, 110), 1, rectTex),
                new Droid(droidSpriteSheet, droidspriteSheetCoordinates, 0, new Rectangle(200, 90, 80, 110), 1, rectTex),
                new Droid(droidSpriteSheet, droidspriteSheetCoordinates, 0, new Rectangle(123, 16, 80, 110), 1, rectTex),

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
                new(rectTex, new Rectangle(240, 225, 190, 40), "INFORMATION", Content.Load<SoundEffect>("sounds/Menu Button Hover"), Content.Load<SpriteFont>("File")),
                new(rectTex, new Rectangle(220, 300, 235, 40), "EXIT TO WINDOWS", Content.Load<SoundEffect>("sounds/Menu Button Hover"), Content.Load<SpriteFont>("File"))

            };
            optionButtons = new ButtonClass[]
            {
                new(rectTex, new Rectangle(450, 435, 200, 40), "Back To Menu", Content.Load<SoundEffect>("sounds/Menu Button Hover"), Content.Load<SpriteFont>("File"))
            };
        }

        protected override void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            keyboard = Keyboard.GetState();
            this.Window.Title = $"The speed of the droids are {speedOfDroid} | Your Republic class has upgraded from 3 to {playerWinningSoFar + 3}";
            if (screen == Screen.Menu)
            {
                playerWinningSoFar = 0;
                menuMusic.Play();
                var mouse = Mouse.GetState();
                if (menuButtons[0].Update(mouse))
                {
                    screen = Screen.TheGame;
                    optionTheme.Stop();
                    menuMusic.Stop();
                    buttonClicked.Play();
                    speedOfDroid = 1;
                    timer = 0;
                    enemies.Clear();
                }
                else if (menuButtons[1].Update(mouse))
                {
                    screen = Screen.Options;
                    menuMusic.Stop();
                    optionTheme.Play();
                    buttonClicked.Play();

                }
                else if (menuButtons[2].Update(mouse))
                {
                    Exit();
                    menuMusic.Stop();   
                    buttonClicked.Play();
                }
            }
            if (screen == Screen.Options)
            {
                var mouse = Mouse.GetState();
                if (optionButtons[0].Update(mouse))
                {
                    screen = Screen.Menu;
                    buttonClicked.Play();
                    optionTheme.Stop();

                }
            }
            else if (screen == Screen.TheGame)
            {
                droidAttackTheme.Play();
                if (theCloneTrooper.Update(keyboard, _graphics, enemies))
                {
                    droidAttackTheme.Stop();
                    screen = Screen.Menu;
                }

                if (timer > 3)
                {
                    int offset = 6;
                    var cloneRect = new Rectangle(theCloneTrooper.Location.X - offset, theCloneTrooper.Location.Y - offset, theCloneTrooper.Location.Width + (offset * 2), theCloneTrooper.Location.Height + (offset * 2));
                    for (int i = 0; i < 2; i++)
                    {
                        whereFirstDroidSpawns = generator.Next(1, 5);
                        var droidRect = cloneRect;
                        if (whereFirstDroidSpawns == 1)
                        {
                            while (droidRect.Intersects(cloneRect))
                            {
                                droidRect = new Rectangle(generator.Next(700, 712), generator.Next(0, 501), 80, 110);
                            }
                            enemies.Add(new Droid(droidSpriteSheet, droidspriteSheetCoordinates, 0, droidRect, speedOfDroid, rectTex));
                        }
                        else if (whereFirstDroidSpawns == 2)
                        {
                            while (droidRect.Intersects(cloneRect))
                            {
                                droidRect = new Rectangle(generator.Next(0, 12), generator.Next(0, 501), 80, 110);
                            }
                            enemies.Add(new Droid(droidSpriteSheet, droidspriteSheetCoordinates, 0, droidRect, speedOfDroid, rectTex));
                        }
                        else if (whereFirstDroidSpawns == 3)
                        {
                            while (droidRect.Intersects(cloneRect))
                            {
                                droidRect = new Rectangle(generator.Next(0, 700), generator.Next(500, 512), 80, 110);
                            }
                            enemies.Add(new Droid(droidSpriteSheet, droidspriteSheetCoordinates, 0, droidRect, speedOfDroid, rectTex));
                        }
                        else
                        {
                            while (droidRect.Intersects(cloneRect))
                            {
                                droidRect = new Rectangle(generator.Next(700, 712), generator.Next(0, 501), 80, 110);
                            }
                            enemies.Add(new Droid(droidSpriteSheet, droidspriteSheetCoordinates, 0, droidRect, speedOfDroid, rectTex));
                        }


                    }
                    timer = 0;
                    playerWinningSoFar += 1;
                    if (playerWinningSoFar == 12)
                    {
                        speedOfDroid += 1;
                    }
                    else if (playerWinningSoFar == 25)
                    {
                        speedOfDroid = speedOfDroid + 1;
                    }
                    else if (playerWinningSoFar == 40)
                    {
                        speedOfDroid = speedOfDroid + 2;
                    }
                    else if (playerWinningSoFar == 100)
                    {
                        speedOfDroid = speedOfDroid + 2;
                    }
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
                _spriteBatch.Draw(theGrass, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
                theCloneTrooper.Draw(_spriteBatch);
                foreach (Droid e in enemies)
                    e.Draw(_spriteBatch);
               
            }
            else if (screen == Screen.Options)
            {
                foreach (ButtonClass b in optionButtons)
                    b.Draw(_spriteBatch);
                _spriteBatch.DrawString(cloneStory, "As the Clone Wars continues the sepertists push\n the Republic back but the Republic won't go\n without a fight, they are sending Clone 991A25\n on a mission  alone to fight a small invasion of\n sepertists on the planet of Naboo. As\n Clone 991A25 pushes the sepertists back the\n Republic is trying to regain political gain\n as the people of the galaxy fear the sepertists\n strong push onto the Republic. The Jedi Order\n has chosen to stay out of the politics of this\n push and continue to search for the Sith\n and their motives...", new Vector2(10, 10), Color.Yellow);
            }


            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}