using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace RealAttemptAtA2DGame
{
    public class Object
    {
        protected Texture2D texture;
        protected Vector2 velocity;
        protected Rectangle bounds;
        public AnimatedSprite Sprite { get; set;}
        //protected Vector2 Location { get; set;}
        protected Vector2 location;
        protected int Speed { get; set; }
        //protected float x;
        //protected float y;

        public Vector2 Location
        {
            get
            {
                return location;
            }
            set
            {
                location = value;
            }
        }
        /*public float X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }


        public float Y     
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }
        */

        public Rectangle Bounds
        {
            get
            {
                return bounds;
            }
            set
            {
                bounds = value;
            }
        }



        public void Draw(SpriteBatch spriteBatch)
        {
            this.Sprite.Draw(spriteBatch, location);
            //spriteBatch.Draw(texture, location, Color.White);


        }
    }


}
