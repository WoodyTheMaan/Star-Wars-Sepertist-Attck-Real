using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Star_Wars_Sepertist_Attck
{
    internal class Droid
    {
        Texture2D droidTexture, blastTexture; // Current B1 Battledroid texture to draw + blast
        Rectangle[] coordinatesRectangle;
        int currentCoordinate;

        Rectangle droidRect; // This rectangle will track where B1 Battledroid is and his size
        int droidSpeed;
        float coolDown;
        List<Blast> activeBlasts;
        Vector2 lastDirectionMoved;

        public Droid(Texture2D droidTexture, Rectangle[] coordinatesRectangle, int currentCoordinate, Rectangle droidRect, int droidSpeed, Texture2D blastTexture)
        {
            this.droidTexture = droidTexture; 
            this.coordinatesRectangle = coordinatesRectangle;
            this.currentCoordinate = currentCoordinate;
            this.droidRect = droidRect;
            this.droidSpeed = droidSpeed;
            this.blastTexture = blastTexture;
            this.coolDown = 0;
            activeBlasts = new List<Blast>();
            lastDirectionMoved = new Vector2(0, -1);
        }
    }

}
