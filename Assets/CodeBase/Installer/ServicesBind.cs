using CodeBase.Infrastructure.Services.SceneLoader;
using Zenject;

namespace CodeBase.Zenject
{
    public class ServicesBind : MonoInstaller, ICoroutineRunner
    {
        public override void InstallBindings()
        {
            SceneLoaderBind();
        }

        private void SceneLoaderBind()
        {
            var sceneLoader = new SceneLoader(this);

            Container
                .Bind<ISceneLoader>()
                .FromInstance(sceneLoader)
                .AsSingle();
        }
    }
}