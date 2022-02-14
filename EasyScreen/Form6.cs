using System;
using System.Drawing;
using System.Windows.Forms;

namespace EasyScreen
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        protected override void SetVisibleCore(bool value)
        {
            base.SetVisibleCore(true);
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.Size = new Size(0, 0);
            this.Location = new Point(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
        }
    }
}


