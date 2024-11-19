using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure.State
{
    public interface IState : IExitableState
    {
        void Enter();
    }

    public interface IPayLoadState : IExitableState
    {
        void Enter(object data);
    } 
    
    public interface IExitableState
    {
        void Exit();
    }
}