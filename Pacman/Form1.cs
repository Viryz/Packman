using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pacman
{
    public partial class Form1 : Form
    {
        // розмір поля 10х10 БЛОКІВ
        private const int numberW = 10;
        private const int numberH = 10;

        // розміри блоку
        private const int blockWidth = 50;
        private const int blockHength = 50;

        PacmanClass pacman = new PacmanClass();

        Rectangle place;

        Graphics g;

        List<Rectangle> listRectangle = new List<Rectangle>();
        List<Rectangle> coinList = new List<Rectangle>();

        Bitmap coin = Pacman.Properties.Resources.Coin;

        public Form1()
        {
            InitializeComponent();

            DoubleBuffered = true;

            //this.CreateNewRectangle(0, 100, 1, 2);
            //this.CreateNewRectangle(150, 100, 3, 1);
            //this.CreateNewRectangle(250, 150, 1, 3);
            //this.CreateNewRectangle(100, 300, 1, 4);
            //this.CreateNewRectangle(450, 100, 1, 2);
            //this.CreateNewRectangle(300, 250, 2, 1);

            CreateCoins();

            place = new Rectangle(0, menuStrip1.Height, numberW * blockWidth, numberH * blockHength);
            this.ClientSize = new System.Drawing.Size(numberW * blockWidth + 1, numberH * blockHength + 1 + menuStrip1.Height);

            this.g = this.CreateGraphics();

            pacman.Place = new Rectangle(50, 50, blockWidth - 2, blockHength - 2);

            timer1.Interval = 10;
            timer1.Enabled = true;
            timer1.Start();
            timer2.Interval = 500;
            timer2.Enabled = true;
            timer2.Start();

            pictureBoxPacman.Location = pacman.Place.Location;
            pictureBoxPacman.Image = pacman.Pics;

        }

        private void CreateNewRectangle(int x, int y, int widthInBlock = 1, int heightInBlock = 1)
        {
            int coordX = x, coordY = y;
            for (int i = 0; i < widthInBlock; i++) // в ширину
            {
                for (int j = 0; j < heightInBlock; j++, coordY += 50) // в висоту
                {
                    listRectangle.Add(new Rectangle(coordX, coordY, blockWidth, blockHength));
                }
                coordY = y;
                coordX += 50;
            }
        }

        private void CreateCoins()
        {
            for (int i = 0; i < numberH * blockHength; i += 50)
            {
                for (int j = 0; j < numberW * blockWidth; j += 50)
                {
                    if(!(listRectangle.Exists((rect) => ((rect.X == i) && (rect.Y == j)))))
                    {
                        coinList.Add(new Rectangle(i, j, blockHength, blockWidth));
                    }
                }
            }
        }

        private void DrawCoins()
        {
            for (int i = 0; i < coinList.Count; i++)
            {
                if (coinList[i] != null)
                {
                    //g.DrawRectangle(Pens.Yellow, coinList[i]);
                    //g.FillRectangle(Brushes.Yellow, coinList[i]);
                    g.DrawImage(coin, coinList[i]);
                }
                
            }
        }

        private bool Check()
        {
            foreach (Rectangle item in listRectangle)
            {
                if (pacman.Place.Bottom == item.Top && ((pacman.Place.Right > item.Left) && (pacman.Place.Left < item.Right)))
                    return false;

                if (pacman.Place.Top == item.Bottom && ((pacman.Place.Right > item.Left) && (pacman.Place.Left < item.Right)))
                    return false;

                if (pacman.Place.Left == item.Right && ((pacman.Place.Bottom > item.Top) && (pacman.Place.Top < item.Bottom)))
                    return false;

                if (pacman.Place.Right == item.Left && ((pacman.Place.Bottom > item.Top) && (pacman.Place.Top < item.Bottom)))
                    return false;
            }
            return true;
        }

        private int Check(List<Rectangle> list)
        {
            foreach (Rectangle item in list)
            {
                if (pacman.Place.Bottom == item.Top && ((pacman.Place.Right > item.Left) && (pacman.Place.Left < item.Right)))
                    return list.FindIndex((rect) => rect == item);

                if (pacman.Place.Top == item.Bottom && ((pacman.Place.Right > item.Left) && (pacman.Place.Left < item.Right)))
                    return list.FindIndex((rect) => rect == item);

                if (pacman.Place.Left == item.Right && ((pacman.Place.Bottom > item.Top) && (pacman.Place.Top < item.Bottom)))
                    return list.FindIndex((rect) => rect == item);

                if (pacman.Place.Right == item.Left && ((pacman.Place.Bottom > item.Top) && (pacman.Place.Top < item.Bottom)))
                    return list.FindIndex((rect) => rect == item);
            }
            return -1;
        }

        private void DrawPlace()
        {
            //g.DrawRectangle(Pens.Black, place);
            //g.FillRectangle(Brushes.Black, place);

            foreach (Rectangle item in listRectangle)
            {
                //g.DrawRectangle(Pens.Black, item);
                g.FillRectangle(Brushes.Black, item);
            }
        }

        private void DrawPacman()
        {
            //g.DrawImage(pacman.Pics, pacman.Place.X, pacman.Place.Y);
            using (Graphics gr = Graphics.FromImage(pictureBoxPacman.Image))
            {
                gr.Clear(Color.Transparent);
                gr.DrawImage(pacman.Pics, 0, 0);
            }
            pictureBoxPacman.Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //g.Clear(Color.Transparent);
            DrawCoins();
            DrawPacman();
            DrawPlace();
            menuStrip1.Items[0].Text = pacman.Place.X.ToString() + "/" + pacman.Place.Y.ToString();
            int check = Check(coinList);
            if (check != -1)
            {
                g.FillRectangle(Brushes.Transparent, coinList[check]);
                Invalidate(coinList[check]);
                coinList.RemoveAt(check);
                if (coinList.Count == 0)
                    MessageBox.Show("You win");
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.ToUpper(e.KeyChar) == (char)Keys.D)
            {
                pacman.VectorX = Vector.Up;
                pacman.VectorY = Vector.None;
            }
            if (Char.ToUpper(e.KeyChar) == (char)Keys.S)
            {
                pacman.VectorX = Vector.None;
                pacman.VectorY = Vector.Up;
            }
            if (Char.ToUpper(e.KeyChar) == (char)Keys.A)
            {
                pacman.VectorX = Vector.Down;
                pacman.VectorY = Vector.None;
            }
            if (Char.ToUpper(e.KeyChar) == (char)Keys.W)
            {
                pacman.VectorX = Vector.None;
                pacman.VectorY = Vector.Down;
            }
            if (!timer1.Enabled)
                timer1.Enabled = true;
            //pacman.Move();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if ((pacman.Place.Right > place.Width) || (pacman.Place.Left < 0) || (pacman.Place.Bottom > place.Bottom) || (pacman.Place.Top < menuStrip1.Bottom) || !Check())
            {
                pacman.MoveB();
                timer1.Enabled = false;
            }
            else
                pacman.Move();
            Invalidate(pacman.Place);
            pictureBoxPacman.Location = pacman.Place.Location;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            pacman.NextSprite();
            //DrawPacman();
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pacman.VectorX = Vector.None;
            pacman.VectorY = Vector.None;
            pacman.Place = new Rectangle(30, 30, blockWidth, blockHength);
            
            DrawPacman();
            Invalidate(pacman.Place);
        }
    }
}
