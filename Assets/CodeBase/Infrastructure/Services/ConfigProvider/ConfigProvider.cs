using System.Collections.Generic;
using System.Linq;
using CodeBase.Data.StaticData;
using CodeBase.Infrastructure.Services.AssetProvider;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
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

        public GameModeData GetGameModeData(GameModeType gameModeType) =>
            GetListDataOfType<GameModeData>(_staticData).FirstOrDefault(x => x.GameModeType == gameModeType);

        public BuildingData GetBuildingData(BuildingType buildingType) =>
            GetListDataOfType<BuildingData>(_staticData).FirstOrDefault(x => x.BuildingType == buildingType);

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