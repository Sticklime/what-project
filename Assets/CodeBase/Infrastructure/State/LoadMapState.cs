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

        private readonly List<VisualElement> _resourceLabels = new List<VisualElement>();
        private VisualElement _rootHud;
        private VisualElement _buttonBuild;
        private VisualElement _resourceContainer;
        private readonly ConnectServerState _connectServerState;

        public LoadMapState(IGameStateMachine stateMachine, IGameFactory gameFactory,
            IUIFactory uiFactory, IPersistentProgress persistentProgress, IConfigProvider configProvider)
        {
            _stateMachine = stateMachine;
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
            _persistentProgress = persistentProgress;
            _configProvider = configProvider;
        }

        public void Enter()
        {
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

        private void InitCamera() =>
            _gameFactory.CreateEntityCamera(Camera.main);
    }
}