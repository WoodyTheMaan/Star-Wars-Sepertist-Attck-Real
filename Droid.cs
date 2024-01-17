using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Star_Wars_Sepertist_Attck
{
    internal class Droid
    {
        Texture2D droidTexture, blastTextureDroid; // Current B1 Battledroid texture to draw + blast
        Rectangle[] coordinatesRectangle;
        int currentCoordinateDroid;

        Rectangle droidRect; // This rectangle will track where B1 Battledroid is and his size
        int droidSpeed;
        float coolDownDroid;
        List<Blast> activeBlasts;
        Vector2 lastDirectionMoved;

        public Droid(Texture2D droidTexture, Rectangle[] coordinatesRectangle, int currentCoordinate, Rectangle droidRect, int droidSpeed, Texture2D blastTexture)
        {
            this.droidTexture = droidTexture; 
            this.coordinatesRectangle = coordinatesRectangle;
            this.currentCoordinateDroid = currentCoordinate;
            this.droidRect = droidRect;
            this.droidSpeed = droidSpeed;
            this.blastTextureDroid = blastTexture;
            activeBlasts = new List<Blast>();
            lastDirectionMoved = new Vector2(0, -1);
        }
        public Rectangle Location { get { return droidRect; } }
        public void Update(Rectangle playerLoc)
        {
            for (int i = 0; i < activeBlasts.Count; i++)
            {
                if (activeBlasts[i].Update())
                {
                    activeBlasts.RemoveAt(i);
                    i--;
                }
            }
            if (droidRect.X < playerLoc.X)
            {
                droidRect.X += droidSpeed;
                
            }
            else
            {
                droidRect.X -= droidSpeed;
            }
            if (droidRect.Y < playerLoc.Y)
            {
                droidRect.Y += droidSpeed;

            }
            else
            {
                droidRect.Y -= droidSpeed;
            }
        }
        public void Draw(SpriteBatch Sprite)
        {
            foreach (var blast in activeBlasts)
            {
                blast.Draw(Sprite);
            }
            Sprite.Draw(droidTexture, droidRect, coordinatesRectangle[currentCoordinateDroid], Color.White);

        }

    }

}
