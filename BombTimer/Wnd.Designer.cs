using SFML.Audio;
using System.ComponentModel;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Text;

namespace BombTimer
{
    partial class Wnd
    {
        private IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private const int HWND_TOPMOST = -1;
        private const int SWP_NOSIZE = 0x0001;
        private const int SWP_NOMOVE = 0x0002;

        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

        protected override void WndProc(ref Message wm)
        {
            const int WM_ACTIVATE = 0x0006;

            if (wm.Msg == WM_ACTIVATE)
            {
                SetWindowPos(Handle, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE);
            }

            base.WndProc(ref wm);
        }

        #region TIMER
        Keys[] numKeys =
        {
            Keys.D0, Keys.NumPad0,
            Keys.D1, Keys.NumPad1,
            Keys.D2, Keys.NumPad2,
            Keys.D3, Keys.NumPad3,
            Keys.D4, Keys.NumPad4,
            Keys.D5, Keys.NumPad5,
            Keys.D6, Keys.NumPad6,
            Keys.D7, Keys.NumPad7,
            Keys.D8, Keys.NumPad8,
            Keys.D9, Keys.NumPad9
        };

        StringBuilder inputStr = new StringBuilder("000000");
        string inputStrSave;
        string timeStr = "00:00:00";
        string timeStrSave;
        static bool timerStarted = false;
        private void Wnd_KeyDown(object sender, KeyEventArgs e)
        {
            foreach (Keys key in numKeys)
            {
                if (e.KeyCode == key && !timerStarted)
                {
                    inputStr.Append(key.ToString()[^1]);
                    if (inputStr.Length > 6) inputStr.Remove(0, 1);

                    timeStr = $"{inputStr.ToString(0, 2):D2}:{inputStr.ToString(2, 2):D2}:{inputStr.ToString(4, 2):D2}";
                }
            }

            if (e.KeyCode == Keys.Back && !timerStarted)
            {
                ResetTimer();
            }

            InputText.Text = timeStr;

            if (e.KeyCode == Keys.Enter && !timerStarted && !inputStr.Equals("000000"))
            {
                inputStrSave = inputStr.ToString();
                timeStrSave = timeStr;

                timerStarted = true;
                CountdownTimer.Entry(inputStr);
                CountdownTimer.OutPutEvent += TimerOutput;
                startSound.SoundBuffer = startBuffer;
                startSound.Play();
            }
        }

        SoundBuffer beepBuffer = new SoundBuffer("sounds/c4_click.wav");
        Sound beepSound = new Sound();
        private void TimerOutput()
        {
            if (timerStarted && CountdownTimer.outPut != "99:99:99")
            {
                timeStr = CountdownTimer.outPut;

                if (CountdownTimer.timeLeft < 11 && CountdownTimer.timeLeft > 0)
                {
                    beepSound.SoundBuffer = beepBuffer;
                    beepSound.Play();
                }
            }

            InputText.Text = timeStr;

            if (CountdownTimer.outPut == "00:00:00")
            {
                ResetTimer();
                Boom();
            }
        }

        private void ResetTimer()
        {
            timeStr = "00:00:00";
            InputText.Text = timeStr;
            timerStarted = false;
            inputStr.Clear();
            inputStr.Append("000000");
            CountdownTimer.outPut = "99:99:99";
        }
        #endregion

        #region EXPLOSION
        Form ExpWnd;
        System.Windows.Forms.Timer animTimer;
        readonly int frameSize = 64;
        SoundBuffer explodeBuffer = new SoundBuffer("sounds/c4_explode.wav");
        Sound explode = new Sound();
        private void Boom()
        {
            ExpWnd = new Form();

            ExpWnd.BackColor = Color.Black;
            ExpWnd.ClientSize = new Size(Screen.PrimaryScreen.Bounds.Height, Screen.PrimaryScreen.Bounds.Height);
            ExpWnd.Text = "BOOM";
            ExpWnd.FormBorderStyle = FormBorderStyle.None;
            ExpWnd.StartPosition = FormStartPosition.CenterScreen;
            ExpWnd.ShowInTaskbar = false;
            ExpWnd.TopMost = true;
            ExpWnd.TransparencyKey = Color.Black;

            animTimer = new();
            animTimer.Interval = 100;
            animTimer.Tick += AnimTick;
            animTimer.Start();

            ExpWnd.Show();

            explode.SoundBuffer = explodeBuffer;
            explode.Play();
        }

        int cFrame = 0;
        private void AnimTick(object sender, EventArgs e)
        {
            if (cFrame > 15)
            {
                cFrame = 0;
                animTimer.Stop();
                ExpWnd.Close();
                animTimer = null;
                ExpWnd = null;
                Show();
                notifyIcon.Visible = false;

                timeStr = timeStrSave;
                InputText.Text = timeStr;
                inputStr.Clear();
                inputStr.Append(inputStrSave);

                return;
            }

            Bitmap ExpSpr = Properties.Resources.explosionsheet;
            ExpWnd.BackgroundImage = Animate(ExpSpr, cFrame);
            ExpWnd.BackgroundImageLayout = ImageLayout.Zoom;

            cFrame++;
        }

        private Bitmap Animate(Bitmap source, int currentFrame)
        {
            Bitmap ExpSheet = new Bitmap(frameSize, frameSize);

            for (int y = 0; y < frameSize; y++)
            {
                for (int x = 0; x < frameSize; x++)
                {
                    ExpSheet.SetPixel(x, y, source.GetPixel(x + (currentFrame % 4) * frameSize, y + (currentFrame / 4) * frameSize));
                }
            }

            return ExpSheet;
        }
        #endregion

        #region Windows Form Designer generated code

        ComponentResourceManager rsrc;
        PrivateFontCollection fontCollection = new();
        private void InitializeComponent()
        {
            components = new Container();
            rsrc = new ComponentResourceManager(typeof(Wnd));
            c4img = new PictureBox();
            ctxMenuStrip = new ContextMenuStrip(components);
            ScaleOption = new ToolStripMenuItem();
            UpOption = new ToolStripMenuItem();
            DownOption = new ToolStripMenuItem();
            soundSelectOpt = new ToolStripMenuItem();
            moveoutOpt = new ToolStripMenuItem();
            bombplOpt = new ToolStripMenuItem();
            comgoOpt = new ToolStripMenuItem();
            letsGoOpt = new ToolStripMenuItem();
            locknloadOpt = new ToolStripMenuItem();
            DefuseOption = new ToolStripMenuItem();
            HideOption = new ToolStripMenuItem();
            QuitOption = new ToolStripMenuItem();
            HelpOption = new ToolStripMenuItem();
            InputText = new Label();
            ((ISupportInitialize)c4img).BeginInit();
            ctxMenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // c4img
            // 
            c4img.BackColor = Color.Transparent;
            c4img.ContextMenuStrip = ctxMenuStrip;
            c4img.Dock = DockStyle.Fill;
            c4img.Image = Properties.Resources.c4_render;
            c4img.Location = new Point(0, 0);
            c4img.Name = "c4img";
            //c4img.Size = new Size(350, 350);
            c4img.SizeMode = PictureBoxSizeMode.StretchImage;
            c4img.TabIndex = 0;
            c4img.TabStop = false;
            c4img.MouseDown += Wnd_MouseDown;
            c4img.MouseMove += Wnd_MouseMove;
            c4img.MouseUp += Wnd_MouseUp;
            // 
            // ctxMenuStrip
            // 
            ctxMenuStrip.ImageScalingSize = new Size(24, 24);
            ctxMenuStrip.Items.AddRange(new ToolStripItem[] { ScaleOption, soundSelectOpt, DefuseOption, HideOption, HelpOption, QuitOption });
            ctxMenuStrip.Name = "ctxMenuStrip";
            ctxMenuStrip.Size = new Size(241, 197);
            // 
            // ScaleOption
            // 
            ScaleOption.DropDownItems.AddRange(new ToolStripItem[] { UpOption, DownOption });
            ScaleOption.Name = "ScaleOption";
            ScaleOption.Size = new Size(240, 32);
            ScaleOption.Text = "Scale";
            // 
            // UpOption
            // 
            UpOption.Name = "UpOption";
            //UpOption.ShortcutKeys = Keys.Control | Keys.Oemplus;
            UpOption.ShortcutKeyDisplayString = "Ctrl+Plus";
            UpOption.Size = new Size(299, 34);
            UpOption.Text = "Up";
            UpOption.Click += UpOption_Click;
            // 
            // DownOption
            // 
            DownOption.Name = "DownOption";
            //DownOption.ShortcutKeys = Keys.Control | Keys.OemMinus;
            DownOption.ShortcutKeyDisplayString = "Ctrl+Minus";
            DownOption.Size = new Size(299, 34);
            DownOption.Text = "Down";
            DownOption.Click += DownOption_Click;
            // 
            // soundSelectOpt
            // 
            soundSelectOpt.DropDownItems.AddRange(new ToolStripItem[] { moveoutOpt, bombplOpt, comgoOpt, letsGoOpt, locknloadOpt });
            soundSelectOpt.Name = "soundSelectOpt";
            soundSelectOpt.Size = new Size(240, 32);
            soundSelectOpt.Text = "Select Sound";
            // 
            // moveoutOpt
            // 
            moveoutOpt.Name = "moveoutOpt";
            moveoutOpt.ShortcutKeys = Keys.Control | Keys.D0;
            moveoutOpt.Size = new Size(364, 34);
            moveoutOpt.Text = "Alright let's move out";
            moveoutOpt.Click += MoveoutOpt_Click;
            // 
            // bombplOpt
            // 
            bombplOpt.Name = "bombplOpt";
            bombplOpt.ShortcutKeys = Keys.Control | Keys.D1;
            bombplOpt.Size = new Size(364, 34);
            bombplOpt.Text = "Bomb has been planted";
            bombplOpt.Click += BombplOpt_Click;
            // 
            // comgoOpt
            // 
            comgoOpt.Name = "comgoOpt";
            comgoOpt.ShortcutKeys = Keys.Control | Keys.D2;
            comgoOpt.Size = new Size(364, 34);
            comgoOpt.Text = "Go go go!";
            comgoOpt.Click += ComgoOpt_Click;
            // 
            // letsGoOpt
            // 
            letsGoOpt.Name = "letsGoOpt";
            letsGoOpt.ShortcutKeys = Keys.Control | Keys.D3;
            letsGoOpt.Size = new Size(364, 34);
            letsGoOpt.Text = "Okay let's go!";
            letsGoOpt.Click += LetsGoOpt_Click;
            // 
            // locknloadOpt
            // 
            locknloadOpt.Name = "locknloadOpt";
            locknloadOpt.ShortcutKeys = Keys.Control | Keys.D4;
            locknloadOpt.Size = new Size(364, 34);
            locknloadOpt.Text = "Lock n' load";
            locknloadOpt.Click += LocknloadOpt_Click;
            // 
            // DefuseOption
            // 
            DefuseOption.Name = "DefuseOption";
            DefuseOption.ShortcutKeys = Keys.Control | Keys.R;
            DefuseOption.Size = new Size(240, 32);
            DefuseOption.Text = "Defuse";
            DefuseOption.Click += DefuseOption_Click;
            // 
            // HideOption
            // 
            HideOption.Name = "HideOption";
            HideOption.ShortcutKeys = Keys.Control | Keys.H;
            HideOption.Size = new Size(240, 32);
            HideOption.Text = "Hide";
            HideOption.Click += HideOption_Click;
            // 
            // QuitOption
            // 
            QuitOption.Name = "QuitOption";
            QuitOption.ShortcutKeys = Keys.Control | Keys.Q;
            QuitOption.Size = new Size(240, 32);
            QuitOption.Text = "Quit";
            QuitOption.Click += QuitOption_Click;
            // 
            // HelpOption
            // 
            HelpOption.Name = "HelpOption";
            HelpOption.Size = new Size(240, 32);
            HelpOption.Text = "Help";
            HelpOption.Click += HelpOption_Click;
            // 
            // InputText
            // 
            InputText.AutoSize = true;
            InputText.BackColor = Color.Transparent;
            fontCollection.AddFontFile("ProggyClean.ttf");
            InputText.Name = "InputText";
            InputText.Size = new Size(97, 20);
            InputText.TabIndex = 1;
            InputText.Text = "00:00:00";
            InputText.MouseDown += Wnd_MouseDown;
            InputText.MouseMove += Wnd_MouseMove;
            InputText.MouseUp += Wnd_MouseUp;
            // 
            // Wnd
            // 
            BackColor = Color.Gray;
            StartPosition = FormStartPosition.Manual;
            Controls.Add(InputText);
            Controls.Add(c4img);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)rsrc.GetObject("$this.Icon");
            KeyPreview = true;
            Name = "Wnd";
            Text = "C4";
            TransparencyKey = Color.Gray;
            Load += Wnd_Load;
            KeyDown += Wnd_KeyDown;
            Resize += Wnd_Resize;
            ((ISupportInitialize)c4img).EndInit();
            ctxMenuStrip.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox c4img;
        private Label InputText;
        private ContextMenuStrip ctxMenuStrip;
        private ToolStripMenuItem ScaleOption;
        private ToolStripMenuItem QuitOption;
        private ToolStripMenuItem UpOption;
        private ToolStripMenuItem DownOption;
        private ToolStripMenuItem DefuseOption;
        private ToolStripMenuItem HideOption;
        private ToolStripMenuItem soundSelectOpt;
        private ToolStripMenuItem moveoutOpt;
        private ToolStripMenuItem bombplOpt;
        private ToolStripMenuItem comgoOpt;
        private ToolStripMenuItem letsGoOpt;
        private ToolStripMenuItem locknloadOpt;
        private ToolStripMenuItem HelpOption;
    }
}
