using System.Diagnostics;

namespace BombTimer
{
    public partial class UpdateNotifier : Form
    {
        public UpdateNotifier()
        {
            InitializeComponent();
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
