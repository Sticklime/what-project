using CodeBase.Infrastructure.Services.AssetProvider;
using Cysharp.Threading.Tasks;

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