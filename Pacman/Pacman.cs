using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    enum Vector
    {
        Up = 1,
        Down = -1,
        None = 0
    }

    class PacmanClass
    {
        public delegate void CheckCoords();
        public event CheckCoords Coords50;

        public Rectangle Place { get { return place; }  set { place = value; } }

        private Rectangle place;

        private Bitmap pics;

        public Bitmap Pics { get { return pics; } }

        private int count;

        public PacmanClass()
        {
            count = 0;
            VectorX = Vector.None;
            VectorY = Vector.None;
            pics = Pacman.Properties.Resources.sprite_3;
        }

        private Vector nextVectorX;
        private Vector nextVectorY;

        private Vector vectorX;
        private Vector vectorY;

        public Vector VectorX
        {
            get { return vectorX; }
            set
            {
                nextVectorX = value;
            }
        }

        public Vector VectorY
        {
            get { return vectorY; }
            set
            {
                nextVectorY = value;
            }
        }

        public void NextSprite()
        {
            count++;
            switch (count)
            {
                case 0:
                    pics = Pacman.Properties.Resources.sprite_0;
                    break;
                case 1:
                    pics = Pacman.Properties.Resources.sprite_1;
                    break;
                case 2:
                    pics = Pacman.Properties.Resources.sprite_2;
                    break;
                case 3:
                    pics = Pacman.Properties.Resources.sprite_3;
                    count = 0;
                    break;
                default:
                    break;
            }
        }

        public void Move()
        {
            if ((this.place.X % 50 == 0) && (this.place.Y % 50 == 0))
            {
                vectorX = nextVectorX;
                vectorY = nextVectorY;
            }
            place.Y += 1 * (int)VectorY;
            place.X += 1 * (int)VectorX;
        }
        public void MoveB()
        {
            place.Y -= 50 * (int)nextVectorX;
            place.X -= 50 * (int)nextVectorY;
        }
    }
}
