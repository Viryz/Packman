using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test
{
    public partial class Form1 : Form
    {
        PacmanClass pacman = new PacmanClass();

        Graphics gr;
        Bitmap img;
        public Form1()
        {
            InitializeComponent();
            img = new Bitmap(pictureBoxPacman.Width, pictureBoxPacman.Height);
            pictureBoxPacman.Image = pacman.Pics;
            timer1.Interval = 100;
            timer1.Enabled = true;
            timer1.Start();
            timer2.Interval = 10;
            timer2.Start();
            pictureBoxPacman.BackColor = Color.Transparent;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pacman.NextSprite();

            using (gr = Graphics.FromImage(pictureBoxPacman.Image))
            {
                gr.Clear(Color.Transparent);
                gr.DrawImage(pacman.Pics, 0, 0);
            }
            pictureBoxPacman.Refresh();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            pictureBoxPacman.Location = new Point(pictureBoxPacman.Location.X + 1, pictureBoxPacman.Location.Y);
        }
    }
}
