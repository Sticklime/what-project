using CodeBase.Data;
using CodeBase.Infrastructure.Services.SceneLoader;
using CodeBase.Infrastructure.Factory;
using CodeBase.UserInterface.ViewModel;
using Unity.Properties;
using UnityEngine.UIElements;
using UnityEngine;

namespace CodeBase.Infrastructure.State
{
    public class LoadMapState : IState
    {
        private const string NameScene = "MapScene";

        private readonly IPersistentProgress _persistentProgress;
        private readonly IGameStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;

        private VisualElement _rootHud;
        private VisualElement _buttonBuild;
        private VisualElement _resourceContainer;

        public LoadMapState(IGameStateMachine stateMachine, ISceneLoader sceneLoader, IGameFactory gameFactory,
            IUIFactory uiFactory, IPersistentProgress persistentProgress)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
            _persistentProgress = persistentProgress;
        }

        public async void Enter()
        {
            await _sceneLoader.Load(NameScene);

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
            BindingUI();
        }

        private void InitUI()
        {
            _rootHud = _uiFactory.CreateHud();
            _buttonBuild = _uiFactory.CreateBuildButton();
            _resourceContainer = _uiFactory.CreateResourceContainer();

            _rootHud.Add(_buttonBuild);

            foreach (var resourceData in _persistentProgress.Data.ResourceData)
                _resourceContainer.Q<VisualElement>("GroupBoxResources").Add(_uiFactory.CreateResourceLabel());

            _rootHud.Add(_resourceContainer);
        }

        private void BindingUI()
        {
            BuildPlanViewModel buildPlan = new BuildPlanViewModel(_gameFactory, "Barracks");
            Button barracksButton = _buttonBuild.Q<Button>("Barracks");
            
            barracksButton.clicked += buildPlan.CreateBuildPlan;
            
            barracksButton.SetBinding(nameof(Label.text), new DataBinding
            {
                dataSource = buildPlan,
                dataSourcePath = new PropertyPath(nameof(BuildPlanViewModel.NameButton)),
                updateTrigger = BindingUpdateTrigger.WhenDirty,
                bindingMode = BindingMode.TwoWay,
            });
            
            //ResourceViewModel resource = new ResourceViewModel()
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