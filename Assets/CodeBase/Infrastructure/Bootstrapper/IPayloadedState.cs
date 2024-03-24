using CodeBase.Infrastructure.States;

namespace CodeBase.Infrastructure.Bootstrapper
{
    public interface IPayloadedState<TPayload> : IExitableState
    {
        void Enter(TPayload payload);
    }
}