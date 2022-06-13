using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using System.Diagnostics;
namespace tiktaktoe
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D rect;
        int c=0;
        int x, y;
        int[] a = { 0, 210, 420 };
        int[] board = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        Vector2 coor;
        Color[] data;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        public int tictaktoec(int[] a)
        {
            return 0;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            _graphics.PreferredBackBufferHeight = 620;
            _graphics.PreferredBackBufferWidth = 620;
            _graphics.ApplyChanges();


            rect = new Texture2D(_graphics.GraphicsDevice, 200, 200);
            data = new Color[200 * 200];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.White;
            rect.SetData(data);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            MouseState mouse = Mouse.GetState();
            x = mouse.Position.X;
            y = mouse.Position.Y;

            Debug.WriteLine(x+" "+y);


            if (mouse.LeftButton == ButtonState.Pressed)
            {
                if (c == 0)
                c = 1;
            }
            else
            {
                c = 0;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            
            for(int i=0; i<9; i++ )
            {
                int row =i/3 ;
                int col =i - (row*3);

                Debug.WriteLine("row "+row+" column "+col);
                coor.X = a[row];
                coor.Y = a[col];
                switch(board[i])
                {
                    case 0:
                        _spriteBatch.Draw(rect, coor, Color.Red);
                        break;
                    case 1:
                        _spriteBatch.Draw(rect, coor, Color.Blue);
                        break;
                    case 2:
                        _spriteBatch.Draw(rect, coor, Color.Green);
                        break;
                }
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
