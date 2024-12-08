using Newtonsoft.Json;
using SFML.Audio;
using Octokit;
using System.Net;
//using System.Diagnostics;

namespace BombTimer
{
    public partial class Wnd : Form
    {
        UpdateNotifier updateNotifier = new UpdateNotifier();

        string currentVersion = "v1.3.2";
        string workspaceName = "Stelusteee";
        string repositoryName = "BombTimer";
        public async void CheckForUpdate()
        {
            var client = new GitHubClient(new ProductHeaderValue("BombTimer"));
            var releases = await client.Repository.Release.GetAll(workspaceName, repositoryName);
            
            using (var wc = new WebClient())
            {
                try
                {
                    if (currentVersion != releases[0].TagName)
                    {
                        updateNotifier.ShowDialog();
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        int WndSclAmount = 50;
        SaveData data = new SaveData();
        public Wnd()
        {
            try { CheckForUpdate(); } catch (Exception) {}

            if (File.Exists("UpdateDownloaderUsed.exe"))
            {
                File.Delete("UpdateDownloaderUsed.exe");
            }

            if (File.Exists("save.json"))
            {
                string json = File.ReadAllText("save.json");
                data = JsonConvert.DeserializeObject<SaveData>(json);
            }
            else
            {
                data.soundIndex = 1;
                data.wndLocation = new Point(0, 0);
                data.wndSize = new Size(1, 1) * (minWndSize + maxWndSize) / 2;
                string json = JsonConvert.SerializeObject(data, Formatting.Indented);
                File.WriteAllText("save.json", json);
            }

            InitializeComponent();

            WndSclAmount = Screen.PrimaryScreen.Bounds.Height / 30;

            minWndSize = RoundToN(Screen.PrimaryScreen.Bounds.Height / 4, WndSclAmount);
            maxWndSize = RoundToN(Screen.PrimaryScreen.Bounds.Height / 2, WndSclAmount);

            if (data.wndSize.Width > maxWndSize || data.wndSize.Width < minWndSize)
                data.wndSize = new Size(1, 1) * (minWndSize + maxWndSize) / 2;

            Location = data.wndLocation;
            ClientSize = data.wndSize;

            ctxMenuStrip.Renderer = new CustomContextMenuRenderer();
        }

        readonly List<string> soundList = new List<string> { "sounds/moveout.wav", "sounds/bombpl.wav", "sounds/com_go.wav", "sounds/letsgo.wav", "sounds/locknload.wav" };
        SoundBuffer startBuffer;
        Sound startSound = new Sound();
        int soundIndex;
        private void Wnd_Load(object sender, EventArgs e)
        {
            soundIndex = data.soundIndex;
            SoundSelectAction(soundIndex);
            InputText.Parent = c4img;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case (Keys.Control | Keys.Q):
                    QuitAction();
                    return true;
                case (Keys.Control | Keys.R):
                    DefuseAction();
                    return true;
                case (Keys.Control | Keys.H):
                    HideAction();
                    return true;
                case (Keys.Control | Keys.Oemplus):
                    ScaleWindow(WndSclAmount);
                    return true;
                case (Keys.Control | Keys.OemMinus):
                    ScaleWindow(-WndSclAmount);
                    return true;
                case (Keys.Control | Keys.D0):
                    SoundSelectAction(0);
                    return true;
                case (Keys.Control | Keys.D1):
                    SoundSelectAction(1);
                    return true;
                case (Keys.Control | Keys.D2):
                    SoundSelectAction(2);
                    return true;
                case (Keys.Control | Keys.D3):
                    SoundSelectAction(3);
                    return true;
                case (Keys.Control | Keys.D4):
                    SoundSelectAction(4);
                    return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void NotifyIcon_Click(object? sender, EventArgs e)
        {
            Show();
            notifyIcon.Visible = false;
        }

        bool mouseDown = false;
        private void Wnd_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                preMPos = MousePosition;
                mouseDown = true;
            }
        }

        private void Wnd_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDown = false;
            }
        }

        private static Point preMPos;
        private static Point curMPos;
        private void Wnd_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                curMPos = MousePosition;

                Point mouseDelta = new(curMPos.X - preMPos.X, curMPos.Y - preMPos.Y);

                preMPos = curMPos;

                Location = new Point(Location.X + mouseDelta.X, Location.Y + mouseDelta.Y);
            }
        }

        private void Wnd_Resize(object sender, EventArgs e)
        {
            float dpiScale = 96f / DeviceDpi;
            float scaleFactor = (float)Width / 350;
            InputText.Location = new Point((int)(140 * scaleFactor), (int)(96 * scaleFactor));
            InputText.Font = new Font(fontCollection.Families[0], 18F * dpiScale * scaleFactor, FontStyle.Regular, GraphicsUnit.Point, 0);
        }

        #region Context Menu
        private void QuitOption_Click(object sender, EventArgs e)
        {
            QuitAction();
        }

        private void HelpOption_Click(object sender, EventArgs e)
        {
            //MessageBox.Show((DeviceDpi * ClientSize.Width).ToString());
            HelpWnd helpWnd = new HelpWnd();
            helpWnd.label5.Text = currentVersion;
            helpWnd.ShowDialog();
        }

        private void UpOption_Click(object sender, EventArgs e)
        {
            ScaleWindow(WndSclAmount);
        }

        private void DownOption_Click(object sender, EventArgs e)
        {
            ScaleWindow(-WndSclAmount);
        }

        private void DefuseOption_Click(object sender, EventArgs e)
        {
            DefuseAction();
        }

        private void HideOption_Click(object sender, EventArgs e)
        {
            HideAction();
        }

        private void LocknloadOpt_Click(object sender, EventArgs e)
        {
            SoundSelectAction(4);
        }

        private void LetsGoOpt_Click(object sender, EventArgs e)
        {
            SoundSelectAction(3);
        }

        private void ComgoOpt_Click(object sender, EventArgs e)
        {
            SoundSelectAction(2);
        }

        private void BombplOpt_Click(object sender, EventArgs e)
        {
            SoundSelectAction(1);
        }

        private void MoveoutOpt_Click(object sender, EventArgs e)
        {
            SoundSelectAction(0);
        }
        #endregion

        private void QuitAction()
        {
            data.soundIndex = soundIndex;
            data.wndLocation = Location;
            data.wndSize = ClientSize;
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText("save.json", json);

            System.Windows.Forms.Application.Exit();
        }

        SoundBuffer defuseBuffer = new SoundBuffer("sounds/bombdef.wav");
        Sound defuseSound = new Sound();
        private void DefuseAction()
        {
            if (timerStarted)
            {
                CountdownTimer.timer?.Stop();
                ResetTimer();
                defuseSound.SoundBuffer = defuseBuffer;
                defuseSound.Play();
            }
        }

        NotifyIcon notifyIcon = new NotifyIcon();
        private void HideAction()
        {
            notifyIcon.Icon = (Icon)rsrc.GetObject("$this.Icon");
            notifyIcon.Visible = true;
            notifyIcon.Click += NotifyIcon_Click;
            Hide();
        }

        int RoundToN(int number, int N)
        {
            return (number + (N / 2)) / N * N;
        }

        int minWndSize, maxWndSize;
        private void ScaleWindow(int scaleAmount)
        {
            int newSize = Width + scaleAmount;
            newSize = Math.Max(minWndSize, Math.Min(maxWndSize, newSize));
            ClientSize = new Size(newSize, newSize);
        }

        private void SoundSelectAction(int i)
        {
            startBuffer = new SoundBuffer(soundList[i]);
            soundIndex = i;
        }
    }
}
