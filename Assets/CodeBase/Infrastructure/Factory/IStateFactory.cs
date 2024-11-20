using CodeBase.Infrastructure.State;

namespace CodeBase.Infrastructure.Factory
{
    public interface IStateFactory
    {
        IExitableState CreateSystem<TState>() where TState : class, IExitableState;
    }
}