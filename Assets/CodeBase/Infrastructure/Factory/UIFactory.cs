using System.Threading.Tasks;
using CodeBase.Data;
using CodeBase.Data.StaticData;
using CodeBase.Domain.BuildingSystem;
using CodeBase.Infrastructure.Services.AssetProvider;
using CodeBase.Infrastructure.Services.ConfigProvider;
using CodeBase.UserInterface.ViewModel;
using Cysharp.Threading.Tasks;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;
using BindingId = UnityEngine.UIElements.BindingId;

namespace CodeBase.Infrastructure.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly DiContainer _container;
        private readonly IAssetProvider _assetProvider;
        private readonly BuildingOperation _buildingOperation;
        private readonly IConfigProvider _configProvider;

        private GameObject _uiDocument;
        private VisualTreeAsset _buildButtonPrefab;
        private VisualTreeAsset _resourceContainer;
        private VisualTreeAsset _resourceLabel;

        public UIFactory(DiContainer container, IAssetProvider assetProvider, BuildingOperation buildingOperation,
            IConfigProvider configProvider)
        {
            _container = container;
            _assetProvider = assetProvider;
            _buildingOperation = buildingOperation;
            _configProvider = configProvider;
        }

        public async UniTask Load()
        {
            _uiDocument = await _assetProvider.LoadAsync<GameObject>("UIDocument");
            _buildButtonPrefab = await _assetProvider.LoadAsync<VisualTreeAsset>("BuildButton");
            _resourceContainer = await _assetProvider.LoadAsync<VisualTreeAsset>("GroupBoxResources");
            _resourceLabel = await _assetProvider.LoadAsync<VisualTreeAsset>("ResourceLabel");
        }

        public VisualElement CreateHud()
        {
            VisualElement rootVisualElement = CreateUiDocument();
            rootVisualElement.Q<VisualElement>("Hud-Container");

            return rootVisualElement;
        }

        public VisualElement CreateResourceContainer() =>
            _resourceContainer.CloneTree();

        public VisualElement CreateResourceLabel(ResourceData resourceData)
        {
            var resourceLabel = _resourceLabel.Instantiate();
            ResourceViewModel resourceViewModel = new ResourceViewModel(resourceData);

            BindingModelView(resourceLabel.Q<Label>("Value"), nameof(Label.text), resourceViewModel,
                resourceViewModel.CountResource);

            return resourceLabel;
        }

        public VisualElement CreateBuildButton()
        {
            var buildButton = _buildButtonPrefab.Instantiate();

            BuildPlanViewModel buildPlan = new BuildPlanViewModel(_buildingOperation, _configProvider, BuildingType.Barrack);
            Button barracksButton = buildButton.Q<Button>("Barracks");

            barracksButton.clicked += buildPlan.CreateBuildPlan;
            BindingModelView(buildButton, nameof(Label.text), buildPlan, buildPlan.NameButton);

            return buildButton;
        }

        private VisualElement CreateUiDocument()
        {
            GameObject uiDocumentInstance = Object.Instantiate(_uiDocument);
            var rootVisualElement = uiDocumentInstance.GetComponent<UIDocument>().rootVisualElement;

            return rootVisualElement;
        }

        private void BindingModelView<TModel, TValue>(VisualElement visualElement, BindingId bindingPath, TModel modelView, TValue bindProperty) where TModel : class 
        {
            visualElement.SetBinding(bindingPath, new DataBinding()
            {
                dataSource = modelView,
                dataSourcePath = new PropertyPath(nameof(bindProperty)),
            });
        }
    }

    public interface IUIFactory
    {
        UniTask Load();
        VisualElement CreateResourceContainer();
        VisualElement CreateResourceLabel(ResourceData resourceData);
        VisualElement CreateBuildButton();
        VisualElement CreateHud();
    }
}