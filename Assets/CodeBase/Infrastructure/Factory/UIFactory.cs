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
using VContainer;
using BindingId = UnityEngine.UIElements.BindingId;

namespace CodeBase.Infrastructure.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assetProvider;

        public UIFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public async UniTask Load()
        {
       
        }
    }

    public interface IUIFactory
    {
        UniTask Load();
    }
}