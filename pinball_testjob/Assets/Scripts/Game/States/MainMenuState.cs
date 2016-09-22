using System;
using GUI.MainMenu;
using Helpers.Extension;
using Kernel.Game.StateMachine;

namespace Game.States
{
    [State(StateType.MainMenu)]
    public class MainMenuState : BaseScreenState<MainMenuScreen>
    {
        protected override void OnActivate(Action callback)
        {
            DataProvider.DataProvider.OnDataLoading += Screen.SetProgress;
            callback.SafeInvoke();
        }

        protected override void OnDeactivate(Action callback)
        {
            DataProvider.DataProvider.OnDataLoading -= Screen.SetProgress;
            callback.SafeInvoke();
        }

        protected override void OnDispose()
        {

        }
    }
}