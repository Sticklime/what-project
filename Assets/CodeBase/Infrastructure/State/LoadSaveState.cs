using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.Data.StaticData;
using CodeBase.Infrastructure.Services.ConfigProvider;
using Fusion;

namespace CodeBase.Infrastructure.State
{
    public class LoadSaveState : IPayLoadState
    {
        private readonly IStateMachine _stateMachine;
        private readonly IPersistentProgress _persistentProgress;
        private readonly IConfigProvider _configProvider;

        public LoadSaveState(IGameStateMachine stateMachine, IPersistentProgress persistentProgress,
            IConfigProvider configProvider)
        {
            _stateMachine = stateMachine;
            _persistentProgress = persistentProgress;
            _configProvider = configProvider;
        }

        public void Enter(object data)
        {
            var resources = new Dictionary<ResourcesType, ResourceData>();

            GameModeData resourcesStaticData = _configProvider.GetGameModeData(GameModeType.Default);

            foreach (var resourcesData in resourcesStaticData.Resources) 
                resources.Add(resourcesData.ResourcesType, new ResourceData(resourcesData.ValueResources));

            _persistentProgress.Data = new PlayerData(resources);

            _stateMachine.Enter<LoadMapState>(data);
        }

        public void Exit()
        {
        }
    }
}