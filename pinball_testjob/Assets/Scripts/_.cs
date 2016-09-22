using Audio;
using Game;
using GUI;

public static class _
{
    public static AudioManager AudioManager { get; private set; }
    public static GUIManager GUIManager { get; private set; }

    public static GameController GameController { get; private set; }
    public static DataProvider.DataProvider DataProvider { get; private set; }
}