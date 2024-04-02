namespace CodeBase.Infrastructure.Bootstrapper.State
{
    public interface IPayloadedState<TPayload> : IExitableState
    {
        void Enter(TPayload payload);
    }
}