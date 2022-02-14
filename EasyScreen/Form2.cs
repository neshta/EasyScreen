using System;
using System.Drawing;
using System.Windows.Forms;

namespace EasyScreen
{
    public partial class Form2 : Form
    {

        #region Variables

        public static bool working = false;
        public static bool Form_init = false;
        Controller ct;
        Point mOffset;

        const byte MODE_ENABLE_DARKNESS = 1;
        const byte MODE_DISABLE_DARKNESS = 2;
        const byte MODE_PICTURE_RESIZE = 3;
        const byte MODE_STAY_NONE = 0;

        byte tMode = MODE_STAY_NONE;

        private static int HintLifeTime = -1;

        #endregion

        #region SimpleFuncs

        public void OnShown()
        {
            ct.ShowHint(1, 200);
            this.Opacity = 0.85;
            this.Cursor = Cursors.Cross;
            pictureBox1.Cursor = Cursors.SizeAll;
            global::EasyScreen.Program.globalState = 1;
        }

        public static void SetHintLifeTime(int lifeTime)
        {
            HintLifeTime = lifeTime;
        }

        private void GoHideHint()
        {
            HintLifeTime = -1;
            ct.HideHint();
        }

        public void ResetPictureSize()
        {
            //if(pictureBox1.Image != null) pictureBox1.Image.Dispose();
            if (ct.worktype == 228 || ct.worktype > 10) return;
            /*
            if (pictureBox1.Image != null) Graphics.FromImage(pictureBox1.Image).Clear(this.TransparencyKey);
            Bitmap BM = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);
            Graphics GH = Graphics.FromImage(BM as Image);
            //double op = this.Opacity;
            this.Opacity = 0;
            ct.DisablePanels();
            GH.CopyFromScreen(pictureBox1.Location.X, pictureBox1.Location.Y, 0, 0, pictureBox1.Size);
            this.Opacity = 0.75;
            pictureBox1.Image = BM;
            pictureBox1.BackColor = Color.Black;
            ct.ResizePictureToCenter();
            ct.worktype = 11;

            this.Cursor = Cursors.Default;
            pictureBox1.Cursor = Cursors.Default;
            
            using (Graphics g = Graphics.FromImage(pictureBox1.Image))
            {
                SolidBrush Brush = new SolidBrush(Color.FromArgb(0, 0, 0, 0));
                g.FillRectangle(Brush, 0, 0, pictureBox1.Image.Width, pictureBox1.Image.Height);
            }
            
            ct.AskForEdit(ct.GetPictureType());
             * */
            //ct.DisablePanels();
            this.Visible = false;
            try
            {
                Bitmap BM = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);
                Graphics GH = Graphics.FromImage(BM as Image);
                GH.CopyFromScreen(pictureBox1.Location.X, pictureBox1.Location.Y, 0, 0, pictureBox1.Size);
                //this.Visible = true;
                //pictureBox1.Image = BM;
                GH.Dispose();
                //MessageBox.Show(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));
                string name = "ScreenShot - " + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".png";
                BM.Save(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\" + name, System.Drawing.Imaging.ImageFormat.Png);
                MessageBox.Show("Скриншот был сохранён на рабочий стол.", "Выполнено", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка:\n" + ex.InnerException.ToString() + "\nИсключение:\n" + ex.Message.ToString(), "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                Controller.LogErr("Произошла ошибка:\n" + ex.InnerException.ToString() + "\nИсключение:\n" + ex.Message.ToString() + "Полная ошибка:\n" + ex.ToString() + "\r\n");
            }
            global::EasyScreen.Program.globalState = 0;
            this.Hide();
            this.RestoreWorkSpace();
            
            /*
            label1.Text =
                "Size: " + pictureBox1.Image.Width + "x" + pictureBox1.Image.Height + "\n" +
                "PhysicalDimension: " + pictureBox1.Image.PhysicalDimension.Width + "x" + pictureBox1.Image.PhysicalDimension.Height + "\n" +
                "Resolution: " + pictureBox1.Image.HorizontalResolution + "x" + pictureBox1.Image.VerticalResolution + "\n" +
                "PixelFormat: " + pictureBox1.Image.PixelFormat + "\n" +
                "RawFormat: " + pictureBox1.Image.RawFormat + "\n"
            ;
            */
            //tMode = MODE_PICTURE_RESIZE;
            //timer1.Enabled = true;
        }

        public void RestoreWorkSpace()
        {
            if (ct.worktype == 228)
            {
                global::EasyScreen.Program.globalState = 0;
                this.Hide();
            }
            //if (pictureBox1.Image != null) pictureBox1.Image.Dispose();
            if (pictureBox1.Image != null) Graphics.FromImage(pictureBox1.Image).Clear(this.TransparencyKey);
            ct.Initialize();
            tMode = MODE_DISABLE_DARKNESS;
            timer1.Enabled = true;
            ct.worktype = 228;
        }

        #endregion

        #region AutoFuncs

        public Form2()
        {
            this.Hide();
            this.TopMost = true;
            InitializeComponent();
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            //this.AllowTransparency = true;
            //this.BackColor = Color.Green;
            //this.TransparencyKey = this.BackColor;
            //this.Opacity = 0.85;
            ct = new Controller(pictureBox1, pictureBox8, panel1, panel2, panel3, panel4, panel5, panel6, panel7, panel8);
            ct.Initialize();

            pictureBox1.SizeChanged += new EventHandler(ct.UpdateMovingPanels);
            pictureBox1.LocationChanged += new EventHandler(ct.UpdateMovingPanels);

            pictureBox8.BackColor = this.BackColor;

            tMode = MODE_DISABLE_DARKNESS;
            Form_init = true;
        }

        private void Form2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Left) return;
            if (ct.worktype != 228)
            {
                ct.ShowHint(0, 200);
                return;
            }
            working = true;
            ct.StartSelecting(e);
            GoHideHint();
        }

        private void Form2_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                RestoreWorkSpace();
                return;
            }

            if (working != true) return;

            if (pictureBox1.Location != new Point(0, 0) && (pictureBox1.Size.Width < 10 || pictureBox1.Size.Height < 10))
            {
                pictureBox1.Size = new Size((pictureBox1.Size.Width < 10) ? 10 : pictureBox1.Size.Width, (pictureBox1.Size.Height < 10) ? 10 : pictureBox1.Size.Height);
                MessageBox.Show("Минимальный размер рабочей области 10х10 px.\nУказанная область была автоматически увеличена до минимальных размеров.", "EasyScreen - Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            ct.StopSelecting(sender, e);
            label2.Visible = false;
            working = false;
            /*
            double res = (double)pictureBox1.Size.Width / pictureBox1.Size.Height;


            label1.Text = "pn1: " + panel1.Location.ToString() + "\n" +
                          "pn2: " + panel2.Location.ToString() + "\n" +
                          "pn3: " + panel3.Location.ToString() + "\n" +
                          "pn4: " + panel4.Location.ToString() + "\n" +
                          "pn5: " + panel5.Location.ToString() + "\n" +
                          "pn6: " + panel6.Location.ToString() + "\n" +
                          "pn7: " + panel7.Location.ToString() + "\n" +
                          "pn8: " + panel8.Location.ToString() + "\n" + 
                          "SCREEN RESOLUTION: " + Screen.PrimaryScreen.Bounds.Width + "x" + Screen.PrimaryScreen.Bounds.Height + "\n" +
                          "IMAGE SOURCE: " + pictureBox1.Size.Width + "x" + pictureBox1.Size.Height + "\n" +
                          "IMAGE DESTIN: " + pictureBox1.Size.Width * pictureBox1.Size.Height + "; " + res + "\n"
            ;
            */
            
        }

        private void Form2_MouseMove(object sender, MouseEventArgs e)
        {
            if (working != true) return;
            ct.PictureBoxUpdate(e, label2);
            label2.Text = pictureBox1.Size.Width + "x" + pictureBox1.Size.Height;
            label2.Visible = true;
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form_init = false;
        }

        private void Form2_MouseEnter(object sender, EventArgs e)
        {
            if (tMode == MODE_PICTURE_RESIZE || ct.worktype > 10) return;
            tMode = MODE_DISABLE_DARKNESS;
            //timer1.Enabled = true;
        }

        private void Form2_MouseLeave(object sender, EventArgs e)
        {
            if (tMode == MODE_PICTURE_RESIZE || ct.worktype > 10) return;
            tMode = MODE_ENABLE_DARKNESS;
            //timer1.Enabled = true;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right) RestoreWorkSpace();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (HintLifeTime > 0) HintLifeTime--;
            if (HintLifeTime == 0) GoHideHint();
            if (Control.MousePosition.Y < ct.GetHintSize().Height && (Control.MousePosition.X > Screen.PrimaryScreen.Bounds.Width / 2 - ct.GetHintSize().Width / 2 && Control.MousePosition.X < Screen.PrimaryScreen.Bounds.Width / 2 + ct.GetHintSize().Width / 2))
            {
                GoHideHint();
            }
            //label1.Visible = true;
            //label1.ForeColor = Color.Aqua;
            //label1.Text = ct.worktype.ToString();
            //if (pictureBox1.Location.Y <= pictureBox8.Location.Y + pictureBox8.Size.Height) GoHideHint();

            switch (tMode)
            {
                case MODE_ENABLE_DARKNESS:
                    {
                        if (this.Opacity < 0.85) this.Opacity += 0.01;
                        else if (this.Opacity == 0.85)
                        {
                            tMode = MODE_STAY_NONE;
                            //timer1.Enabled = false;
                        }
                        break;
                    }
                case MODE_DISABLE_DARKNESS:
                    {
                        if (this.Opacity > 0.5) this.Opacity -= 0.01;
                        else if (this.Opacity == 0.6)
                        {
                            tMode = MODE_STAY_NONE;
                            //timer1.Enabled = false;
                        }
                        break;
                    }
                case MODE_STAY_NONE:
                    {
                        //timer1.Enabled = false;
                        break;
                    }
                case MODE_PICTURE_RESIZE:
                    {
                        if (pictureBox1.Location.X == 0 && pictureBox1.Location.Y == 0 && pictureBox1.Size.Width == Screen.PrimaryScreen.Bounds.Width && pictureBox1.Size.Height == Screen.PrimaryScreen.Bounds.Height)
                        {
                            tMode = MODE_STAY_NONE;
                            //timer1.Enabled = false;
                        }
                        //pictureBox1.Size = new Size((pictureBox1.Size.Width >= Screen.PrimaryScreen.Bounds.Width - otSize*2) ? pictureBox1.Size.Width : pictureBox1.Size.Width + 5, (pictureBox1.Size.Height >= Screen.PrimaryScreen.Bounds.Height - otSize*2) ? pictureBox1.Size.Height : pictureBox1.Size.Height + 5);
                        //pictureBox1.Location = new Point((pictureBox1.Location.X <= otSize) ? otSize : pictureBox1.Location.X - 5, (pictureBox1.Location.Y <= otSize) ? otSize : pictureBox1.Location.Y - 5);
                        
                        break;
                    }
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            ct.ResizePicture(1, 0);
            ct.DisablePanels();
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (ct.worktype != 2) return;
            ct.ResizePicture(1, 1);
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            ct.ResizePicture(1, 2);
            ct.EnablePanels();
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            ct.ResizePicture(2, 0);
            ct.DisablePanels();
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (ct.worktype != 3) return;
            ct.ResizePicture(2, 1);
        }

        private void panel2_MouseUp(object sender, MouseEventArgs e)
        {
            ct.ResizePicture(2, 2);
            ct.EnablePanels();
        }

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            ct.ResizePicture(3, 0);
            ct.DisablePanels();
        }

        private void panel3_MouseMove(object sender, MouseEventArgs e)
        {
            if (ct.worktype != 4) return;
            ct.ResizePicture(3, 1);
        }

        private void panel3_MouseUp(object sender, MouseEventArgs e)
        {
            ct.ResizePicture(3, 2);
            ct.EnablePanels();
        }

        private void panel4_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            ct.ResizePicture(4, 0);
            ct.DisablePanels();
        }

        private void panel4_MouseMove(object sender, MouseEventArgs e)
        {
            if (ct.worktype != 5) return;
            ct.ResizePicture(4, 1);
        }

        private void panel4_MouseUp(object sender, MouseEventArgs e)
        {
            ct.ResizePicture(4, 2);
            ct.EnablePanels();
        }

        private void panel5_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            ct.ResizePicture(5, 0);
            ct.DisablePanels();
        }

        private void panel5_MouseMove(object sender, MouseEventArgs e)
        {
            if (ct.worktype != 6) return;
            ct.ResizePicture(5, 1);
        }

        private void panel5_MouseUp(object sender, MouseEventArgs e)
        {
            ct.ResizePicture(5, 2);
            ct.EnablePanels();
        }

        private void panel6_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            ct.ResizePicture(6, 0);
            ct.DisablePanels();
        }

        private void panel6_MouseMove(object sender, MouseEventArgs e)
        {
            if (ct.worktype != 7) return;
            ct.ResizePicture(6, 1);
        }

        private void panel6_MouseUp(object sender, MouseEventArgs e)
        {
            ct.ResizePicture(6, 2);
            ct.EnablePanels();
        }

        private void panel7_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            ct.ResizePicture(7, 0);
            ct.DisablePanels();
        }

        private void panel7_MouseMove(object sender, MouseEventArgs e)
        {
            if (ct.worktype != 8) return;
            ct.ResizePicture(7, 1);
        }

        private void panel7_MouseUp(object sender, MouseEventArgs e)
        {
            ct.ResizePicture(7, 2);
            ct.EnablePanels();
        }

        private void panel8_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            ct.ResizePicture(8, 0);
            ct.DisablePanels();
        }

        private void panel8_MouseMove(object sender, MouseEventArgs e)
        {
            if (ct.worktype != 9) return;
            ct.ResizePicture(8, 1);
        }

        private void panel8_MouseUp(object sender, MouseEventArgs e)
        {
            ct.ResizePicture(8, 2);
            ct.EnablePanels();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Left || ct.worktype > 10) return;
            mOffset = new Point(-e.X, -e.Y);
            ct.worktype = 10;
            ct.DisablePanels();
            //label2.Visible = true;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (ct.worktype != 10) return;
            Point mousePos = Control.MousePosition;
            mousePos.Offset(mOffset.X, mOffset.Y);
            if (mousePos.X < 0) mousePos.X = 0;
            else if (mousePos.X + pictureBox1.Size.Width > Screen.PrimaryScreen.Bounds.Width) mousePos.X = Screen.PrimaryScreen.Bounds.Width - pictureBox1.Size.Width;
            if (mousePos.Y < 0) mousePos.Y = 0;
            else if (mousePos.Y + pictureBox1.Size.Height > Screen.PrimaryScreen.Bounds.Height) mousePos.Y = Screen.PrimaryScreen.Bounds.Height - pictureBox1.Size.Height;
            pictureBox1.Location = mousePos;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (ct.worktype != 10) return;
            ct.worktype = 0;
            ct.EnablePanels();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
        }

        private void Form2_Shown(object sender, EventArgs e)
        {

        }

        private void Form2_Resize(object sender, EventArgs e)
        {

        }

        #endregion

    }
}
