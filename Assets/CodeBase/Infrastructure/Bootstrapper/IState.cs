using CodeBase.Infrastructure.States;

namespace CodeBase.Infrastructure.Bootstrapper
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}