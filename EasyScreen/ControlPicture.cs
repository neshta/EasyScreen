using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace EasyScreen
{
    class ControlPicture
    {

        PictureBox pictureBox;
        int[] zeropos = new int[2];

        /* 1,2 - up, down
         * 3,4 - left, right
         */

        public ControlPicture(PictureBox pB)
        {
            pictureBox = pB;
        }
        
        public void PictureBoxInitialize()
        {
            pictureBox.Size = new Size(0, 0);
            pictureBox.Location = new Point(0, 0);
        }

        public void StartSelecting(MouseEventArgs e)
        {
            pictureBox.Size = new Size(0, 0);
            pictureBox.Location = new Point(e.Location.X, e.Location.Y);
            pictureBox.BackColor = System.Drawing.SystemColors.ActiveCaption;
            pictureBox.BorderStyle = BorderStyle.FixedSingle;
            zeropos[0] = pictureBox.Location.X;
            zeropos[1] = pictureBox.Location.Y;
        }

        public void PictureBoxUpdate(MouseEventArgs e)
        {
            int mode = 1;
            if (e.Location.X > zeropos[0])
            {
                if (e.Location.Y > zeropos[1]) mode = 1;
                else if (e.Location.Y < zeropos[1]) mode = -1;
            }
            else if (e.Location.X < zeropos[0])
            {
                if (e.Location.Y > zeropos[1]) mode = 2;
                else if (e.Location.Y < zeropos[1]) mode = -2;
            }

            switch (mode)
            {
                case 1:
                    {
                        pictureBox.Size = new Size(e.Location.X - zeropos[0], e.Location.Y - zeropos[1]);
                        pictureBox.Location = new Point(zeropos[0], zeropos[1]);
                        break;
                    }
                case -1:
                    {
                        pictureBox.Size = new Size(e.Location.X - zeropos[0], zeropos[1] - e.Location.Y);
                        pictureBox.Location = new Point(zeropos[0], e.Location.Y);
                        break;
                    }
                case 2:
                    {
                        pictureBox.Size = new Size(zeropos[0] - e.Location.X, e.Location.Y - zeropos[1]);
                        pictureBox.Location = new Point(e.Location.X, zeropos[1]);
                        break;
                    }
                case -2:
                    {
                        pictureBox.Size = new Size(zeropos[0] - e.Location.X, zeropos[1] - e.Location.Y);
                        pictureBox.Location = new Point(e.Location.X, e.Location.Y);
                        break;
                    }
            }
             //* 1 - right, down
             //* -1 - right, up
             //* 2 - left, down
             //* -2 - left, up
        }
    }
}
