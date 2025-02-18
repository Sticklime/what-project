using Entitas;
using VContainer;

namespace CodeBase.Infrastructure.Factory
{
    public class SystemFactory : ISystemFactory
    {
        private readonly IObjectResolver _objectResolver;

        public SystemFactory(IObjectResolver objectResolver)
        {
            _objectResolver = objectResolver;
        }

        public ISystem CreateSystem<TSystem>() where TSystem : class, ISystem
        {
            IScopedObjectResolver scope =
                _objectResolver.CreateScope(builder => builder.Register<TSystem>(Lifetime.Singleton));

            return scope.Resolve<TSystem>();
        }
    }
}