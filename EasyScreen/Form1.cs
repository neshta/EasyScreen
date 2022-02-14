using System;
using System.Drawing;
using System.Windows.Forms;

namespace EasyScreen
{
    public partial class Form1 : Form
    {

        bool moving = false;

        private Point mouseOffset;

        public void UpdateLabelText(int VK, int SC, int Flag)
        {
            if(Program.Form1_init != true) return;
            label1.Text = "VirtKey: " + VK + "\nScanCode: " + SC + "\nFlag: " + Flag;
        }

        public Form1()
        {
            //if (Program.Form1_init == true) return;
            //this.Hide();
            this.Visible = false;
            this.Hide();
            InitializeComponent();
            Program.Form1_init = true;
            //this.Show();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            /*
            if (e.Button == MouseButtons.Left)
            {
                mouseOffset = new Point(-e.X, -e.Y);
                moving = true;
            }
            */
            //MessageBox.Show(label1.Text, "info", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }

        private void panel1_MouseEnter(object sender, EventArgs e)
        {
            //MessageBox.Show("_MouseEnter()1", "info", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }

        private void panel1_MouseHover(object sender, EventArgs e)
        {
            //MessageBox.Show("_MouseHover()1", "info", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }

        private void panel1_MouseLeave(object sender, EventArgs e)
        {
            //MessageBox.Show("_MouseLeave()1", "info", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            //if(e.Button == System.Windows.Forms.MouseButtons.Left) moving = false;
            //MessageBox.Show("_MouseUp()1", "info", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            /*
            if (moving == true)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouseOffset.X, mouseOffset.Y);
                Location = mousePos;
                return;
            }
             * */
        }

        void MoveWindow(int posX, int posY)
        {
            this.Location = new Point(posX, posY);
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox5.BackColor = Color.FromArgb(255, 185, 119, 119);
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox5.BackColor = Color.FromArgb(255, 172, 119, 119);
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            pictureBox4.BackColor = Color.FromArgb(255, 109, 130, 226);
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox4.BackColor = Color.FromArgb(255, 92, 130, 226);
        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseOffset = new Point(-e.X, -e.Y);
                moving = true;
            }
        }

        private void pictureBox3_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left) moving = false;
        }

        private void pictureBox3_MouseMove(object sender, MouseEventArgs e)
        {
            if (moving == true)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouseOffset.X, mouseOffset.Y);
                Location = mousePos;
                return;
            }
        }

        private void pictureBox4_MouseEnter(object sender, EventArgs e)
        {
            System.Drawing.Bitmap img = EasyScreen.Properties.Resources.EasyScreen_Min_Internal;
            pictureBox4.Image = img;
        }

        private void pictureBox4_MouseDown(object sender, MouseEventArgs e)
        {
            System.Drawing.Bitmap img = EasyScreen.Properties.Resources.EasyScreen_Min_Down;
            pictureBox4.Image = img;
        }

        private void pictureBox4_MouseUp(object sender, MouseEventArgs e)
        {
            System.Drawing.Bitmap img = EasyScreen.Properties.Resources.EasyScreen_Minimize;
            pictureBox4.Image = img;
        }

        private void pictureBox5_MouseDown(object sender, MouseEventArgs e)
        {
            System.Drawing.Bitmap img = EasyScreen.Properties.Resources.EasyScreen_Close_Down;
            pictureBox5.Image = img;
        }

        private void pictureBox5_MouseEnter(object sender, EventArgs e)
        {
            System.Drawing.Bitmap img = EasyScreen.Properties.Resources.EasyScreen_Close_Internal;
            pictureBox5.Image = img;
        }

        private void pictureBox5_MouseUp(object sender, MouseEventArgs e)
        {
            System.Drawing.Bitmap img = EasyScreen.Properties.Resources.EasyScreen_Close;
            pictureBox5.Image = img;
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            System.Drawing.Bitmap img = EasyScreen.Properties.Resources.EasyScreen_Minimize;
            pictureBox4.Image = img;
        }

        private void pictureBox5_MouseLeave(object sender, EventArgs e)
        {
            System.Drawing.Bitmap img = EasyScreen.Properties.Resources.EasyScreen_Close;
            pictureBox5.Image = img;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this.Hide();
            //Form2 f2 = new Form2();
            //f2.Show();
            //this.Show();
        }

    }
}
