using System.Reflection;
using Kernel.Game.AttributeFactory;

namespace Kernel.Game.StateMachine
{
    public class StateMachine : BaseStateMachine<StateType>
    {
        private readonly BaseElementFactory<StateType, StateAttribute, IState> _elementFactory = new BaseElementFactory<StateType, StateAttribute, IState>(Assembly.GetExecutingAssembly());

        public StateMachine(StateType firstState) : base(firstState)
        {
        }

        protected override IState GetInstance(StateType state, params object[] args)
        {
            return _elementFactory.GetFactoryElement(state, args);
        }
    }
}