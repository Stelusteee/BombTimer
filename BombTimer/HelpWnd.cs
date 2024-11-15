using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BombTimer
{
    public partial class HelpWnd : Form
    {
        public HelpWnd()
        {
            InitializeComponent();

            /*float dpiScale = 144f / DeviceDpi;
            PointF scaleFactor = new PointF((float)Width / 400, (float)Height / 300);

            HelpLabel.Location = new Point((int)(12 * scaleFactor.X), (int)(9 * scaleFactor.Y));
            HelpLabel.Font = new Font("Segoe UI", 9F * dpiScale * scaleFactor.X);

            label1.Location = new Point((int)(12 * scaleFactor.X), (int)(80 * scaleFactor.Y));
            label1.Font = new Font("Segoe UI", 9F * dpiScale * scaleFactor.X);

            label2.Location = new Point((int)(12 * scaleFactor.X), (int)(105 * scaleFactor.Y));
            label2.Font = new Font("Segoe UI", 9F * dpiScale * scaleFactor.X);

            label3.Location = new Point((int)(12 * scaleFactor.X), (int)(130 * scaleFactor.Y));
            label3.Font = new Font("Segoe UI", 9F * dpiScale * scaleFactor.X);

            label4.Location = new Point((int)(12 * scaleFactor.X), (int)(155 * scaleFactor.Y));
            label4.Font = new Font("Segoe UI", 9F * dpiScale * scaleFactor.X);

            label5.Location = new Point((int)(12 * scaleFactor.X), (int)(180 * scaleFactor.Y));
            label5.Font = new Font("Segoe UI", 9F * dpiScale * scaleFactor.X);

            button1.Location = new Point((int)(276 * scaleFactor.X), (int)(254 * scaleFactor.Y));
            button1.Font = new Font("Segoe UI", 9F * dpiScale * scaleFactor.X);*/
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
