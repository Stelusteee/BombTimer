using System.Diagnostics;

namespace BombTimer
{
    public partial class UpdateNotifier : Form
    {
        public UpdateNotifier()
        {
            InitializeComponent();

            float dpiScale = 144f / DeviceDpi;
            PointF scaleFactor = new ((float)Width / 500, (float)Height / 200);
            
            label1.Location = new Point((int)(12 * scaleFactor.X), (int)(9 * scaleFactor.Y));
            label1.Font = new Font("Arial", 10F * dpiScale * scaleFactor.X, FontStyle.Regular, GraphicsUnit.Point, 0);
            
            label2.Location = new Point((int)(51 * scaleFactor.X), (int)(88 * scaleFactor.Y));
            label2.Font = new Font("Arial", 10F * dpiScale * scaleFactor.X, FontStyle.Regular, GraphicsUnit.Point, 0);
            
            declineBtn.Location = new Point((int)(12 * scaleFactor.X), (int)(154 * scaleFactor.Y));
            declineBtn.Font = new Font("Arial", 10F * dpiScale * scaleFactor.X, FontStyle.Regular, GraphicsUnit.Point, 0);
            
            updateBtn.Location = new Point((int)(376 * scaleFactor.X), (int)(154 * scaleFactor.Y));
            updateBtn.Font = new Font("Arial", 10F * dpiScale * scaleFactor.X, FontStyle.Regular, GraphicsUnit.Point, 0);
        }

        private void declineBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            if (File.Exists("UpdateDownloader.exe")) File.Move("UpdateDownloader.exe", "UpdateDownloaderUsed.exe");
            Process.Start("UpdateDownloaderUsed.exe");
            Application.Exit();
        }
    }
}
