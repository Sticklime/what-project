using CodeBase.Infrastructure.Bootstrapper.State;
using Zenject;

namespace CodeBase.Infrastructure.Bootstrapper.Factory
{
    public class StateFactory : IStateFactory
    {
        private readonly DiContainer _container;

        public StateFactory(DiContainer container)
        {
            _container = container;
        }

        public IExitableState CreateState<TState>() where TState : class, IExitableState => 
            _container.Instantiate<TState>();
    }
}