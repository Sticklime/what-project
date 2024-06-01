using System.Collections.Generic;
using CodeBase.Data;

namespace CodeBase.Infrastructure.State
{
    public class LoadSaveState : IState
    {
        private readonly IStateMachine _stateMachine;
        private readonly IPersistentProgress _persistentProgress;

        public LoadSaveState(IGameStateMachine stateMachine, IPersistentProgress persistentProgress)
        {
            _stateMachine = stateMachine;
            _persistentProgress = persistentProgress;
        }

        public void Enter()
        {
            var resources = new Dictionary<ResourcesType, ResourceData>
            {
                { ResourcesType.Cristal, new ResourceData(10) }
            };

            _persistentProgress.Data = new PlayerData(resources);
            
            _stateMachine.Enter<LoadMapState>();
        }

        public void Exit()
        {
        }
    }
}