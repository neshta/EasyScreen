using System;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using Microsoft.Win32;

namespace EasyScreen
{
    class Controller
    {
        Form5 f5 = new Form5();
        static Form3 f3 = new Form3();
        Form4 f4 = new Form4();
        PictureBox pictureBox1, pictureBox2;
        Panel panel1, panel2, panel3, panel4, panel5, panel6, panel7, panel8;
        public byte worktype = 228;
        const byte pnSize = 10;
        const int otSize = 100;
        const int wSize = 150;
        int[] pSize = new int[2];
        int[] zeropos = new int[2];
        byte type = 0;
        //Bitmap HintImage;

        public Controller(PictureBox pB1, PictureBox pB2, Panel pn1, Panel pn2, Panel pn3, Panel pn4, Panel pn5, Panel pn6, Panel pn7, Panel pn8)
        {
            // constructor
            pictureBox1 = pB1;
            pictureBox2 = pB2;
            panel1 = pn1;
            panel2 = pn2;
            panel3 = pn3;
            panel4 = pn4;
            panel5 = pn5;
            panel6 = pn6;
            panel7 = pn7;
            panel8 = pn8;
        }

        public void Initialize()
        {
            DisablePanels();

            panel1.Size = new Size(pnSize, pnSize);
            panel2.Size = new Size(pnSize, pnSize);
            panel3.Size = new Size(pnSize, pnSize);
            panel4.Size = new Size(pnSize, pnSize);
            panel5.Size = new Size(pnSize, pnSize);
            panel6.Size = new Size(pnSize, pnSize);
            panel7.Size = new Size(pnSize, pnSize);
            panel8.Size = new Size(pnSize, pnSize);

            panel1.BackColor = Color.White;
            panel2.BackColor = Color.White;
            panel3.BackColor = Color.White;
            panel4.BackColor = Color.White;
            panel5.BackColor = Color.White;
            panel6.BackColor = Color.White;
            panel7.BackColor = Color.White;
            panel8.BackColor = Color.White;

            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel4.BorderStyle = BorderStyle.FixedSingle;
            panel5.BorderStyle = BorderStyle.FixedSingle;
            panel6.BorderStyle = BorderStyle.FixedSingle;
            panel7.BorderStyle = BorderStyle.FixedSingle;
            panel8.BorderStyle = BorderStyle.FixedSingle;

            pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
            pictureBox1.Size = new Size(0, 0);
            pictureBox1.Location = new Point(0, 0);
        }

        public void StartSelecting(MouseEventArgs e)
        {
            DisablePanels();
            pictureBox1.Size = new Size(0, 0);
            pictureBox1.Location = new Point(e.Location.X, e.Location.Y);
            pictureBox1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            zeropos[0] = pictureBox1.Location.X;
            zeropos[1] = pictureBox1.Location.Y;
            worktype = 1;
        }

        public void StopSelecting(object sender, MouseEventArgs e)
        {
            EnablePanels();
            worktype = 0;
            UpdateMovingPanels(sender, e);
            pictureBox1.Cursor = Cursors.SizeAll;
        }

        public void UpdateMovingPanels(object sender, EventArgs e)
        {
            panel1.Location = new Point(pictureBox1.Location.X - pnSize / 2, pictureBox1.Location.Y - pnSize / 2);
            panel2.Location = new Point(pictureBox1.Location.X + pictureBox1.Size.Width / 2 - pnSize / 2, pictureBox1.Location.Y - pnSize / 2);
            panel3.Location = new Point(pictureBox1.Location.X + pictureBox1.Size.Width - pnSize / 2, pictureBox1.Location.Y - pnSize / 2);
            panel4.Location = new Point(pictureBox1.Location.X - pnSize / 2, pictureBox1.Location.Y + pictureBox1.Size.Height / 2 - pnSize / 2);
            panel5.Location = new Point(pictureBox1.Location.X + pictureBox1.Size.Width - pnSize / 2, pictureBox1.Location.Y + pictureBox1.Size.Height / 2 - pnSize / 2);
            panel6.Location = new Point(pictureBox1.Location.X - pnSize / 2, pictureBox1.Location.Y + pictureBox1.Size.Height - pnSize / 2);
            panel7.Location = new Point(pictureBox1.Location.X + pictureBox1.Size.Width / 2 - pnSize / 2, pictureBox1.Location.Y + pictureBox1.Size.Height - pnSize / 2);
            panel8.Location = new Point(pictureBox1.Location.X + pictureBox1.Size.Width - pnSize / 2, pictureBox1.Location.Y + pictureBox1.Size.Height - pnSize / 2);
        }

        public void PictureBoxUpdate(MouseEventArgs e, Label label)
        {
            byte mode = 1;
            if (e.Location.X > zeropos[0])
            {
                if (e.Location.Y > zeropos[1]) mode = 1;
                else if (e.Location.Y < zeropos[1]) mode = 2;
            }
            else if (e.Location.X < zeropos[0])
            {
                if (e.Location.Y > zeropos[1]) mode = 3;
                else if (e.Location.Y < zeropos[1]) mode = 4;
            }

            switch (mode)
            {
                case 1:
                    {
                        pictureBox1.Size = new Size(e.Location.X - zeropos[0], e.Location.Y - zeropos[1]);
                        pictureBox1.Location = new Point(zeropos[0], zeropos[1]);
                        if (pictureBox1.Size.Width >= label.Size.Width + 2 && pictureBox1.Size.Height >= label.Size.Height + 2)
                        {
                            label.Location = new Point(pictureBox1.Location.X + pictureBox1.Size.Width - label.Size.Width - 1, pictureBox1.Location.Y + pictureBox1.Size.Height - label.Size.Height - 1);
                            type = 1;
                        }
                        else if (pictureBox1.Size.Width < label.Size.Width + 2 && pictureBox1.Size.Height >= label.Size.Height + 2)
                        {
                            
                            if (Screen.PrimaryScreen.Bounds.Width - pictureBox1.Size.Width + pictureBox1.Location.X >= label.Size.Width + 1)
                            {
                                label.Location = new Point(pictureBox1.Location.X + pictureBox1.Size.Width, pictureBox1.Location.Y + pictureBox1.Size.Height - label.Size.Height - 1);
                                type = 2;
                            }
                            else if (Screen.PrimaryScreen.Bounds.Width - pictureBox1.Size.Width - pictureBox1.Location.X < label.Size.Width + 1)
                            {
                                label.Location = new Point(Screen.PrimaryScreen.Bounds.Width - label.Size.Width, pictureBox1.Location.Y + pictureBox1.Size.Height);
                                type = 3;
                            }
                        }
                        else if (pictureBox1.Size.Width >= label.Size.Width + 2 && pictureBox1.Size.Height < label.Size.Height + 2)
                        {
                            label.Location = new Point(pictureBox1.Location.X + pictureBox1.Size.Width - label.Size.Width - 1, pictureBox1.Location.Y + pictureBox1.Size.Height);
                            type = 4;
                        }
                        if (pictureBox1.Size.Width < label.Size.Width + 2 && pictureBox1.Size.Height < label.Size.Height + 2)
                        {
                            label.Location = new Point(zeropos[0], zeropos[1] - label.Size.Height);
                            type = 5;
                        }
                        break;
                    }
                case 2:
                    {
                        pictureBox1.Size = new Size(e.Location.X - zeropos[0], zeropos[1] - e.Location.Y);
                        pictureBox1.Location = new Point(zeropos[0], e.Location.Y);
                        if (pictureBox1.Size.Width >= label.Size.Width + 2 && pictureBox1.Size.Height >= label.Size.Height + 2)
                        {
                            label.Location = new Point(pictureBox1.Location.X + pictureBox1.Size.Width - label.Size.Width - 1, pictureBox1.Location.Y + pictureBox1.Size.Height - label.Size.Height - 1);
                            type = 6;
                        }
                        else if (pictureBox1.Size.Width < label.Size.Width + 2 && pictureBox1.Size.Height >= label.Size.Height + 2)
                        {

                            if (Screen.PrimaryScreen.Bounds.Width - pictureBox1.Size.Width + pictureBox1.Location.X >= label.Size.Width + 1)
                            {
                                label.Location = new Point(pictureBox1.Location.X + pictureBox1.Size.Width, pictureBox1.Location.Y + pictureBox1.Size.Height - label.Size.Height - 1);
                                type = 7;
                            }
                            else if (Screen.PrimaryScreen.Bounds.Width - pictureBox1.Size.Width - pictureBox1.Location.X < label.Size.Width + 1)
                            {
                                label.Location = new Point(Screen.PrimaryScreen.Bounds.Width - label.Size.Width, pictureBox1.Location.Y + pictureBox1.Size.Height);
                                type = 8;
                            }
                        }
                        else if (pictureBox1.Size.Width >= label.Size.Width + 2 && pictureBox1.Size.Height < label.Size.Height + 2)
                        {
                            label.Location = new Point(pictureBox1.Location.X + pictureBox1.Size.Width - label.Size.Width - 1, pictureBox1.Location.Y + pictureBox1.Size.Height);
                            type = 9;
                        }
                        if (pictureBox1.Size.Width < label.Size.Width + 2 && pictureBox1.Size.Height < label.Size.Height + 2)
                        {
                            label.Location = new Point(zeropos[0], zeropos[1] - label.Size.Height);
                            type = 10;
                        }
                        break;
                    }
                case 3:
                    {
                        pictureBox1.Size = new Size(zeropos[0] - e.Location.X, e.Location.Y - zeropos[1]);
                        pictureBox1.Location = new Point(e.Location.X, zeropos[1]);
                        if (pictureBox1.Size.Width >= label.Size.Width + 2 && pictureBox1.Size.Height >= label.Size.Height + 2)
                        {
                            label.Location = new Point(pictureBox1.Location.X + pictureBox1.Size.Width - label.Size.Width - 1, pictureBox1.Location.Y + pictureBox1.Size.Height - label.Size.Height - 1);
                            type = 11;
                        }
                        else if (pictureBox1.Size.Width < label.Size.Width + 2 && pictureBox1.Size.Height >= label.Size.Height + 2)
                        {

                            if (Screen.PrimaryScreen.Bounds.Width - pictureBox1.Size.Width + pictureBox1.Location.X >= label.Size.Width + 1)
                            {
                                label.Location = new Point(pictureBox1.Location.X + pictureBox1.Size.Width, pictureBox1.Location.Y + pictureBox1.Size.Height - label.Size.Height - 1);
                                type = 12;
                            }
                            else if (Screen.PrimaryScreen.Bounds.Width - pictureBox1.Size.Width - pictureBox1.Location.X < label.Size.Width + 1)
                            {
                                label.Location = new Point(Screen.PrimaryScreen.Bounds.Width - label.Size.Width, pictureBox1.Location.Y + pictureBox1.Size.Height);
                                type = 13;
                            }
                        }
                        else if (pictureBox1.Size.Width >= label.Size.Width + 2 && pictureBox1.Size.Height < label.Size.Height + 2)
                        {
                            label.Location = new Point(pictureBox1.Location.X + pictureBox1.Size.Width - label.Size.Width - 1, pictureBox1.Location.Y + pictureBox1.Size.Height);
                            type = 14;
                        }
                        if (pictureBox1.Size.Width < label.Size.Width + 2 && pictureBox1.Size.Height < label.Size.Height + 2)
                        {
                            label.Location = new Point(zeropos[0], zeropos[1] - label.Size.Height);
                            type = 15;
                        }
                        break;
                    }
                case 4:
                    {
                        pictureBox1.Size = new Size(zeropos[0] - e.Location.X, zeropos[1] - e.Location.Y);
                        pictureBox1.Location = new Point(e.Location.X, e.Location.Y);
                        if (pictureBox1.Size.Width >= label.Size.Width + 2 && pictureBox1.Size.Height >= label.Size.Height + 2)
                        {
                            label.Location = new Point(pictureBox1.Location.X + pictureBox1.Size.Width - label.Size.Width - 1, pictureBox1.Location.Y + pictureBox1.Size.Height - label.Size.Height - 1);
                            type = 16;
                        }
                        else if (pictureBox1.Size.Width < label.Size.Width + 2 && pictureBox1.Size.Height >= label.Size.Height + 2)
                        {

                            if (Screen.PrimaryScreen.Bounds.Width - pictureBox1.Size.Width + pictureBox1.Location.X >= label.Size.Width + 1)
                            {
                                label.Location = new Point(pictureBox1.Location.X + pictureBox1.Size.Width, pictureBox1.Location.Y + pictureBox1.Size.Height - label.Size.Height - 1);
                                type = 17;
                            }
                            else if (Screen.PrimaryScreen.Bounds.Width - pictureBox1.Size.Width - pictureBox1.Location.X < label.Size.Width + 1)
                            {
                                label.Location = new Point(Screen.PrimaryScreen.Bounds.Width - label.Size.Width, pictureBox1.Location.Y + pictureBox1.Size.Height);
                                type = 18;
                            }
                        }
                        else if (pictureBox1.Size.Width >= label.Size.Width + 2 && pictureBox1.Size.Height < label.Size.Height + 2)
                        {
                            label.Location = new Point(pictureBox1.Location.X + pictureBox1.Size.Width - label.Size.Width - 1, pictureBox1.Location.Y + pictureBox1.Size.Height);
                            type = 19;
                        }
                        if (pictureBox1.Size.Width < label.Size.Width + 2 && pictureBox1.Size.Height < label.Size.Height + 2)
                        {
                            label.Location = new Point(zeropos[0], zeropos[1] - label.Size.Height);
                            type = 20;
                        }
                        break;
                    }
            }
            //* 1 - right, down
            //* -1 - right, up
            //* 2 - left, down
            //* -2 - left, up
        }

        public void ResizePicture(byte panelID, byte actionID)
        {
            switch (panelID)
            {
                case 1:
                    {
                        switch (actionID)
                        {
                            case 0:
                                {
                                    worktype = 2;
                                    pSize[0] = pictureBox1.Size.Width + Control.MousePosition.X;
                                    pSize[1] = pictureBox1.Size.Height + Control.MousePosition.Y;
                                    break;
                                }
                            case 1:
                                {
                                    Point newPos = new Point((pSize[0] - Control.MousePosition.X <= 10) ? pictureBox1.Location.X : Control.MousePosition.X, (pSize[1] - Control.MousePosition.Y <= 10) ? pictureBox1.Location.Y : Control.MousePosition.Y);
                                    pictureBox1.Location = newPos;
                                    pictureBox1.Size = new Size((pSize[0] - Control.MousePosition.X <= 10) ? pictureBox1.Size.Width : (pSize[0] - Control.MousePosition.X), (pSize[1] - Control.MousePosition.Y <= 10) ? pictureBox1.Size.Height : pSize[1] - Control.MousePosition.Y);
                                    break;
                                }
                            case 2:
                                {
                                    worktype = 0;
                                    pSize[0] = -1;
                                    pSize[1] = -1;
                                    break;
                                }
                        }
                        break;
                    }
                case 2:
                    {
                        switch (actionID)
                        {
                            case 0:
                                {
                                    worktype = 3;
                                    pSize[1] = pictureBox1.Size.Height + Control.MousePosition.Y;
                                    break;
                                }
                            case 1:
                                {
                                    Point newPos = new Point(pictureBox1.Location.X, (pSize[1] - Control.MousePosition.Y <= 10) ? pictureBox1.Location.Y : Control.MousePosition.Y);
                                    pictureBox1.Location = newPos;
                                    pictureBox1.Size = new Size(pictureBox1.Size.Width, (pSize[1] - Control.MousePosition.Y <= 10) ? pictureBox1.Size.Height : pSize[1] - Control.MousePosition.Y);
                                    break;
                                }
                            case 2:
                                {
                                    worktype = 0;
                                    pSize[0] = -1;
                                    pSize[1] = -1;
                                    break;
                                }
                        }
                        break;
                    }
                case 3:
                    {
                        switch (actionID)
                        {
                            case 0:
                                {
                                    worktype = 4;
                                    pSize[0] = pictureBox1.Location.X;
                                    pSize[1] = pictureBox1.Location.Y + pictureBox1.Size.Height;
                                    break;
                                }
                            case 1:
                                {
                                    Point newPos = new Point(pSize[0], (pSize[1] - Control.MousePosition.Y <= 10) ? pictureBox1.Location.Y : Control.MousePosition.Y);
                                    pictureBox1.Location = newPos;
                                    pictureBox1.Size = new Size((Control.MousePosition.X - pSize[0] <= 10) ? pictureBox1.Size.Width : Control.MousePosition.X - pSize[0], (pSize[1] - Control.MousePosition.Y <= 10) ? pictureBox1.Size.Height : pSize[1] - Control.MousePosition.Y);
                                    break;
                                }
                            case 2:
                                {
                                    worktype = 0;
                                    pSize[0] = -1;
                                    pSize[1] = -1;
                                    break;
                                }
                        }
                        break;
                    }
                case 4:
                    {
                        switch (actionID)
                        {
                            case 0:
                                {
                                    worktype = 5;
                                    pSize[0] = pictureBox1.Location.X + pictureBox1.Size.Width;
                                    break;
                                }
                            case 1:
                                {
                                    Point newPos = new Point((pSize[0] - Control.MousePosition.X <= 10) ? pictureBox1.Location.X : Control.MousePosition.X, pictureBox1.Location.Y);
                                    pictureBox1.Location = newPos;
                                    pictureBox1.Size = new Size((pSize[0] - Control.MousePosition.X <= 10) ? pictureBox1.Size.Width : pSize[0] - Control.MousePosition.X, pictureBox1.Size.Height);
                                    break;
                                }
                            case 2:
                                {
                                    worktype = 0;
                                    pSize[0] = -1;
                                    pSize[1] = -1;
                                    break;
                                }
                        }
                        break;
                    }
                case 5:
                    {
                        switch (actionID)
                        {
                            case 0:
                                {
                                    worktype = 6;
                                    pSize[0] = pictureBox1.Location.X;
                                    break;
                                }
                            case 1:
                                {
                                    pictureBox1.Size = new Size((Control.MousePosition.X - pSize[0] <= 10) ? pictureBox1.Size.Width : Control.MousePosition.X - pSize[0], pictureBox1.Size.Height);
                                    break;
                                }
                            case 2:
                                {
                                    worktype = 0;
                                    pSize[0] = -1;
                                    pSize[1] = -1;
                                    break;
                                }
                        }
                        break;
                    }
                case 6:
                    {
                        switch (actionID)
                        {
                            case 0:
                                {
                                    worktype = 7;
                                    pSize[0] = pictureBox1.Location.X + pictureBox1.Size.Width;
                                    pSize[1] = pictureBox1.Location.Y;
                                    break;
                                }
                            case 1:
                                {
                                    Point newPos = new Point((pSize[0] - Control.MousePosition.X <= 10) ? pictureBox1.Location.X : Control.MousePosition.X, pictureBox1.Location.Y);
                                    pictureBox1.Location = newPos;
                                    pictureBox1.Size = new Size((pSize[0] - Control.MousePosition.X <= 10) ? pictureBox1.Size.Width : pSize[0] - Control.MousePosition.X, (Control.MousePosition.Y - pSize[1] <= 10) ? pictureBox1.Size.Height : Control.MousePosition.Y - pSize[1]);
                                    break;
                                }
                            case 2:
                                {
                                    worktype = 0;
                                    pSize[0] = -1;
                                    pSize[1] = -1;
                                    break;
                                }
                        }
                        break;
                    }
                case 7:
                    {
                        switch (actionID)
                        {
                            case 0:
                                {
                                    worktype = 8;
                                    pSize[1] = pictureBox1.Location.Y;
                                    break;
                                }
                            case 1:
                                {
                                    pictureBox1.Size = new Size(pictureBox1.Size.Width, (Control.MousePosition.Y - pSize[1] <= 10) ? pictureBox1.Size.Height : Control.MousePosition.Y - pSize[1]);
                                    break;
                                }
                            case 2:
                                {
                                    worktype = 0;
                                    pSize[0] = -1;
                                    pSize[1] = -1;
                                    break;
                                }
                        }
                        break;
                    }
                case 8:
                    {
                        switch (actionID)
                        {
                            case 0:
                                {
                                    worktype = 9;
                                    pSize[0] = pictureBox1.Location.X;
                                    pSize[1] = pictureBox1.Location.Y;
                                    break;
                                }
                            case 1:
                                {
                                    pictureBox1.Size = new Size((Control.MousePosition.X - pSize[0] <= 10) ? pictureBox1.Size.Width : Control.MousePosition.X - pSize[0], (Control.MousePosition.Y - pSize[1] <= 10) ? pictureBox1.Size.Height : Control.MousePosition.Y - pSize[1]);
                                    break;
                                }
                            case 2:
                                {
                                    worktype = 0;
                                    pSize[0] = -1;
                                    pSize[1] = -1;
                                    break;
                                }
                        }
                        break;
                    }
            }
        }

        public void EnablePanels()
        {
            panel1.Visible = true;
            panel2.Visible = true;
            panel3.Visible = true;
            panel4.Visible = true;
            panel5.Visible = true;
            panel6.Visible = true;
            panel7.Visible = true;
            panel8.Visible = true;
            PanelsAlive();
        }

        public void DisablePanels()
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            panel6.Visible = false;
            panel7.Visible = false;
            panel8.Visible = false;
            NoPanels();
        }

        public void NoPanels()
        {
            panel1.Size = new Size(0, 0);
            panel2.Size = new Size(0, 0);
            panel3.Size = new Size(0, 0);
            panel4.Size = new Size(0, 0);
            panel5.Size = new Size(0, 0);
            panel6.Size = new Size(0, 0);
            panel7.Size = new Size(0, 0);
            panel8.Size = new Size(0, 0);
        }

        public void PanelsAlive()
        {
            panel1.Size = new Size(pnSize, pnSize);
            panel2.Size = new Size(pnSize, pnSize);
            panel3.Size = new Size(pnSize, pnSize);
            panel4.Size = new Size(pnSize, pnSize);
            panel5.Size = new Size(pnSize, pnSize);
            panel6.Size = new Size(pnSize, pnSize);
            panel7.Size = new Size(pnSize, pnSize);
            panel8.Size = new Size(pnSize, pnSize);
        }

        public void BringPictureToFront()
        {
            pictureBox1.BringToFront();
            panel1.BringToFront();
            panel2.BringToFront();
            panel3.BringToFront();
            panel4.BringToFront();
            panel5.BringToFront();
            panel6.BringToFront();
            panel7.BringToFront();
            panel8.BringToFront();
        }

        public void ResizePictureToCenter()
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            //Size newSize = new Size(Screen.PrimaryScreen.Bounds.Width - (otSize + otSize), Screen.PrimaryScreen.Bounds.Height - (otSize + otSize));
            pictureBox1.Location = new Point((Screen.PrimaryScreen.Bounds.Width - pictureBox1.Size.Width) / 2, (Screen.PrimaryScreen.Bounds.Height - pictureBox1.Size.Height) / 2);
            //pictureBox1.Size = newSize;
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            
            BringPictureToFront();

            /*
            while (true)
            {
                if (pictureBox1.Location.X > otSize) newPos.X--;
                else if (pictureBox1.Location.X < otSize) newPos.X += 3;
                else if (pictureBox1.Location.X == otSize) { }

                if (pictureBox1.Location.Y > otSize) newPos.Y--;
                else if (pictureBox1.Location.Y < otSize) newPos.Y += 3;
                else if (pictureBox1.Location.Y == otSize) { }

                // **
                if (Screen.PrimaryScreen.Bounds.Width - pictureBox1.Location.X - pictureBox1.Size.Width > otSize) newSize.Width--;
                else if (Screen.PrimaryScreen.Bounds.Width - pictureBox1.Location.X - pictureBox1.Size.Width < otSize) newSize.Width++;
                else if (Screen.PrimaryScreen.Bounds.Width - pictureBox1.Location.X - pictureBox1.Size.Width == otSize) { }

                if (Screen.PrimaryScreen.Bounds.Height - pictureBox1.Location.Y - pictureBox1.Size.Height > otSize) newSize.Height--;
                else if (Screen.PrimaryScreen.Bounds.Height - pictureBox1.Location.Y - pictureBox1.Size.Height < otSize) newSize.Height++;
                else if (Screen.PrimaryScreen.Bounds.Height - pictureBox1.Location.Y - pictureBox1.Size.Height == otSize) { }
                ** //

                if (pictureBox1.Size.Width > (Screen.PrimaryScreen.Bounds.Width - (otSize + otSize))) newSize.Width--;
                else if (pictureBox1.Size.Width < (Screen.PrimaryScreen.Bounds.Width - (otSize + otSize))) newSize.Width += 3;
                else if (pictureBox1.Size.Width == (Screen.PrimaryScreen.Bounds.Width - (otSize + otSize))) { }

                if (pictureBox1.Size.Height > (Screen.PrimaryScreen.Bounds.Height - (otSize + otSize))) newSize.Height--;
                else if (pictureBox1.Size.Height < (Screen.PrimaryScreen.Bounds.Height - (otSize + otSize))) newSize.Height += 3;
                else if (pictureBox1.Size.Height == (Screen.PrimaryScreen.Bounds.Height - (otSize + otSize))) { }

                pictureBox1.Location = newPos;
                pictureBox1.Size = newSize;
                //if (pictureBox1.Location.X == otSize && pictureBox1.Location.Y == otSize && (Screen.PrimaryScreen.Bounds.Width - pictureBox1.Location.X - pictureBox1.Size.Width == otSize) && (Screen.PrimaryScreen.Bounds.Height - pictureBox1.Location.Y - pictureBox1.Size.Height == otSize))
                if ((pictureBox1.Size.Width + otSize + otSize) == Screen.PrimaryScreen.Bounds.Width && (pictureBox1.Size.Height + otSize + otSize) == Screen.PrimaryScreen.Bounds.Height) break;
            }
            */
        }

        public void ShowHint(byte typeID, int time)
        {
            switch (typeID)
            {
                case 0: // Для выделения области необходимо удалить предыдущую
                    {
                        f5.pictureBox1.Image = EasyScreen.Properties.Resources.forselect;
                        f5.pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
                        f5.Size = new Size(EasyScreen.Properties.Resources.forselect.Width, EasyScreen.Properties.Resources.forselect.Height);
                        f5.pictureBox1.Size = new Size(EasyScreen.Properties.Resources.forselect.Width, EasyScreen.Properties.Resources.forselect.Height);
                        f5.Location = new Point((Screen.PrimaryScreen.Bounds.Width / 2) - (f5.pictureBox1.Size.Width / 2), 0);
                        f5.pictureBox1.Location = new Point(0, 0);
                        f5.pictureBox1.BackColor = Color.Transparent;
                        f5.Show();
                        f5.BringToFront();
                        Form2.SetHintLifeTime(time);
                        break;
                    }
                case 1: // Выделите нужную область
                    {
                        f5.pictureBox1.Image = EasyScreen.Properties.Resources.forfirst;
                        f5.pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
                        f5.Size = new Size(EasyScreen.Properties.Resources.forfirst.Width, EasyScreen.Properties.Resources.forfirst.Height);
                        f5.pictureBox1.Size = new Size(EasyScreen.Properties.Resources.forfirst.Width, EasyScreen.Properties.Resources.forfirst.Height);
                        f5.Location = new Point((Screen.PrimaryScreen.Bounds.Width / 2) - (f5.pictureBox1.Size.Width / 2), 0);
                        f5.pictureBox1.Location = new Point(0, 0);
                        f5.pictureBox1.BackColor = Color.Transparent;
                        f5.Show();
                        f5.BringToFront();
                        Form2.SetHintLifeTime(time);
                        break;
                    }
            }
            return;
        }

        public void HideHint()
        {
            Graphics.FromImage(EasyScreen.Properties.Resources.forselect).Clear(Color.Black);
            f5.Hide();
            //f5.pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
            //f5.pictureBox1.Size = new Size(0, 0);
        }

        public Size GetHintSize()
        {
            return f5.pictureBox1.Size;
        }

        public byte GetPictureType()
        {
            if (pictureBox1.Size.Width >= wSize + 50)
            {
                if (pictureBox1.Size.Height >= wSize + 50)
                {
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
            else
            {
                if (pictureBox1.Size.Height >= wSize + 50)
                {
                    return 3;
                }
                else
                {
                    return 4;
                }
            }

            //
            /*
             * ret 1 - normal
             * ret 2 - wide
             * ret 3 - high
             * ret 4 - tiny
             * 
             */
        }

        public void AskForEdit(byte typeID)
        {

            switch (typeID)
            {
                case 1:
                    {
                        //f3.Opacity = 0.3;
                        f3.Size = new Size(Screen.PrimaryScreen.Bounds.Width, 200);
                        f3.Location = new Point(0, Screen.PrimaryScreen.Bounds.Height / 2 - 100);
                        f3.Opacity = 0;
                        f3.tType = 1;
                        f3.timer1.Enabled = true;
                        f3.Show();

                        f4.BackColor = f4.TransparencyKey;
                        f4.pictureBox1.BackColor = f4.TransparencyKey;
                        f4.pictureBox2.BackColor = f4.TransparencyKey;
                        f4.pictureBox1.Location = new Point(pictureBox1.Location.X + (pictureBox1.Size.Width / 2 - 12 - 100), pictureBox1.Location.Y + pictureBox1.Size.Height / 2 - 100 / 2);
                        f4.pictureBox2.Location = new Point(pictureBox1.Location.X + (pictureBox1.Size.Width / 2 + 12), pictureBox1.Location.Y + pictureBox1.Size.Height / 2 - 100 / 2);
                        f4.pictureBox1.Image = EasyScreen.Properties.Resources.edBtn;
                        f4.pictureBox2.Image = EasyScreen.Properties.Resources.okBtn;
                        f4.pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
                        f4.pictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;
                        f4.Opacity = 0;
                        f4.tType = 1;
                        f4.timer1.Enabled = true;
                        f4.Show();

                        f4.BringToFront();
                        break;
                    }
                case 2:
                    {
                        f3.Opacity = 0.3;
                        f3.Size = new Size(Screen.PrimaryScreen.Bounds.Width, 200);
                        f3.Location = new Point(0, pictureBox1.Location.Y + pictureBox1.Size.Height - 25);
                        f3.Show();

                        f4.BackColor = f4.TransparencyKey;
                        f4.pictureBox1.BackColor = f4.TransparencyKey;
                        f4.pictureBox2.BackColor = f4.TransparencyKey;
                        f4.pictureBox1.Location = new Point(pictureBox1.Location.X + (pictureBox1.Size.Width / 2 - 12 - 100), pictureBox1.Location.Y + pictureBox1.Size.Height + 25);
                        f4.pictureBox2.Location = new Point(pictureBox1.Location.X + (pictureBox1.Size.Width / 2 + 12), pictureBox1.Location.Y + pictureBox1.Size.Height + 25);
                        f4.pictureBox1.Image = EasyScreen.Properties.Resources.edBtn;
                        f4.pictureBox2.Image = EasyScreen.Properties.Resources.okBtn;
                        f4.pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
                        f4.pictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;
                        f4.Show();

                        f4.BringToFront();
                        break;
                    }
                case 3:
                    {
                        f3.Opacity = 0.3;
                        f3.Size = new Size(Screen.PrimaryScreen.Bounds.Width, 200);
                        f3.Location = new Point(0, Screen.PrimaryScreen.Bounds.Height / 2 - 100);
                        f3.Show();

                        f4.BackColor = f4.TransparencyKey;
                        f4.pictureBox1.BackColor = f4.TransparencyKey;
                        f4.pictureBox2.BackColor = f4.TransparencyKey;
                        f4.pictureBox1.Location = new Point(pictureBox1.Location.X - 125, pictureBox1.Location.Y + pictureBox1.Size.Height / 2 - 100 / 2);
                        f4.pictureBox2.Location = new Point(pictureBox1.Location.X + (pictureBox1.Size.Width + 25), pictureBox1.Location.Y + pictureBox1.Size.Height / 2 - 100 / 2);
                        f4.pictureBox1.Image = EasyScreen.Properties.Resources.edBtn;
                        f4.pictureBox2.Image = EasyScreen.Properties.Resources.okBtn;
                        f4.pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
                        f4.pictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;
                        f4.Show();

                        f4.BringToFront();
                        break;
                    }
                case 4:
                    {
                        f3.Opacity = 0.3;
                        f3.Size = new Size(Screen.PrimaryScreen.Bounds.Width, 200);
                        f3.Location = new Point(0, Screen.PrimaryScreen.Bounds.Height / 2 - 100);
                        f3.Show();

                        f4.BackColor = f4.TransparencyKey;
                        f4.pictureBox1.BackColor = f4.TransparencyKey;
                        f4.pictureBox2.BackColor = f4.TransparencyKey;
                        f4.pictureBox1.Location = new Point(pictureBox1.Location.X - 125, pictureBox1.Location.Y + pictureBox1.Size.Height / 2 - 100 / 2);
                        f4.pictureBox2.Location = new Point(pictureBox1.Location.X + (pictureBox1.Size.Width + 25), pictureBox1.Location.Y + pictureBox1.Size.Height / 2 - 100 / 2);
                        f4.pictureBox1.Image = EasyScreen.Properties.Resources.edBtn;
                        f4.pictureBox2.Image = EasyScreen.Properties.Resources.okBtn;
                        f4.pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
                        f4.pictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;
                        f4.OnShownForm();
                        f4.Show();

                        f4.BringToFront();
                        break;
                    }
            }

            //Form3 f3 = new Form3(new Point(pictureBox1.Location.X + (pictureBox1.Size.Width / 2 - 12 - 100), pictureBox1.Location.Y + pictureBox1.Size.Height / 2 - 100 / 2), new Point(pictureBox1.Location.X + (pictureBox1.Size.Width / 2 + 12), pictureBox1.Location.Y + pictureBox1.Size.Height / 2 - 100 / 2));
        }

        static public Point LabelPoint(Label label)
        {
            return new Point(Screen.PrimaryScreen.Bounds.Width - label.Size.Width - 10, f3.Location.Y + f3.Size.Height - label.Size.Height - 10);
        }

        static public void CancelAsking(byte responseID)
        {

        }

        static public void LogErr(string log)
        {
            System.IO.File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\EasyScreen\\errors.log", "[" + System.DateTime.Now.ToLongTimeString() + "." + ((System.DateTime.Now.Millisecond < 100) ? ("0" + System.DateTime.Now.Millisecond.ToString()) : System.DateTime.Now.Millisecond.ToString()) + "] " + log + "\r\n");
            return;
        }

        static public bool Install(string directory, bool install)
        {
            if (install)
            {
                try
                {
                    FileInfo fn = new FileInfo(System.IO.Directory.GetCurrentDirectory() + "\\" + Process.GetCurrentProcess().ProcessName + ".exe");
                    //fn.CopyTo(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\" + Process.GetCurrentProcess().ProcessName + ".exe", true);
                    fn.CopyTo(directory, true);
                }
                catch (Exception e)
                {
                    LogErr("Ошибка при установке в директорию Program Files: \r\n" + e.ToString());
                    return false;
                }
            }
            /*
            else
            {
                try
                {
                    FileInfo fn = new FileInfo(System.IO.Directory.GetCurrentDirectory() + "\\" + Process.GetCurrentProcess().ProcessName + ".exe");
                    //fn.CopyTo(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\" + Process.GetCurrentProcess().ProcessName + ".exe", true);
                    fn.CopyTo(directory, true);
                }
                catch (Exception e)
                {
                    LogErr("Ошибка при удалении из директории Program Files: \r\n" + e.ToString());
                    return false;
                }
            }
            */
            return true;
        }

        static public bool SetAutorunValue(string directory, bool autorun)
        {
            const string name = "EasyScreen";
            //string ExePath = System.Windows.Forms.Application.ExecutablePath;
            string ExePath = directory + "\\" + Process.GetCurrentProcess().ProcessName + ".exe";
            RegistryKey reg;
            reg = Registry.LocalMachine.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run\\");
            try
            {
                if (autorun)
                    reg.SetValue(name, ExePath);
                else
                    reg.DeleteValue(name);

                reg.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }

        static public bool CheckForAutoRun()
        {
            int mode = 0;
            try
            {
                //mode = (int)Registry.GetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\Shell\\MMIReports", "AllocReports", -1);
                //mode = (int)Registry.LocalMachine.GetValue("Software\\EasyScreen\\AutoRun", 3);
                RegistryKey reg = Registry.LocalMachine.OpenSubKey("Software\\EasyScreen\\AutoRun");
                mode = (int)reg.GetValue("AllowAutoRun", 3);
                reg.Close();
            }
            catch (Exception ex)
            {
                return true;
            }
            if (mode == 2) return false;
            else if (mode == 3) return true;
            return true;
        }

        static public bool SetAutoRun(bool set)
        {
            if (set)
            {
                try
                {
                    RegistryKey reg = Registry.LocalMachine.CreateSubKey("Software\\EasyScreen\\AutoRun");
                    reg.SetValue("AllowAutoRun", 1);
                    reg.Close();
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            else
            {
                try
                {
                    RegistryKey reg = Registry.LocalMachine.CreateSubKey("Software\\EasyScreen\\AutoRun");
                    reg.SetValue("AllowAutoRun", 2);
                    reg.Close();
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
