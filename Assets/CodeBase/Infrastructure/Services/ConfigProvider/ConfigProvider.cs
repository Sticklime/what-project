using CodeBase.Data.StaticData;
using CodeBase.Infrastructure.Services.AssetProvider;

namespace CodeBase.Infrastructure.Services.ConfigProvider
{
    public class ConfigProvider : IConfigProvider
    {
        private readonly IAssetProvider _assetProvider;

        public ConfigProvider(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }
        
        public void Load()
        {
            _assetProvider.InitializeAsset();

        }

        public GameModeData GetGameModeData()
        {
            
        }
    }

    public interface IConfigProvider
    {
        public void Load();
    }
}