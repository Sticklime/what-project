using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.Infrastructure.Services.SceneLoader;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services.ConfigProvider;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeBase.Infrastructure.State
{
    public class LoadMapState : IState
    {
        private readonly IPersistentProgress _persistentProgress;
        private readonly IConfigProvider _configProvider;
        private readonly IGameStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;

        public LoadMapState(IGameStateMachine stateMachine, IGameFactory gameFactory,
            IUIFactory uiFactory, IPersistentProgress persistentProgress, IConfigProvider configProvider,
            ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
            _stateMachine = stateMachine;
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
            _persistentProgress = persistentProgress;
            _configProvider = configProvider;
        }

        public async void Enter()
        {
            await _sceneLoader.Load("MapScene");
            InitScene();
        }

        public void Exit()
        {
        }

        private void InitScene()
        {
            InitCamera();
            InitCharacters();
            InitHud();
        }

        private void InitHud()
        {
        }

        private void InitCharacters()
        {
            _gameFactory.CreateUnit(Vector3.zero);
            _gameFactory.CreateUnit(Vector3.zero);
            _gameFactory.CreateUnit(Vector3.zero);
            _gameFactory.CreateUnit(Vector3.zero);
            _gameFactory.CreateUnit(Vector3.zero);
        }

        private void InitCamera()
        {
            _gameFactory.CreateEntityCamera(Camera.main);
        }
    }
}