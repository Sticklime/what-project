using System.Threading.Tasks;
using CodeBase.Infrastructure.Services.AssetProvider;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace CodeBase.Infrastructure.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly DiContainer _container;
        private readonly IAssetProvider _assetProvider;

        private GameObject _uiDocument;
        private VisualTreeAsset _buildButton;
        private VisualTreeAsset _resourceContainer;
        private VisualTreeAsset _resourceLabel;

        public UIFactory(DiContainer container, IAssetProvider assetProvider)
        {
            _container = container;
            _assetProvider = assetProvider;
        }

        public async UniTask Load()
        {
            _uiDocument = await _assetProvider.LoadAsync<GameObject>("UIDocument");
            _buildButton = await _assetProvider.LoadAsync<VisualTreeAsset>("BuildButton");
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

        public VisualElement CreateResourceLabel() =>
            _resourceLabel.CloneTree();

        public VisualElement CreateBuildButton() =>
            _buildButton.CloneTree();

        private VisualElement CreateUiDocument()
        {
            GameObject uiDocumentInstance = Object.Instantiate(_uiDocument);
            var rootVisualElement = uiDocumentInstance.GetComponent<UIDocument>().rootVisualElement;

            return rootVisualElement;
        }
    }

    public interface IUIFactory
    {
        UniTask Load();
        VisualElement CreateResourceContainer();
        VisualElement CreateResourceLabel();
        VisualElement CreateBuildButton();
        VisualElement CreateHud();
    }
}