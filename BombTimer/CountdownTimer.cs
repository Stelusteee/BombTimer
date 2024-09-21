using System.Text;

namespace BombTimer
{
    public static class CountdownTimer
    {
        public static System.Windows.Forms.Timer? timer;
        public static int timeLeft;
        public static string outPut = "99:99:99";

        public static void Entry(StringBuilder inputStr)
        {
            // Seperate
            string hours = inputStr.ToString(0, 2);
            string minutes = inputStr.ToString(2, 2);
            string seconds = inputStr.ToString(4, 2);

            // Normalize
            int h = int.Parse(hours);
            int m = int.Parse(minutes);
            int s = int.Parse(seconds);
            h = h > 59 ? 59 : h;
            m = m > 59 ? 59 : m;
            s = s > 59 ? 59 : s;

            // Format
            StartCountdown(h * 3600 + m * 60 + s);
        }

        private static void StartCountdown(int inputTime)
        {
            timeLeft = inputTime;

            timer = new() { Interval = 1000 };
            timer.Tick += Timer_Tick;

            timer.Start();
        }

        public delegate void OutPutEventHandler();
        public static event OutPutEventHandler? OutPutEvent;
        private static void Timer_Tick(object? sender, EventArgs e)
        {
            timeLeft--;

            int s = timeLeft % 60;
            int m = (timeLeft / 60) % 60;
            int h = timeLeft / 3600;

            outPut = $"{h:D2}:{m:D2}:{s:D2}";
            OutPutEvent?.Invoke();

            if (timeLeft <= 0)
            {
                timer?.Stop();
            }
        }
    }
}