using Audio;
using DataProvider;
using DataProvider.Entries;
using Helpers.Modules;
using Kernel.Game.StateMachine;
using UnityEngine;

namespace Game
{
    public class GameController : MonoBehaviour, IModule
    {
        private StateMachine _stateMashine;

        public bool IsInitialized { get; set; }

        public Session.Session CurrentSession { get; private set; }

        public void Initialize()
        {
            _stateMashine = new StateMachine(StateType.MainMenu);
            IsInitialized = true;
        }

        public void StartGame()
        {
            _stateMashine.SetState(StateType.ScenarioSelect, null);
            _.AudioManager.Play(MusicType.GamePlay);
        }

        public void StartSession(ScenarioEntry scenarioEntry)
        {
            CurrentSession = new Session.Session(scenarioEntry);
            _stateMashine.SetState(StateType.Game, null);
        }

        public void RestoreSession()
        {
            CurrentSession = SaveController.Load();
            _stateMashine.SetState(StateType.Game, null);
        }

        public void Dispose()
        {
            IsInitialized = false;
        }
    }
}