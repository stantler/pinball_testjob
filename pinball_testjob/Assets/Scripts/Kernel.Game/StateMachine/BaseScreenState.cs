using System;
using GUI.Base;

namespace Kernel.Game.StateMachine
{
    public abstract class BaseScreenState<T> : IState where T : GUIComponentBase
    {
        protected T Screen;

        protected BaseScreenState()
        {
            Screen = _.GUIManager.InitializeComponent<T>();
            Screen.SetActive(false);
        }

        protected abstract void OnActivate(Action callback);
        protected abstract void OnDeactivate(Action callback);
        protected virtual void OnDispose() { }

        public void Activate(Action callback)
        {
            Screen.SetActive(true);
            OnActivate(callback);
        }

        public void Deactivate(Action callback)
        {
            OnDeactivate(callback);
            Screen.SetActive(false);
        }

        public void Dispose()
        {
            OnDispose();

            Screen.SetActive(false);
            Screen.Dispose();
            Screen = null;
        }
    }
}