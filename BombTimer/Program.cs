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
