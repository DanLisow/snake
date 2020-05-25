using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake_2._0
{
    public partial class Form1 : Form
    {

        Point[] snake;
        Point apple;
        SolidBrush brush;
        SolidBrush snakeBrush;
        SolidBrush appleBrush;
        SolidBrush headBrush;
        Random rand;
        string direction = "up";
        int length = 1;
        int width, height;
        int count = 0;
        

        public Form1()
        {
            rand = new Random();
            snake = new Point[10000];
            InitializeComponent();
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            width = pictureBox1.Width / 10;
            height = pictureBox1.Height / 10;
            snake[0].X = width / 2;
            snake[0].Y = height / 2;

            brush = new SolidBrush(Color.White);
            snakeBrush = new SolidBrush(Color.Blue);
            appleBrush = new SolidBrush(Color.Green);
            headBrush = new SolidBrush(Color.Crimson);

            apple.X = rand.Next(0, width - 1);
            apple.Y = rand.Next(0, height - 1);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Graphics graphic = Graphics.FromImage(pictureBox1.Image);
            graphic.FillRectangle(brush, 0, 0, pictureBox1.Width, pictureBox1.Height);

            if (length > 3)
            {
                for (int i = 1; i < length; i++)
                {
                    for (int j = i + 1; j < length; j++)
                    {
                        if(snake[i].X == snake[j].X && snake[i].Y == snake[j].Y)
                        {
                            timer1.Stop();
                            MessageBox.Show("Вы проиграли");
                            Application.Exit();
                        }
                    }
                }
            }

            for (int i = 0; i < length; i++)
            {

                if (snake[i].X < 0) snake[i].X += width;
                if (snake[i].X > width) snake[i].X -= width + 1;
                if (snake[i].Y < 0) snake[i].Y += height;
                if (snake[i].Y > height) snake[i].Y -= height + 1;

                graphic.FillEllipse(snakeBrush, snake[i].X * 10, snake[i].Y * 10, 10, 10);

                
                if(apple.X == snake[i].X && apple.Y == snake[i].Y)
                {
                    apple.X = rand.Next(0, width - 1);
                    apple.Y = rand.Next(0, height - 1);
                    length++;
                    timer1.Interval -= 1;
                    label1.Text = "Score: " + ++count;
                }

            }

            graphic.FillEllipse(appleBrush, apple.X * 10, apple.Y * 10, 10, 10);
            graphic.FillEllipse(headBrush, snake[0].X * 10, snake[0].Y * 10, 10, 10);

            createSnake();
            moveSnake();

            if (length > 10000 - 3)
            {
                length = 10000 - 3;
            }

            pictureBox1.Invalidate();
        }

        public void moveSnake()
        {
            if (direction == "up") snake[0].Y -= 1;
            if (direction == "down") snake[0].Y += 1;
            if (direction == "right") snake[0].X += 1;
            if (direction == "left") snake[0].X -= 1;
        }

        public void createSnake()
        {
            for (int i = length; i >= 0; i--)
            {
                snake[i + 1].X = snake[i].X;
                snake[i + 1].Y = snake[i].Y;
            }

            if (length < 3) length++;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Up)
            {
                direction = "up";
            }
            if (e.KeyCode == Keys.Down)
            {
                direction = "down";
            }
            if (e.KeyCode == Keys.Right)
            {
                direction = "right";
            }
            if (e.KeyCode == Keys.Left)
            {
                direction = "left";
            }
        }
    }
}
