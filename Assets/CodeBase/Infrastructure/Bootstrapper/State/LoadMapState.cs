using CodeBase.Infrastructure.Services.SceneLoader;

namespace CodeBase.Infrastructure.Bootstrapper.State
{
    public class LoadMapState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;

        public LoadMapState(IGameStateMachine stateMachine ,ISceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public async void Enter()
        {
            await _sceneLoader.Load("MapScene");
        }

        public void Exit()
        {
        }
    }
}