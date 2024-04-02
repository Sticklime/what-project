using CodeBase.Infrastructure.Bootstrapper.State;

namespace CodeBase.Infrastructure.Bootstrapper.Factory
{
    public interface IStateFactory
    {
        IExitableState CreateState<TState>() where TState : class, IExitableState;
    }
}