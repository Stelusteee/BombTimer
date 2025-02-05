namespace BombTimer;

public static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new Wnd());
    }
}

// Uninstaller
// Project timer
// Switch to Google Cloud API
// Close the issue
// Add presets like 1 hour or half an hour to f1 f2 or smth
