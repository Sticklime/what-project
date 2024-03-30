using CodeBase.Infrastructure.Services.SceneLoader;
using UnityEngine;

namespace CodeBase.Infrastructure.Bootstrapper.State
{
    public class LoadMapState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;

        public LoadMapState(IGameStateMachine stateMachine, ISceneLoader sceneLoader, IGameFactory gameFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
        }

        public async void Enter()
        {
            await _sceneLoader.Load("MapScene");

            InitCamera();
        }

        public void Exit()
        {
            
        }

        private void InitCamera() => 
            _gameFactory.CreateEntityCamera(Camera.main);
    }
}