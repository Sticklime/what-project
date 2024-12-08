using System.Collections.Generic;
using System.Linq;
using CodeBase.Data.StaticData;
using CodeBase.Infrastructure.Services.AssetProvider;
using CodeBase.Infrastructure.State;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.ConfigProvider
{
    public class ConfigProvider : IConfigProvider
    {
        private readonly IAssetProvider _assetProvider;
        private List<ScriptableObject> _staticData;

        public ConfigProvider(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public async UniTask Load()
        {
            _staticData = await _assetProvider.LoadAssetsByLabelAsync<ScriptableObject>("Configs");
        }

        public GameModeConfig GetGameMode(GameModeType gameModeType) =>
            GetListDataOfType<GameModeConfig>(_staticData).FirstOrDefault(x => x.GameModeType == gameModeType);

        public BuildingConfig GetBuilding(BuildingType buildingType) =>
            GetListDataOfType<BuildingConfig>(_staticData).FirstOrDefault(x => x.BuildingType == buildingType);

        public ServerConnectConfig GetServerConnectConfig() =>
            GetFirstDataOfType<ServerConnectConfig>(_staticData);

        private TData GetFirstDataOfType<TData>(List<ScriptableObject> allData)
        {
            TData firstData = default(TData);

            foreach (ScriptableObject data in allData)
            {
                if (data is TData dataOfType)
                {
                    firstData = dataOfType;
                    break;
                }
            }

            return firstData;
        }

        private List<TData> GetListDataOfType<TData>(List<ScriptableObject> allData)
        {
            List<TData> listData = new List<TData>();

            foreach (ScriptableObject data in allData)
            {
                if (data is TData dataOfType)
                    listData.Add(dataOfType);
            }

            return listData;
        }
    }
}