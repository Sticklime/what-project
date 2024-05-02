using System.Threading.Tasks;
using CodeBase.Infrastructure.Services.AssetManager;
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

        public UIFactory(DiContainer container, IAssetProvider assetProvider)
        {
            _container = container;
            _assetProvider = assetProvider;
        }

        public async Task Load()
        {
            _uiDocument = await _assetProvider.LoadAsync<GameObject>("UIDocument");
            _buildButton = await _assetProvider.LoadAsync<VisualTreeAsset>("BuildButton");
        }

        public VisualElement CreateRootHud()
        {
            VisualElement rootVisualElement = CreateUiDocument();
            rootVisualElement.Q<VisualElement>("Hud-Container");

            return rootVisualElement;
        }

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
        Task Load();
        VisualElement CreateBuildButton();
        VisualElement CreateRootHud();
    }
}