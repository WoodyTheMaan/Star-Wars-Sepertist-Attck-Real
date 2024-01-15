using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Star_Wars_Sepertist_Attck
{
    public class Blast
    {
        Texture2D blastshot;
        Rectangle blastRect;
        Vector2 blastDirection;
        float despawnTimer; 

        public Blast(Texture2D texture, Rectangle location, Vector2 direction) 
        { 
            blastDirection = direction;
            blastRect = location;
            blastshot = texture;
            despawnTimer = 0;
        }
        public bool Update()
        {
            blastRect.Offset(blastDirection);
            despawnTimer += 1;
            return despawnTimer > 100;
        }
        public void Draw(SpriteBatch sprite)
        {
            sprite.Draw(blastshot, blastRect, Color.White);
        }
        public float DespawnTime { get { return despawnTimer; } }
        public Rectangle Location { get { return blastRect; } }    

    }
}
