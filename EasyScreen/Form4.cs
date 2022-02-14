using System;
using System.Drawing;
using System.Windows.Forms;

namespace EasyScreen
{
    public partial class Form4 : Form
    {

        public byte tType = 0;

        public Form4()
        {
            InitializeComponent();
            pictureBox3.Visible = false;
            label1.Visible = false;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Left) return;
            if (tType < 2)
            {
                MessageBox.Show("Редактирование скриншотов будет доступно в следующих версиях.\nСледите за обновлениями.", "Недоступно", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            //Environment.Exit(0);
        }

        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Left) return;
            if (tType < 2)
            {
                tType = 2;
                timer1.Enabled = true;
            }
            else if (tType == 4)
            {
                //MessageBox.Show("ASGNU93B91B");
            }
            //Environment.Exit(0);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Location = Controller.LabelPoint(label1);
            label1.Visible = true;
            switch (tType)
            {
                case 0:
                    {
                        timer1.Enabled = false;
                        break;
                    }
                case 1:
                    {
                        if (this.Opacity != 1) this.Opacity += 0.03;
                        if (this.Opacity >= 1) tType = 0;
                        break;
                    }
                case 2:
                    {
                        if (this.Opacity != 0) this.Opacity -= 0.05;
                        if (this.Opacity <= 0)
                        {
                            //pictureBox2.Location = new Point(Screen.PrimaryScreen.Bounds.Width / 2 - pictureBox2.Size.Width / 2, Screen.PrimaryScreen.Bounds.Height / 2 - pictureBox2.Size.Height / 2);
                            //pictureBox1.Location = new Point(pictureBox2.Location.X - pictureBox1.Size.Width - 25, Screen.PrimaryScreen.Bounds.Height / 2 - pictureBox1.Size.Height / 2);
                            //pictureBox3.Location = new Point(pictureBox2.Location.X + pictureBox2.Size.Width + 25, Screen.PrimaryScreen.Bounds.Height / 2 - pictureBox3.Size.Height / 2);
                            pictureBox1.Image = EasyScreen.Properties.Resources.shBtn;
                            pictureBox2.Image = EasyScreen.Properties.Resources.svBtn;
                            tType = 3;
                        }
                        break;
                    }
                case 3:
                    {
                        if (this.Opacity != 1) this.Opacity += 0.05;
                        if (this.Opacity >= 1)
                        {
                            tType = 4;
                        }
                        break;
                    }
                case 4:
                    {
                        timer1.Enabled = false;
                        break;
                    }
            }
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.Location = new Point(pictureBox1.Location.X - 5, pictureBox1.Location.Y - 5);
            pictureBox1.Size = new Size(pictureBox1.Size.Width + 10, pictureBox1.Size.Height + 10);
            if (tType < 2) label1.Text = "Редактировать скриншот";
            else if (tType == 4) label1.Text = "Загрузить на Imgur";
            label1.Location = Controller.LabelPoint(label1);
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            //pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.Location = new Point(pictureBox1.Location.X + 5, pictureBox1.Location.Y + 5);
            pictureBox1.Size = new Size(pictureBox1.Size.Width - 10, pictureBox1.Size.Height - 10);
            label1.Text = "";
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.Location = new Point(pictureBox2.Location.X - 5, pictureBox2.Location.Y - 5);
            pictureBox2.Size = new Size(pictureBox2.Size.Width + 10, pictureBox2.Size.Height + 10);
            if (tType < 2) label1.Text = "Далее";
            else if (tType == 4) label1.Text = "Сохранить на Рабочий стол";
            label1.Location = Controller.LabelPoint(label1);
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Location = new Point(pictureBox2.Location.X + 5, pictureBox2.Location.Y + 5);
            pictureBox2.Size = new Size(pictureBox2.Size.Width - 10, pictureBox2.Size.Height - 10);
            label1.Text = "";
        }

        public void OnShownForm()
        {
            label1.Visible = true;
            label1.Location = new Point((Screen.PrimaryScreen.Bounds.Width - pictureBox1.Size.Width - pictureBox1.Size.Width - (pictureBox2.Location.X - pictureBox1.Location.X)) / 2 - label1.Size.Width / 2);
        }
    }
}
