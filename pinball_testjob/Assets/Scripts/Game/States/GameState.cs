using System;
using Helpers.Extension;
using Kernel.Game.StateMachine;

namespace Game.States
{
    [State(StateType.Game)]
    public class GameState : IState
    {
        //public ScenarioController ScenarioController { get; private set; }

        public void Activate(Action callback)
        {
            //ScenarioController = new ScenarioController(callback);
        }

        public void Deactivate(Action callback)
        {
            //ScenarioController.Dispose();
            callback.SafeInvoke();
        }

        public void Dispose()
        {
            
        }
    }
}