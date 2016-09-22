using System;

namespace Kernel.Game.StateMachine
{
    public interface IState
    {
        void Activate(Action callback);
        void Deactivate(Action callback);
        void Dispose();
    }
}