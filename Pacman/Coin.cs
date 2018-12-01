using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    class Coin
    {
        private Rectangle place;
        private Bitmap pics = Pacman.Properties.Resources.Coin;

        public Rectangle Place { get { return place; } }

        public Coin(int x, int y, int w, int h)
        {
            place = new Rectangle(x, y, w, h);
        }

    }
}
