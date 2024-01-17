using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace Star_Wars_Sepertist_Attck
{
    internal class CloneTrooperClass
    {
        Texture2D cloneTexture, blastTexture; // Current Clone Trooper texture to draw + blast
        Rectangle[] coordinatesRectangle;
        int currentCoordinate;
        SoundEffect blastShot;
        
        Rectangle cloneRect; // This rectangle will track where Clone Trooper is and his size
        int cloneSpeed;
        float coolDown;
        List<Blast> activeBlasts;
        Vector2 lastDirectionMoved;

        public CloneTrooperClass(Texture2D cloneTexture, Rectangle[] coordinatesRectangle, int currentCoordinate, Rectangle cloneRect, int cloneSpeed, Texture2D blastTexture, SoundEffect blastSound)
        {
            this.cloneTexture = cloneTexture;
            this.coordinatesRectangle = coordinatesRectangle;
            this.currentCoordinate = currentCoordinate;
            this.cloneRect = cloneRect;
            this.cloneSpeed = cloneSpeed;
            this.blastTexture = blastTexture;
            this.coolDown = 0;
            activeBlasts = new List<Blast>();
            lastDirectionMoved = new Vector2(0,-1);
            blastShot = blastSound;
        }

        public bool Update(KeyboardState keyboardState, GraphicsDeviceManager graphics, List<Droid>droids)
        {
            bool died = false;
            coolDown -= 0.05f;          
            for (int i = 0; i < activeBlasts.Count; i++)
            {
                if (activeBlasts[i].Update())
                {
                    activeBlasts.RemoveAt(i);
                    i--;
                }
            }

            Vector2 velocity = new Vector2(0, 0);
            if (keyboardState.IsKeyDown(Keys.A))
            {
                velocity.X = -1;
                currentCoordinate = 0; //change 0 to whatever left pic is
            }
            if (keyboardState.IsKeyDown(Keys.D))
            {
                velocity.X = 1;
                currentCoordinate = 1; //change 1 to whatever right pic is
                
            }

            if (keyboardState.IsKeyDown(Keys.W))
            {
                velocity.Y = -1;
                currentCoordinate = 2; //change 2 to whatever up pic is
                
            }

            if (keyboardState.IsKeyDown(Keys.S))
            {
                velocity.Y = 1;
                currentCoordinate = 3; //change 3 to whatever down pic is
            }
            if (velocity != new Vector2(0, 0))
            {
                velocity.Normalize();
                lastDirectionMoved = velocity;
                cloneRect.Offset(velocity * cloneSpeed);
            }

            //Shoot
            if (keyboardState.IsKeyDown(Keys.Space) && coolDown < 0)
            {
                Blast newBlast = new Blast(blastTexture, new Rectangle(cloneRect.X, cloneRect.Y, 10, 10), lastDirectionMoved*7);
                activeBlasts.Add(newBlast);
                blastShot.Play();
                coolDown = 1.5f;
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

            for (int i = 0; i < droids.Count; i++)
            {
                droids[i].Update(cloneRect);
                if (droids[i].Location.Intersects(cloneRect))
                {
                    died = true; break;
                }
                foreach (Blast b in activeBlasts)
                {
                    if (b.Location.Intersects(droids[i].Location))
                    {
                        droids.RemoveAt(i);
                        i--;
                        if (i < 0)
                            break;
                    }
                }
            }

            return died;
        }
        public void Draw(SpriteBatch Sprite)
        {
            foreach (var blast in activeBlasts)
            {
                blast.Draw(Sprite);
            }
            Sprite.Draw(cloneTexture, cloneRect, coordinatesRectangle[currentCoordinate], Color.White);

        }
    }
}
