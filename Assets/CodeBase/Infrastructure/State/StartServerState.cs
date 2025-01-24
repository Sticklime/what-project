using _Scripts.Netcore.Data.Attributes;
using _Scripts.Netcore.Data.ConnectionData;
using _Scripts.Netcore.NetworkComponents.NetworkVariableComponent;
using _Scripts.Netcore.NetworkComponents.RPCComponents;
using _Scripts.Netcore.RPCSystem;
using _Scripts.Netcore.RPCSystem.ProcessorsData;
using _Scripts.Netcore.Runner;
using CodeBase.Infrastructure.Services.ConfigProvider;
using CodeBase.Infrastructure.Services.SceneLoader;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.State
{
    public class StartServerState : NetworkService , IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IConfigProvider _configProvider;
        private readonly INetworkRunner _networkRunner;
        private readonly ISceneLoader _sceneLoader;

        private const string NameScene = "MapScene";
        private int _sessionIndex;

        public StartServerState(IGameStateMachine stateMachine,
            IConfigProvider configProvider,
            INetworkRunner networkRunner,
            ISceneLoader sceneLoader)
        {
            _gameStateMachine = stateMachine;
            _configProvider = configProvider;
            _networkRunner = networkRunner;
            _sceneLoader = sceneLoader;

            RPCInvoker.RegisterRPCInstance<StartServerState>(this);
            _networkRunner.OnPlayerConnected += SendData;
        }

        public async void Enter()
        {
            ConnectServerData serverData = new()
            {
                MaxClients = 2,
                TcpPort = 5055,
                UdpPort = 5057
            };
            
            await _networkRunner.StartServer(serverData);
        }
        
        private async void SendData(int playerId)
        {
            await UniTask.WaitForSeconds(5);
            await _sceneLoader.Load(NameScene);
            _gameStateMachine.Enter<BootSystemState>();
        }
        
        public void Exit()
        {
        }
    }
}
