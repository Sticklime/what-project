using System.Net;
using _Scripts.Netcore.Data.Attributes;
using _Scripts.Netcore.Data.ConnectionData;
using _Scripts.Netcore.NetworkComponents.RPCComponents;
using _Scripts.Netcore.RPCSystem;
using _Scripts.Netcore.Runner;
using CodeBase.Data.StaticData;
using CodeBase.Infrastructure.Services.ConfigProvider;
using CodeBase.Infrastructure.Services.SceneLoader;
using UnityEngine;

namespace CodeBase.Infrastructure.State
{
    public class ConnectToServer : NetworkService ,IState
    {
        private const string NameScene = "MapScene";
        
        private ServerConnectConfig _serverConnectConfig;
        private readonly IConfigProvider _configProvider;
        private readonly INetworkRunner _runner;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ISceneLoader _sceneLoader;

        private int _sessionIndex;
        
        public ConnectToServer(IConfigProvider configProvider,
            INetworkRunner runner,
            IGameStateMachine gameStateMachine,
            ISceneLoader sceneLoader)
        {
            _configProvider = configProvider;
            _runner = runner;
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;

            RPCInvoker.RegisterRPCInstance<ConnectToServer>(this);
        }

        public async void Enter()
        {
            IPAddress.TryParse("127.0.0.1", out IPAddress ipAddress);
            
            ConnectClientData clientData = new ()
            {
                Ip = ipAddress,
                TcpPort = 5055,
                UdpPort = 5056
            };
            
            await _runner.StartClient(clientData);
            await _sceneLoader.Load(NameScene);
        }

        public void Exit()
        {
        }
    }
}