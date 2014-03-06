using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace RealAttemptAtA2DGame
{
    public class AnimatedSprite
    {
        public Texture2D Texture { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        private int currentFrame;
        private int totalFrames;
        public int Width { get; set; }
        public int Height { get; set; }
        public int WalkingTimer { get; set; }
        public bool Invulnerable { get; set; }
        public bool Flash { get; set; }
        public int invulnerableAnimationTimer;
        public AnimatedSprite(Texture2D texture, int rows, int columns)
        {
            Texture = texture;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
            Width = Texture.Width / Columns;
            Height = Texture.Height / Rows;
            WalkingTimer = 0;
            Invulnerable = false;
            invulnerableAnimationTimer = 0;
        }

        public void Update(bool walking, bool invulnerable)
        {
            Invulnerable = invulnerable;

            if (Invulnerable)
            {
                invulnerableAnimationTimer++;
                if (invulnerableAnimationTimer >= 10)
                {
                    SwitchInvulnerableFlash();
                    invulnerableAnimationTimer = 0;
                }
            }

            if (walking)
            {
                WalkingTimer++;
                if (WalkingTimer >= 10)
                {
                    currentFrame++;
                    WalkingTimer = 0;
                }
            }


            if (currentFrame >= totalFrames)
                currentFrame = 0;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {

            int row = 0;
            int column = currentFrame % Columns;

            if (Invulnerable && Flash)
            {
                row = 1;
                column = currentFrame % Columns;
            }
            Rectangle sourceRectangle = new Rectangle(Width * column, Height * row, Width, Height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, Width, Height);


            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);

        }

        public void SwitchInvulnerableFlash()
        {
            if (Flash)
            {
                Flash = false;
            }
            else
            {
                Flash = true;
            }
        }
    }
}
