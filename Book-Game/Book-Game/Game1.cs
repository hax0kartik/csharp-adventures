using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
namespace Book_Game
{
    public class Snake : Game
    {
        enum Direction
        {
            Left = 0,
            Right,
            Up,
            Down,
            Continue
        }

        int foodpos=10;
        Direction faceDirection;
        Vector2 foodCoord; /* food position on grid */
        LinkedList<Vector2> snakeNodes;
        Texture2D vline, hline, food, snake;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Vector2 CalculateVector(int col, int row)
        {
            return new Vector2((col * 54.5f) + (54.5f - 40 + 10.0f) / 2, (row * 54.5f) + (54.5f - 40 + 10.0f) / 2);
        }
        public Snake()
        {
            snakeNodes = new LinkedList<Vector2>();
            snakeNodes.AddLast(CalculateVector(1, 3)); /* head */
            snakeNodes.AddLast(CalculateVector(0, 3)); /* tail */
            faceDirection = Direction.Right;
            int foodposRow = foodpos / 9;
            int foodposCol = foodpos - (foodposRow * 9);
            foodCoord = CalculateVector(foodposCol, foodposRow);
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            _graphics.PreferredBackBufferHeight = 500;
            _graphics.PreferredBackBufferWidth = 500;
            _graphics.ApplyChanges();

            /* Rect Drawing Code */
            vline = new Texture2D(GraphicsDevice, 10, 500);
            hline = new Texture2D(GraphicsDevice, 500, 10);
            food = new Texture2D(GraphicsDevice, 40, 40);
            snake = new Texture2D(GraphicsDevice, 40, 40);

            Color[] data = new Color[10 * 500];
            Color[] foodColor = new Color[40 * 40];
            Color[] SnakeNodeColor = new Color[40 * 40];

            for (int i = 0; i < data.Length; i++)
                data[i] = Color.Chocolate;

            for (int i = 0; i < foodColor.Length; i++)
                foodColor[i] = Color.Red;

            for (int i = 0; i < SnakeNodeColor.Length; i++)
                SnakeNodeColor[i] = Color.Green;

            vline.SetData(data);
            hline.SetData(data);
            food.SetData(foodColor);
            snake.SetData(SnakeNodeColor);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

        }

        int counter = 0;
        protected override void Update(GameTime gameTime)
        {
            counter++;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Down) && faceDirection!=Direction.Up)
                faceDirection = Direction.Down;

            else if (Keyboard.GetState().IsKeyDown(Keys.Up) && faceDirection != Direction.Down)
                faceDirection = Direction.Up;

            else if (Keyboard.GetState().IsKeyDown(Keys.Right) && faceDirection != Direction.Left)
                faceDirection = Direction.Right;

            else if (Keyboard.GetState().IsKeyDown(Keys.Left) && faceDirection != Direction.Right)
                faceDirection = Direction.Left;

            if (counter > 30)
            {
                /* For Continue in same */
                Vector2 head = snakeNodes.First.Value;
                Vector2 last = snakeNodes.Last.Value;
                switch (faceDirection)
                {
                    case Direction.Right:
                        snakeNodes.AddFirst(new Vector2(head.X + 54.5f, head.Y));
                        snakeNodes.RemoveLast();
                        break;

                    case Direction.Left:
                        snakeNodes.AddFirst(new Vector2(head.X - 54.5f, head.Y));
                        snakeNodes.RemoveLast();
                        break;

                    case Direction.Up:
                        snakeNodes.AddFirst(new Vector2(head.X, head.Y - 54.5f));
                        snakeNodes.RemoveLast();
                        break;

                    case Direction.Down:
                        snakeNodes.AddFirst(new Vector2(head.X, head.Y + 54.5f));
                        snakeNodes.RemoveLast();
                        break;

                }
                counter = 0;

                if (foodCoord.X == snakeNodes.First.Value.X && foodCoord.Y == snakeNodes.First.Value.Y)
                {
                    snakeNodes.AddLast(last);
                    Random rd = new Random();
                    foodpos =(int)rd.Next(0,81);
                    int foodposRow = foodpos / 9;
                    int foodposCol = foodpos - (foodposRow * 9);
                    foodCoord = CalculateVector(foodposCol, foodposRow);
                }
                head = snakeNodes.First.Value;

                if (head.X > 500 || head.X < 0)
                {
                    float x = head.X > 500 ? head.X - (54.5f * 9) : head.X + (54.5f * 9);
                    snakeNodes.RemoveFirst();
                    snakeNodes.AddFirst(new Vector2 (x,head.Y));
                }
                else if (head.Y > 500 || head.Y < 0)
                {
                    float y = head.Y > 500 ? head.Y - (54.5f * 9) : head.Y + (54.5f * 9);
                    snakeNodes.RemoveFirst();
                    snakeNodes.AddFirst(new Vector2(head.X, y));
                }

                var head2 = snakeNodes.First.Next;
                while (head2 != null)
                {
                    if(head2.Value.X == head.X && head2.Value.Y == head.Y)
                    {
                        Exit();
                    }
                    head2 = head2.Next;
                }

            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            /* Draw food */
            _spriteBatch.Draw(food, foodCoord, Color.Red);

            /* Draw Snake */
            foreach (var node in snakeNodes)
            {
                _spriteBatch.Draw(snake, node, Color.Green);
            }

            /* Draw Grid */
            for (int i = 0; i < 10; i++)
            {
                Vector2 coor = new Vector2(0 + (i * 54.5f), 0);
                Vector2 coor1 = new Vector2(0, 0 + (i * 54.5f));
                _spriteBatch.Draw(vline, coor, Color.White);
                _spriteBatch.Draw(hline, coor1, Color.White);
            }
            _spriteBatch.End();
            base.Draw(gameTime);

            /* --- */

        }
    }
}
