using System;
using Kernel.Game.AttributeFactory;

namespace Kernel.Game.StateMachine
{
    public class StateAttribute : Attribute, IFactoryAttribute<StateType>
    {
        public StateType Code { get; private set; }

        public StateAttribute(StateType code)
        {
            Code = code;
        }
    }
}