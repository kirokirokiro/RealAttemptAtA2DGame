using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace RealAttemptAtA2DGame
{
    class SimpleEnemy : Object
    {
        public int direction = 0; //0 = left, 1 = right
        public int timeFalling = 0;

        public SimpleEnemy(Texture2D tex, Vector2 position)
        {
            Sprite = new AnimatedSprite(tex, 1, 1);
            location = position;
            bounds = new Rectangle((int)location.X, (int)location.Y, Sprite.Width, Sprite.Height);
            direction = 0;
        }

        //these simple enemies move left to right until they hit a wall like in Mario
        public void Update()
        {
            velocity.X = 0;
            velocity.Y += 1 + timeFalling / 10;
            if (direction == 0)
            {
                velocity.X -= 2;
            }
            else
            {
                velocity.X += 2;
            }

            Rectangle newBound = new Rectangle((int)(location.X + velocity.X), (int)(location.Y + velocity.Y), Sprite.Width, Sprite.Height);

            //COLLISION WITH SCREEN BORDERS, ONLY USED FOR TESTING ENEMIES :)
            //replace with collisions with walls later

            //right border
            if (800 < (newBound.Right) && (bounds.Right - Sprite.Width * 0.1) < 800)
            {
                float distanceBetweenEnemyAndWall = 800 - bounds.Right;
                velocity.X = distanceBetweenEnemyAndWall;
                direction = 0;
            }


            //left border
            if (0 >= (newBound.Left) && (bounds.Left + Sprite.Width * 0.1) > 0)
            {
                float distanceBetweenEnemyAndWall = bounds.Left - 0;
                velocity.X = distanceBetweenEnemyAndWall;
                direction = 1;
            }

            //bottom border
            if (800 <= (newBound.Bottom) && (bounds.Bottom - Sprite.Height * 0.1) < 800)
            {
                float distanceBetweenEnemyAndWall = 800 - bounds.Bottom;
                velocity.Y = distanceBetweenEnemyAndWall;
                
            }


            location.X += velocity.X;
            location.Y += velocity.Y;
            bounds = new Rectangle((int)location.X, (int)location.Y, Sprite.Width, Sprite.Height);
        }
    }
}
