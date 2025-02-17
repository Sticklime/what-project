using CodeBase.Data.StaticData;
using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure.Services.ConfigProvider
{
    public interface IConfigProvider
    {
        UniTask Load();
        GameModeConfig GetGameMode(GameModeType gameModeType);
        BuildingConfig GetBuilding(BuildingType buildingType);
        SimpleUnitsConfigs GetSimpleUnitsConfig();
    }
}