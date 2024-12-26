using System.Threading.Tasks;
using CodeBase.Data.StaticData;
using CodeBase.Infrastructure.Services.ConfigProvider;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.State
{
    public class StartServerState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IConfigProvider _configProvider;

        private ServerConnectConfig _serverConnectConfig;

        private const string NameScene = "MapScene";
        private int _sessionIndex;

        public StartServerState(IGameStateMachine stateMachine, IConfigProvider configProvider)
        {
            _gameStateMachine = stateMachine;
            _configProvider = configProvider;
        }

        public async void Enter()
        {
            _serverConnectConfig = _configProvider.GetServerConnectConfig();

            
        }

        public void Exit()
        {
        }
        
        private string GetSessionName()
        {
            Debug.Log($"{_serverConnectConfig.SessionName}_{_sessionIndex}");
            return $"{_serverConnectConfig.SessionName}_{_sessionIndex}";
        }
    }
}