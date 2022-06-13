using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using System.Diagnostics;
namespace tictactoe
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D rect;
        bool c = false;
        int x, y;
        int[] a = { 0, 210, 420 };
        int[] board = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        Vector2[] boardcoords;
        Vector2 coor;
        Color[] data;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
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

            /* Generate Board Coords */
            boardcoords = new Vector2[9];
            for(int i = 0; i < 9; i++){
                int row = i / 3;
                boardcoords[i].X = a[row];
                boardcoords[i].Y = a[i - (row * 3)];
            }
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        int counter = 0;
        bool CheckWin(int option)
        {
            for(int i = 0; i < 3; i++)
            {
                /* Check rows */
                if (board[i] == option && board[i + 3] == option && board[i + 6] == option)
                    return true;

                /* Check columns */
                if (board[(i * 3)] == option && board[(i * 3) + 1] == option && board[(i * 3) + 2] == option)
                    return true;
            }
            /* Check diagnols */
            if ((board[0] == option && board[4] == option && board[8] == option) ||
                (board[2] == option && board[4] == option && board[6] == option))
                return true;
            return false;
        }

        bool CheckTie()
        {
            int i = 0;
            while(i < 9)
            {
                if (board[i] == 0)
                    return false;
                i++;
            }
            return true;
        }
        protected override void Update(GameTime gameTime)
        {
            counter++;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            MouseState mouse = Mouse.GetState();
            x = mouse.Position.X;
            y = mouse.Position.Y;

            if (mouse.LeftButton == ButtonState.Pressed && counter > 30)
            {
                for(int i = 0; i < 9; i++)
                {
                    if (x > boardcoords[i].X && x < boardcoords[i].X + 200.0f
                        && y > boardcoords[i].Y && y < boardcoords[i].Y + 200.0f
                        && board[i] == 0)
                    {
                        int player =  c ? 2 : 1;
                        board[i] = player;
                        c = !c;
                        if (CheckWin(player))
                        {
                            Debug.WriteLine("Player " + player + " Won!");
                            Exit();
                        }
                        else if (CheckTie())
                        {
                            Debug.WriteLine("Its a Tie!");
                            Exit();
                        }
                    }
                }
                counter = 0;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            
            for(int i = 0; i < 9; i++)
            {
                switch(board[i])
                {
                    case 0:
                        _spriteBatch.Draw(rect, boardcoords[i], Color.Gray);
                        break;
                    case 1:
                        _spriteBatch.Draw(rect, boardcoords[i], Color.Blue);
                        break;
                    case 2:
                        _spriteBatch.Draw(rect, boardcoords[i], Color.Green);
                        break;
                }
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
