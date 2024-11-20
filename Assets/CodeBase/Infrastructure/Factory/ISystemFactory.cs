using Entitas; 

namespace CodeBase.Infrastructure.Factory
{
    public interface ISystemFactory
    {
        ISystem CreateSystem<TSystem>() where TSystem : class, ISystem;
    }
}