using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    enum Vector
    {
        Up = 1,
        Down = -1,
        None = 0
    }

    class PacmanClass
    {
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
            pics = test.Properties.Resources.sprite_3;
        }

        public Vector VectorX { get; set; }
        public Vector VectorY { get; set; }

        public void NextSprite()
        {
            count++;
            switch (count)
            {
                case 0:
                    pics = test.Properties.Resources.sprite_0;
                    break;
                case 1:
                    pics = test.Properties.Resources.sprite_1;
                    break;
                case 2:
                    pics = test.Properties.Resources.sprite_2;
                    break;
                case 3:
                    pics = test.Properties.Resources.sprite_3;
                    count = 0;
                    break;
                default:
                    break;
            }
        }

        public void Move()
        {
            place.Y += 1 * (int)VectorY;
            place.X += 1 * (int)VectorX;
        }
        public void MoveB()
        {
            place.Y -= 1 * (int)VectorY;
            place.X -= 1 * (int)VectorX;
        }

    }
}
