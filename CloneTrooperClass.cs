using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static System.Net.Mime.MediaTypeNames;

namespace Star_Wars_Sepertist_Attck
{
    internal class CloneTrooperClass
    {
        Texture2D cloneTexture; // Current Clone Trooper texture to draw
        Rectangle[] coordinatesRectangle;
        int currentCoordinate;
        Rectangle cloneRect; // This rectangle will track where Clone Trooper is and his size
        int cloneSpeed;

        public CloneTrooperClass(Texture2D cloneTexture, Rectangle[] coordinatesRectangle, int currentCoordinate, Rectangle cloneRect, int cloneSpeed)
        {
            this.cloneTexture = cloneTexture;
            this.coordinatesRectangle = coordinatesRectangle;
            this.currentCoordinate = currentCoordinate;
            this.cloneRect = cloneRect;
            this.cloneSpeed = cloneSpeed;
        }

        public void Update(KeyboardState keyboardState, GraphicsDeviceManager graphics)
        {
            if (keyboardState.IsKeyDown(Keys.A))
            {
                cloneRect.X -= cloneSpeed;
                currentCoordinate = 0; //change 0 to whatever left pic is
            }
            if (keyboardState.IsKeyDown(Keys.D))
            {
                cloneRect.X += cloneSpeed;
                currentCoordinate = 1; //change 1 to whatever right pic is
            }

            if (keyboardState.IsKeyDown(Keys.W))
            {
                cloneRect.Y -= cloneSpeed;
                currentCoordinate = 2; //change 2 to whatever up pic is

            }

            if (keyboardState.IsKeyDown(Keys.S))
            {
                cloneRect.Y += cloneSpeed;
                currentCoordinate = 3; //change 3 to whatever down pic is

            }


            if (cloneRect.Right >= graphics.PreferredBackBufferWidth)
            {
                cloneRect.X = graphics.PreferredBackBufferWidth - cloneRect.Width;
            }
            else if ((cloneRect.Left <= 0))
            {
                cloneRect.X = 0;
            }
            if ((cloneRect.Top <= 0))
            {
                cloneRect.Y = 0;
            }
            else if (cloneRect.Bottom >= graphics.PreferredBackBufferHeight)
            {
                cloneRect.Y = graphics.PreferredBackBufferHeight - cloneRect.Height;
            }
        }
        public void Draw(SpriteBatch Sprite)
        {
            Sprite.Draw(cloneTexture, cloneRect, coordinatesRectangle[currentCoordinate], Color.White);
        }
    }
}
