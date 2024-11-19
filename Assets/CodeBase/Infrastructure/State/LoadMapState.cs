using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.Infrastructure.Services.SceneLoader;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services.ConfigProvider;
using Fusion;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeBase.Infrastructure.State
{
    public class LoadMapState : IPayLoadState
    {
        private const string NameScene = "MapScene";

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

        private PlayerRef _playerRef;
        
        public LoadMapState(IGameStateMachine stateMachine, ISceneLoader sceneLoader, IGameFactory gameFactory,
            IUIFactory uiFactory, IPersistentProgress persistentProgress, IConfigProvider configProvider)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
            _persistentProgress = persistentProgress;
            _configProvider = configProvider;
        }

        public async void Enter(object data)
        {
            await _sceneLoader.Load(NameScene);
            _playerRef = (PlayerRef)data;

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
            _rootHud = _uiFactory.CreateHud();
            _buttonBuild = _uiFactory.CreateBuildButton();
            _resourceContainer = _uiFactory.CreateResourceContainer();

            _rootHud.Add(_buttonBuild);

            foreach (ResourceData resourceData in _persistentProgress.Data.ResourceData.Values)
                _resourceLabels.Add(_uiFactory.CreateResourceLabel(resourceData));

            foreach (VisualElement resourcesLabel in _resourceLabels)
                _resourceContainer.Q<VisualElement>("GroupBoxResources").Add(resourcesLabel);

            _rootHud.Add(_resourceContainer);
        }

        private void InitCharacters()
        {
            _gameFactory.CreateUnit(Vector3.zero, _playerRef);
            _gameFactory.CreateUnit(Vector3.zero, _playerRef);
            _gameFactory.CreateUnit(Vector3.zero, _playerRef);
            _gameFactory.CreateUnit(Vector3.zero, _playerRef);
            _gameFactory.CreateUnit(Vector3.zero, _playerRef);
        }

        private void InitCamera() =>
            _gameFactory.CreateEntityCamera(Camera.main);
    }
}