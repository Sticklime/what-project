using CodeBase.Infrastructure.States;

namespace CodeBase.Infrastructure.Bootstrapper
{
    public interface IStateFactory
    {
        IExitableState CreateState<TState>() where TState : class, IExitableState;
    }
}