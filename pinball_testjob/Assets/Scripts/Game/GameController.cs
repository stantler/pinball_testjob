using System.Linq;
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

        private void Update()
        {
            if (_gameInstance == null)
            {
                return;
            }
            _gameInstance.Update();
        }

        private void StartGame(int settingsId)
        {
            _.GUIManager.LoadingScreen.StartLoading(() =>
            {
                _gameInstance = new GameInstance(this, settingsId, () =>
                {
                    _.GUIManager.LoadingScreen.FinishLoading(null);
                });
                _menuScreen.SetActive(false);
            });
        }

        public void EndGame()
        {
            _.GUIManager.LoadingScreen.StartLoading(() =>
            {
                _gameInstance.Dispose();
                _gameInstance = null;
                _menuScreen.SetActive(true);
                _.GUIManager.LoadingScreen.FinishLoading(null);
            });
        }

        public void Initialize()
        {
            _menuScreen = _.GUIManager.InitializeComponent<MainMenuScreen>();
            _menuScreen.Initialize(_.DataProvider.GameSettings.Select(s => s.Name));
            _menuScreen.OnStart += StartGame;
            _menuScreen.SetActive(true);

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