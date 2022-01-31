using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace SnakeGame
{
    public partial class Form1 : Form
    {

        private List<Circle> Snake = new List<Circle>();    //This creates a list array for the Snake.
        private Circle food = new Circle();                 //This creates a Circle class called Food.

        int maxWidth;                                       //This will be the maximum Width for the window of the Game.
        int maxHeight;                                      //This will be the maximum Height for the window of the Game.

        int score;                                          //This will keep track of the player's score.
        int highScore;                                      //This will keep track of the Highest Score a player gets.

        Random rand = new Random();                         //This creates a new object within the class called rand.

        bool goLeft, goRight, goDown, goUp;                 //Boolean variables to keep track of directions.


        public Form1()
        {
            InitializeComponent();

            new Settings();                                 //This is used to set the default for the game.
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            //KeyIsDown will trigger a change in direction of the Snake.
            if (e.KeyCode == Keys.Left && Settings.directions != "right")
            {
                goLeft = true;
            }
            if (e.KeyCode == Keys.Right && Settings.directions != "left")
            {
                goRight = true;
            }
            if (e.KeyCode == Keys.Up && Settings.directions != "down")
            {
                goUp = true;
            }
            if (e.KeyCode == Keys.Down && Settings.directions != "up")
            {
                goDown = true;
            }



        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            //KeyIsUp also triggers a change in direction of the snake that will end up being used in the KeyIsDown Function.
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            if (e.KeyCode == Keys.Up)
            {
                goUp = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                goDown = false;
            }
        }

        private void StartGame(object sender, EventArgs e)
        {
            //When the player clicks the Start button, we restart the game brand new.
            RestartGame();
        }

        

        private void GameTimerEvent(object sender, EventArgs e)
        {
            // setting the directions

            if (goLeft)
            {
                Settings.directions = "left";
            }
            if (goRight)
            {
                Settings.directions = "right";
            }
            if (goDown)
            {
                Settings.directions = "down";
            }
            if (goUp)
            {
                Settings.directions = "up";
            }
            // end of directions

            //This will be the main loop for the Snake's head and parts.

            for (int i = Snake.Count - 1; i >= 0; i--)
            {
                //We check to see if the Snake's Head is active.
                if (i == 0)
                {
                    //If so, we are going to move the rest of the body according to which way the head is moving.
                    switch (Settings.directions)
                    {
                        case "left":
                            Snake[i].X--;
                            break;
                        case "right":
                            Snake[i].X++;
                            break;
                        case "down":
                            Snake[i].Y++;
                            break;
                        case "up":
                            Snake[i].Y--;
                            break;
                    }
                    //All of the following If statements keep the snake within the boundaries of the Window
                    if (Snake[i].X < 0)
                    {
                        Snake[i].X = maxWidth;
                    }
                    if (Snake[i].X > maxWidth)
                    {
                        Snake[i].X = 0;
                    }
                    if (Snake[i].Y < 0)
                    {
                        Snake[i].Y = maxHeight;
                    }
                    if (Snake[i].Y > maxHeight)
                    {
                        Snake[i].Y = 0;
                    }

                    //This will detect collision between the Snake's head and Food
                    if (Snake[i].X == food.X && Snake[i].Y == food.Y)
                    {
                        EatFood();
                    }
                    //This will check if the Snake Collides with itself, i.e. Eats itself.
                    for (int j = 1; j < Snake.Count; j++)
                    {

                        if (Snake[i].X == Snake[j].X && Snake[i].Y == Snake[j].Y)
                        {
                            //If the Snake did in fact collide with itself and eat itself, the Game is now over.
                            GameOver();
                        }

                    }


                }
                else
                {
                    //There have been no collisions, and the game continues on.
                    Snake[i].X = Snake[i - 1].X;
                    Snake[i].Y = Snake[i - 1].Y;
                }
            }


            picCanvas.Invalidate();

        }

        private void UpdatePictureBoxGraphics(object sender, PaintEventArgs e)
        {
            //This is where we will see the snake and its parts moving.
            Graphics canvas = e.Graphics;       //Creating a new graphics class called canvas.

            Brush snakeColour;

            for (int i = 0; i < Snake.Count; i++)
            {
                if (i == 0)
                {
                    //This colors the head of the snake.
                    snakeColour = Brushes.Black;
                }
                else
                {
                    //This colors the body of the snake.
                    snakeColour = Brushes.DarkGreen;
                }
                //This will draw the Snake's body and head.
                canvas.FillEllipse(snakeColour, new Rectangle(Snake[i].X * Settings.Width, Snake[i].Y * Settings.Height,Settings.Width, Settings.Height));
            }

            //This will draw the food.
            canvas.FillEllipse(Brushes.DarkRed, new Rectangle(food.X * Settings.Width, food.Y * Settings.Height, Settings.Width, Settings.Height));
        }

        private void RestartGame()
        {
            maxWidth = picCanvas.Width / Settings.Width - 1;
            maxHeight = picCanvas.Height / Settings.Height - 1;

            Snake.Clear();

            startButton.Enabled = false;
            snapButton.Enabled = false;
            score = 0;      //I set the initial value of score to 0.
            txtScore.Text = "Score: " + score;

            Circle head = new Circle { X = 10, Y = 5 };
            Snake.Add(head); // adding the head part of the snake to the list

            for (int i = 0; i < 10; i++)
            {
                Circle body = new Circle();
                Snake.Add(body);
            }
            //Creating the first food object.
            food = new Circle { X = rand.Next(2, maxWidth), Y = rand.Next(2, maxHeight) };

            gameTimer.Start();

        }

        private void EatFood()
        {
            //Every time the Snake eats food, we will add 1 to the score.
            score += 1;

            txtScore.Text = "Score: " + score;
            
            //This will add a part to the Snake's Body.
            Circle body = new Circle
            {
                X = Snake[Snake.Count - 1].X,
                Y = Snake[Snake.Count - 1].Y
            };

            //This will add the part to the Snake Array.
            Snake.Add(body);

            //This will create a new Food with a random X and Y.
            food = new Circle { X = rand.Next(2, maxWidth), Y = rand.Next(2, maxHeight) };


        }

        private void GameOver()
        {
            //When the game has ended, the following will run.
            gameTimer.Stop();
            startButton.Enabled = true;
            snapButton.Enabled = true;

            //If the score the player just got beat a previous high score, I will display that to them.
            if (score > highScore)
            {
                highScore = score;

                txtHighScore.Text = "High Score: " + Environment.NewLine + highScore;
                txtHighScore.ForeColor = Color.Maroon;
                txtHighScore.TextAlign = ContentAlignment.MiddleCenter;
            }
        }


    }
}

