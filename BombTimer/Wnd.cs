using Newtonsoft.Json;
using SFML.Audio;

namespace BombTimer
{
    public partial class Wnd : Form
    {
        SaveData data = new SaveData();
        public Wnd()
        {
            if (File.Exists("save.json"))
            {
                string json = File.ReadAllText("save.json");
                data = JsonConvert.DeserializeObject<SaveData>(json);
            }
            else
            {
                data.soundIndex = 1;
                data.wndLocation = new Point(0, 0);
                data.wndSize = new Size(350, 350);
                string json = JsonConvert.SerializeObject(data, Formatting.Indented);
                File.WriteAllText("save.json", json);
            }
            InitializeComponent();
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
            if (keyData == (Keys.Control | Keys.Q))
            {
                QuitAction();
                return true;
            }

            if (keyData == (Keys.Control | Keys.R))
            {
                DefuseAction();
                return true;
            }

            if (keyData == (Keys.Control | Keys.H))
            {
                HideAction();
                return true;
            }

            if (keyData == (Keys.Control | Keys.Oemplus))
            {
                ScaleWindow(50);
                return true;
            }

            if (keyData == (Keys.Control | Keys.OemMinus))
            {
                ScaleWindow(-50);
                return true;
            }

            if (keyData == (Keys.Control | Keys.D0))
            {
                SoundSelectAction(0);
                return true;
            }

            if (keyData == (Keys.Control | Keys.D1))
            {
                SoundSelectAction(1);
                return true;
            }

            if (keyData == (Keys.Control | Keys.D2))
            {
                SoundSelectAction(2);
                return true;
            }

            if (keyData == (Keys.Control | Keys.D3))
            {
                SoundSelectAction(3);
                return true;
            }

            if (keyData == (Keys.Control | Keys.D4))
            {
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

                Point mouseDelta = new Point(curMPos.X - preMPos.X, curMPos.Y - preMPos.Y);

                preMPos = curMPos;

                Location = new Point(Location.X + mouseDelta.X, Location.Y + mouseDelta.Y);
            }
        }

        // ****************** //
        // REWRITE THIS LATER //
        // ****************** //
        private void Wnd_Resize(object sender, EventArgs e)
        {
            float scaleFactor = (float)Width / 350;
            InputText.Location = new Point((int)(140 * scaleFactor), (int)(96 * scaleFactor));
            InputText.Font = new Font(InputText.Font.FontFamily, 12f * scaleFactor, FontStyle.Regular, GraphicsUnit.Point, 0);
        }

        #region Context Menu
        private void QuitOption_Click(object sender, EventArgs e)
        {
            QuitAction();
        }

        private void UpOption_Click(object sender, EventArgs e)
        {
            ScaleWindow(50);
        }

        private void DownOption_Click(object sender, EventArgs e)
        {
            ScaleWindow(-50);
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

            Application.Exit();
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

        NotifyIcon notifyIcon;
        private void HideAction()
        {
            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = (Icon)rsrc.GetObject("$this.Icon");
            notifyIcon.Visible = true;
            notifyIcon.Click += NotifyIcon_Click;
            Hide();
        }

        const int minSize = 250;
        int maxSize = 540;
        private void ScaleWindow(int scaleAmount)
        {
            int newSize = Width + scaleAmount;
            maxSize = Screen.PrimaryScreen.Bounds.Height / 2;
            newSize = Math.Max(minSize, Math.Min(maxSize, newSize));
            ClientSize = new Size(newSize, newSize);
        }

        private void SoundSelectAction(int i)
        {
            startBuffer = new SoundBuffer(soundList[i]);
            soundIndex = i;
        }
    }
}