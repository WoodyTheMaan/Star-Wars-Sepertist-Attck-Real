using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace STAR_WARS_SEPERTIST_ATTACK_
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Rectangle[] spriteSheetCoordinates;
        int currentSprite;
        Texture2D menuImage;
        Texture2D texture;
        float timer;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            spriteSheetCoordinates = new Rectangle[]
            {

                new Rectangle(16, 23, 23,42), //StandStill
                new Rectangle(445, 51, 22, 42), //Walk1
                new Rectangle(167, 150, 25, 42), //walk2
            };
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            texture = Content.Load<Texture2D>("Clone Trooper Better Sprite Sheet");
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
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(texture, new Vector2(), spriteSheetCoordinates[currentSprite], Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}