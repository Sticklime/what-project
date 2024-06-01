using CodeBase.Infrastructure.State;

namespace CodeBase.Infrastructure.Factory
{
    public interface IStateFactory
    {
        IExitableState CreateState<TState>() where TState : class, IExitableState;
    }
}