using System;
using System.Collections.Generic;

namespace Kernel.Game.StateMachine
{
    public abstract class BaseStateMachine<T> : IDisposable
    {
        private readonly Dictionary<T, IState> _statesByType = new Dictionary<T, IState>();

        protected abstract IState GetInstance(T state, params object[] args);

        public T CurrentState { get; private set; }

        public BaseStateMachine(T firstState, params object[] args)
        {
            var states = Enum.GetValues(typeof(T));
            foreach (T s in states)
            {
                _statesByType.Add(s, GetInstance(s, args));
            }

            //_.GUIManager.LoadingScreen.StartLoading(() =>
            //{
                CurrentState = firstState;
                //_statesByType[CurrentState].Activate(() => { _.GUIManager.LoadingScreen.FinishLoading(null); });
            _statesByType[CurrentState].Activate(null);
            //});
        }

        public void SetState(T state, Action callback)
        {
            if (CurrentState.Equals(state))
            {
                return;
            }

            _.GUIManager.LoadingScreen.StartLoading(() =>
            {
                _statesByType[CurrentState].Deactivate(() =>
                {
                    CurrentState = state;
                    _statesByType[CurrentState].Activate(() =>
                    {
                        _.GUIManager.LoadingScreen.FinishLoading(callback);
                    });
                });
            });
        }

        public TState GetStateByType<TState>(T stateType) where TState : IState
        {
            return (TState)_statesByType[stateType];
        }

        public void Dispose()
        {
            foreach (var s in _statesByType.Values)
            {
                s.Dispose();
            }
        }
    }
}
