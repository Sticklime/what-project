using System.Net.Sockets;
using CodeBase.Infrastructure.Services.ConfigProvider;
using CodeBase.Network.Attributes;
using CodeBase.Network.Data.ConnectionData;
using CodeBase.Network.NetworkComponents.NetworkVariableComponent;
using CodeBase.Network.NetworkComponents.NetworkVariableComponent.Processor;
using CodeBase.Network.Proxy;
using CodeBase.Network.Runner;
using UnityEngine;

namespace CodeBase.Infrastructure.State
{
    public class StartServerState : IState, IRPCCaller
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IConfigProvider _configProvider;
        private readonly INetworkRunner _networkRunner;

        private const string NameScene = "MapScene";
        private int _sessionIndex;

        public StartServerState(IGameStateMachine stateMachine,
            IConfigProvider configProvider,
            INetworkRunner networkRunner)
        {
            _gameStateMachine = stateMachine;
            _configProvider = configProvider;
            _networkRunner = networkRunner;

            RpcProxy.RegisterRPCInstance<StartServerState>(this);
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
        
        private void SendData(int playerId)
        {
            var methodInfoClient = typeof(StartServerState).GetMethod(nameof(ClientMethod));
            RpcProxy.TryInvokeRPC<StartServerState>(methodInfoClient, ProtocolType.Tcp, $"Привет от сервера TCP!{playerId}");
            //RpcProxy.TryInvokeRPC<StartServerState>(methodInfoClient, ProtocolType.Udp, $"Привет от сервера UDP!{playerId}");
            
            TestVar.Instance.NetworkVariable.Value = 100;
        }
        
        public void Exit()
        {
        }
        
        [RPCAttributes.ClientRPC]
        public void ClientMethod(string message)
        {
            Debug.Log($"Client received: {message}");
        }
    }
}

public class TestVar
{
    private static TestVar _instance;

    public NetworkVariable<int> NetworkVariable { get; } = new("PlayerScore", 0);

    public static TestVar Instance
    {
        get {
            if (_instance != null)
                return _instance;
            
            _instance = new TestVar();
            return _instance;
        }
    }
}