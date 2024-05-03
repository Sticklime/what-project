using System.Collections.Generic;
using CodeBase.Infrastructure.Bootstrapper.State;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services.PresenterLocator;
using CodeBase.Infrastructure.Services.SceneLoader;
using CodeBase.Presenters;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeBase.Infrastructure.State
{
    public class LoadMapState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IPresenterLocator _presenterLocator;
        private readonly ISceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;

        private VisualElement _rootHud;
        private VisualElement _buttonBuild;

        public LoadMapState(IGameStateMachine stateMachine, ISceneLoader sceneLoader, IGameFactory gameFactory,
            IUIFactory uiFactory, IPresenterLocator presenterLocator)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
            _presenterLocator = presenterLocator;
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
            InitUI();
            RegisterPresenter();
        }

        private void InitUI()
        {
            _rootHud = _uiFactory.CreateRootHud();
            _buttonBuild = _uiFactory.CreateBuildButton();

            _rootHud.Add(_buttonBuild);
        }

        private void RegisterPresenter()
        {
            _presenterLocator.RegisterPresenter(new ButtonBuildPresenter(_buttonBuild));
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