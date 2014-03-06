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
    class Objectives : Object
    {
        public Objectives(Texture2D tex, Vector2 position)
        {
            location = position;
            Sprite = new AnimatedSprite(tex, 1, 1);
            bounds = new Rectangle((int)location.X, (int)location.Y, Sprite.Width, Sprite.Height);
        }
    }
}
