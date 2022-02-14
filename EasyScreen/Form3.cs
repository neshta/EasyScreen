using System;
using System.Windows.Forms;

namespace EasyScreen
{
    public partial class Form3 : Form
    {

        //Point loc1, loc2;
        public byte tType = 0;

        public Form3()
        {
            InitializeComponent();
            //loc1 = l1;
            //loc2 = l2;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            //CallButtons(loc1, loc2);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (tType)
            {
                case 0:
                    {
                        timer1.Enabled = false;
                        break;
                    }
                case 1:
                    {
                        if (this.Opacity != 0.3) this.Opacity += 0.01;
                        if (this.Opacity >= 0.3) tType = 0;
                        break;
                    }
            }
        }
    }
}
