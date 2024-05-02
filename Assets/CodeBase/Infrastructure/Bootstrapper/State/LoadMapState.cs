using CodeBase.Infrastructure.Bootstrapper.Factory;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services.SceneLoader;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeBase.Infrastructure.Bootstrapper.State
{
    public class LoadMapState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;
        private readonly Contexts _context;

        private VisualElement _buildButton;
        private VisualElement _rootHud;

        public LoadMapState(IGameStateMachine stateMachine, ISceneLoader sceneLoader, IGameFactory gameFactory,
            IUIFactory uiFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
            _context = Contexts.sharedInstance;
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
        }

        private void InitUI()
        {
            _rootHud = _uiFactory.CreateRootHud();
            _buildButton = _uiFactory.CreateBuildButton();

            _rootHud.Add(_buildButton);
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