using System.Threading.Tasks;

namespace CodeBase.Infrastructure.Bootstrapper.State
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}