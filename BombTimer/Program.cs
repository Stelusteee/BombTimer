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

// Don't worry if it looks stuck: setup and updatedownloader [DONE]
// Don't override the save file [DONE]
// Valve themed UI [DONE]
// Never ask update again [CANCELED]
// Uninstaller
// Project timer
