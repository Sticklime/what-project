using CodeBase.Data.StaticData;
using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure.Services.ConfigProvider
{
    public interface IConfigProvider
    {
        UniTask Load();
        GameModeData GetGameModeData(GameModeType gameModeType);
        BuildingData GetBuildingData(BuildingType buildingType);
    }
}