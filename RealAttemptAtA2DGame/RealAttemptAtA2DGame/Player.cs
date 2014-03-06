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
    class Player : Object
    {
       

        public bool walking;
        public bool jumping;
        public bool falling;
        public int timeFalling;
        public int jumpTime; //time spent in the air
        public float jumpStrength;
        public bool canJump = true; //enable to press A, false when you're in the air!
        public int maxJumpTime = 65;
        public bool stoppedJumping = false;
        public int hp = 2;
        public bool Invulnerable { get; set; }
        public int InvulnerableTimer { get; set; }
        
        public Player(Texture2D tex, Vector2 position)
        {
            Speed = 3;
            Sprite = new AnimatedSprite(tex, 2, 3);
            canJump = true;
            jumpTime = 0;
            jumping = false;
            Location = position;
            texture = tex;
            bounds = new Rectangle((int)location.X, (int)location.Y, Sprite.Width, Sprite.Height);
            Invulnerable = true;
        }

        public void Update(List<Wall> listWalls, List<SimpleEnemy> listEnemies){
            if (Invulnerable)
            {
                InvulnerableTimer++;

                if (InvulnerableTimer >= 120)
                {
                    InvulnerableTimer = 0;
                    Invulnerable = false;
                }
            }

            //Update sprite
            Sprite.Update(walking, Invulnerable);

            //GRAVITY
            //The velocity increases at the beginning of each frame except if you touch the ground
            //to simulate the air acceleration
            velocity.X = 0;
            velocity.Y += 1 + timeFalling / 10;
            
            
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            if (gamePadState.IsButtonDown(Buttons.X))
            {
                Speed = 5;
            }
            else
            {
                Speed = 3;
            }

            if (gamePadState.IsButtonDown(Buttons.A) && jumping == false)
            {
                
                jumping = true;
                velocity.Y -= 5;
            }
            if (gamePadState.IsButtonDown(Buttons.A) && jumping == true && velocity.Y < 0 && canJump)
            {
                jumpTime++;
                velocity.Y -= 1.5f;
                if (jumpTime > 7)
                {
                    canJump = false;
                }

            }

            //gamepad movement!
            if (gamePadState.ThumbSticks.Left.X != 0)
            {
                walking = true;
            }
            else
            {
                walking = false;
            }
            velocity.X += gamePadState.ThumbSticks.Left.X * Speed;
            Rectangle newBound = new Rectangle((int)(location.X + velocity.X), (int)(location.Y + velocity.Y), Sprite.Width, Sprite.Height);
           
            
            for (int i = 0; i < listWalls.Count; i++)
            {     
 
                if (newBound.Intersects(listWalls[i].Bounds))
                {
                    //right wall
                    if (listWalls[i].Location.X <= (newBound.Right) && (bounds.Right - 1) <= listWalls[i].Location.X
                        && (listWalls[i].Bounds.Top < Bounds.Bottom))
                    {
                        location.X = listWalls[i].Bounds.Left - Sprite.Width;
                        velocity.X = 0; 
                        
                    }



                    //left wall
                    if (listWalls[i].Bounds.Right >= (newBound.Left) && (bounds.Left + Sprite.Width * 0.1) > listWalls[i].Bounds.Right
                        && (listWalls[i].Bounds.Top < Bounds.Bottom))
                    {
                        location.X = listWalls[i].Bounds.Right;
                        velocity.X = 0;
                    }

                    //floor
                    if (listWalls[i].Location.Y <= (newBound.Bottom) && (bounds.Bottom - Sprite.Height * 0.1) < listWalls[i].Location.Y
                        && (listWalls[i].Bounds.Left < Bounds.Right) && (listWalls[i].Bounds.Right > Bounds.Left))
                    {
                        location.Y = listWalls[i].Location.Y - Sprite.Height;
                        velocity.Y = 0;
                        jumping = false;
                        timeFalling = 0;
                        canJump = true;
                        jumpTime = 0;
                        Console.WriteLine("bop" + listWalls[i].Location.X + " and " + (newBound.Right - Speed));
                    }

                    //ceiling
                    if (listWalls[i].Bounds.Bottom >= (newBound.Top) && (bounds.Top + Sprite.Height * 0.1) > listWalls[i].Bounds.Bottom
                        && (listWalls[i].Bounds.Left < Bounds.Right) && (listWalls[i].Bounds.Right > Bounds.Left))
                    {
                        velocity.Y =bounds.Top - listWalls[i].Bounds.Bottom;
                        falling = true;
                        jumpTime = maxJumpTime;
                    }


                }

            }

            if (!Invulnerable)
            {
                for (int i = 0; i < listEnemies.Count; i++)
                {
                    if (newBound.Intersects(listEnemies[i].Bounds))
                    {
                        //hitting enemy on your right
                        if (listEnemies[i].Bounds.Left < (newBound.Right) && (bounds.Right - 1) < listEnemies[i].Bounds.Left)
                        {
                            hp -= 1;
                            velocity.X -= 10;
                            Invulnerable = true;
                        }


                        //hitting enemy on your left
                        if (listEnemies[i].Bounds.Right >= (newBound.Left) && (bounds.Left + 1) > listEnemies[i].Bounds.Right)
                        {
                            hp -= 1;
                            velocity.X += 10;
                            Invulnerable = true;
                        }


                        if (listEnemies[i].Bounds.Top <= (newBound.Bottom) && (bounds.Bottom - 1) < listEnemies[i].Bounds.Top)
                        {
                            canJump = true;
                            listEnemies.RemoveAt(i);
                            jumping = true;
                            falling = true;
                            jumpStrength = 5;
                            jumpTime = 3; //makes the jump after crushing an ennemy "feel right"
                            velocity.Y = -5;
                            timeFalling = 0;
                            jumpTime = 0;
                        }


                    }
                }
            }

            //COLLISION WITH SCREEN BORDERS, ONLY USED FOR TESTING ENEMIES :)
            //right border
            if (800 < (newBound.Right) && (bounds.Right - Sprite.Width * 0.1) < 800)
            {
                float distanceBetweenPlayerAndWall = 800 - bounds.Right;
                velocity.X = distanceBetweenPlayerAndWall;
            }


            //left border
            if (0 >= (newBound.Left) && (bounds.Left + Sprite.Width * 0.1) > 0)
            {
                float distanceBetweenPlayerAndWall = bounds.Left - 0;
                velocity.X = distanceBetweenPlayerAndWall;
            }

            //bottom border
            if (800 <= (newBound.Bottom) && (bounds.Bottom - Sprite.Height * 0.1) < 800)
            {
                location.Y = 800 - Sprite.Height;
                velocity.Y = 0;
                canJump = true;

                jumping = false;
                timeFalling = 0;
                jumpTime = 0;
            }

            if (velocity.Y > 0)
            {
                timeFalling++;

            }
            else
            {
                timeFalling = 0;
            }

            if (velocity.Y > 8)
            {
                velocity.Y = 8;
            }

            
            location.X += velocity.X;
            location.Y += velocity.Y;
            //location = new Vector2(location.X + velocity.X, location.Y + velocity.Y);

            bounds = new Rectangle((int)location.X, (int)location.Y, Sprite.Width, Sprite.Height);
        }

       
        
    }
}
