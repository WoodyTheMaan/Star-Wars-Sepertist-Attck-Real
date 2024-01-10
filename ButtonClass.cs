using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Star_Wars_Sepertist_Attck
{
    internal class ButtonClass
    {
        Texture2D bgtex;
        Rectangle location;
        Color buttColor;
        string text;
        SoundEffect buttSound;
        SpriteFont font;
        bool didSoundPlay;

        public ButtonClass(Texture2D bgtex, Rectangle location, string buttText, SoundEffect buttSound, SpriteFont font)
        {
            this.bgtex = bgtex;
            this.location = location;
            this.buttColor = Color.DarkBlue;
            this.text = buttText;
            this.buttSound = buttSound;
            this.font = font;
            this.didSoundPlay = false;
        }

        public bool Update(MouseState mouse)
        {
            if (location.Contains(mouse.Position))
            {
                if (!didSoundPlay)
                    buttSound.Play();
                didSoundPlay = true;
                buttColor = Color.DarkBlue * 0.6f;
                return mouse.LeftButton == ButtonState.Pressed;
            }
            else
            {
                didSoundPlay = false;
                buttColor = Color.DarkBlue;
                return false;
            }
        }
        public void Draw(SpriteBatch Sprite)
        {
            Sprite.Draw(bgtex, location, buttColor);
            Sprite.DrawString(font, text, new Vector2(location.X + 5, location.Y+5), Color.White);
        }
    }
}
