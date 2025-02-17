using _Scripts.Netcore.Data.ConnectionData;
using _Scripts.Netcore.NetworkComponents.RPCComponents;
using _Scripts.Netcore.RPCSystem;
using _Scripts.Netcore.Runner;
using _Scripts.Netcore.Spawner;
using CodeBase.Infrastructure.Services.ConfigProvider;
using CodeBase.Infrastructure.Services.SceneLoader;

namespace CodeBase.Infrastructure.State
{
    public class StartServerState : NetworkService , IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IConfigProvider _configProvider;
        private readonly INetworkRunner _networkRunner;
        private readonly ISceneLoader _sceneLoader;
        private readonly INetworkSpawner _networkSpawner;

        private const string NameScene = "MapScene";
        private int _sessionIndex;

        public StartServerState(IGameStateMachine stateMachine,
            IConfigProvider configProvider,
            INetworkRunner networkRunner,
            ISceneLoader sceneLoader,
            INetworkSpawner networkSpawner)
        {
            _gameStateMachine = stateMachine;
            _configProvider = configProvider;
            _networkRunner = networkRunner;
            _sceneLoader = sceneLoader;
            _networkSpawner = networkSpawner;

            RPCInvoker.RegisterRPCInstance<StartServerState>(this);
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
            
            await _sceneLoader.Load(NameScene);
            _gameStateMachine.Enter<BootSystemState>();
        }

        public void Exit()
        {
        }
    }
}
