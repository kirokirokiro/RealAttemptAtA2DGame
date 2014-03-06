using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace RealAttemptAtA2DGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private Texture2D map1;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Player player;
        private SpriteFont font;

        //FPS stats
        private TimeSpan fpsTimer = TimeSpan.Zero;
        private int framePerSecondCount = 0;
        private int framePerSecondReal = 0;

        private List<Objectives> listObjectives = new List<Objectives>();
        private List<Wall> listWalls = new List<Wall>();
        private List<SimpleEnemy> listEnemies = new List<SimpleEnemy>();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferredBackBufferWidth = 800;
            graphics.ApplyChanges();

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        

        //LOAD CONTENT
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            Texture2D textureEnemy = Content.Load<Texture2D>("simpleenemy");
            Texture2D textureObjective = Content.Load<Texture2D>("obj");
            Texture2D textureWall = Content.Load<Texture2D>("wall");
            Texture2D textureCharacter = Content.Load<Texture2D>("characteratlas");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            player = new Player(textureCharacter, new Vector2(1, 1));
            map1 = Content.Load<Texture2D>("map1");
            ReadMap(map1);
            



            //player = new Player(textureCharacter, new Vector2(1, 700));
            /* Random random = new Random();
             int randomX;
             int randomY;
            
              for(int i = 0 ; i < 10 ; i++)
              {
                  randomX =  random.Next(150, 800);
                  randomY =  random.Next(150, 800);
                  listEnemies.Add(new SimpleEnemy(textureEnemy, new Vector2(randomX, randomY)));
              }*/
             

             font = Content.Load<SpriteFont>("SpriteFont1");

            /*

             for(int i = 0 ; i < 100 ; i++)
             {
                 randomX =  random.Next(1, 800);
                 randomY =  random.Next(1, 800);
                 listObjectives.Add(new Objectives(textureObjective, new Vector2(randomX, randomY)));
             }

             for (int i = 0; i < 100; i++)
             {
                 randomX = random.Next(1, 800);
                 randomY = random.Next(1, 800);
                 listWalls.Add(new Wall(textureWall, new Vector2(randomX, randomY)));
             }*/
        }



        //UNLOAD CONTENT
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        

        //UPDATE
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            player.Update(listWalls, listEnemies);
            
            
            /*framePerSecondCount += 1;
            fpsTimer += gameTime.ElapsedGameTime;
            if (fpsTimer >= TimeSpan.FromSeconds(1)){
                framePerSecondReal = framePerSecondCount;
                fpsTimer = TimeSpan.Zero;
                framePerSecondCount = 0;
            }

            for (int i = 0; i < listObjectives.Count; i++)
            {
                if (player.Bounds.Intersects(listObjectives[i].Bounds))
                {
                    listObjectives.RemoveAt(i);
                }

            }

            foreach (SimpleEnemy enemy in listEnemies)
            {
                enemy.Update();
            }*/

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            player.Draw(spriteBatch);
            spriteBatch.DrawString(font, "FPS: "+framePerSecondReal, new Vector2(200, 200), Color.Black);
            //spriteBatch.Draw(pixel, player.Location, Color.White);

            foreach (Objectives objective in listObjectives)
            {
                objective.Draw(spriteBatch);
            }

            foreach (SimpleEnemy enemy in listEnemies)
            {
                enemy.Draw(spriteBatch);
            }

            foreach (Wall wall in listWalls)
            {
                wall.Draw(spriteBatch);
  
            }
            


            spriteBatch.End();
            base.Draw(gameTime);
        }
        
        public Color ReadPixel(int x, int y, Texture2D map)
        {
            int size = map.Width * map.Height;
            Color[] retrievedColor = new Color[1];
            map.GetData<Color>(0, new Rectangle(x, y, 1, 1), retrievedColor, 0, 1);

            return retrievedColor[0];
        }

        public void ReadMap(Texture2D map)
        {
            Color value;
            for (int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    value = ReadPixel(x, y, map);
                    if (value == new Color(255, 0, 255))
                    {
                        
                        Texture2D textureCharacter = Content.Load<Texture2D>("characteratlas");
                        player = new Player(textureCharacter, new Vector2(x * 20, y * 20));
                    }
                    if (value == new Color(0, 0, 0))
                    {
                        
                        Texture2D textureWall = Content.Load<Texture2D>("wall");
                        listWalls.Add(new Wall(textureWall, new Vector2(x * 20, y * 20)));
                    }
                }
            }

        }
    }
}
