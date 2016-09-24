using GUI.MainMenu;
using Helpers.Modules;
using UnityEngine;

namespace Game
{
    public class GameController : MonoBehaviour, IModule
    {
        private MainMenuScreen _menuScreen;

        private GameInstance _gameInstance;

        public bool IsInitialized { get; set; }

        private void StartGame()
        {
            _gameInstance = new GameInstance();
        }

        public void Initialize()
        {
            _menuScreen = _.GUIManager.InitializeComponent<MainMenuScreen>();
            _menuScreen.OnStart += StartGame;

            IsInitialized = true;
        }

        public void Dispose()
        {
            _menuScreen.OnStart -= StartGame;
            _.GUIManager.DisposeScreen(_menuScreen);

            IsInitialized = false;
        }
    }
}