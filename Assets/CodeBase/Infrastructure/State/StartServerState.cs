using _Scripts.Netcore.Data.Attributes;
using _Scripts.Netcore.Data.ConnectionData;
using _Scripts.Netcore.NetworkComponents.NetworkVariableComponent;
using _Scripts.Netcore.NetworkComponents.RPCComponents;
using _Scripts.Netcore.RPCSystem;
using _Scripts.Netcore.RPCSystem.ProcessorsData;
using _Scripts.Netcore.Runner;
using CodeBase.Infrastructure.Services.ConfigProvider;
using UnityEngine;

namespace CodeBase.Infrastructure.State
{
    public class StartServerState : NetworkService , IState
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
            var methodInfoClient = typeof(StartServerState).GetMethod(nameof(ClientMethod));
            RPCInvoker.InvokeServiceRPC<StartServerState>(this, methodInfoClient, NetProtocolType.Tcp, $"Привет от сервера TCP!{playerId}");
            RPCInvoker.InvokeServiceRPC<StartServerState>(this, methodInfoClient, NetProtocolType.Tcp, $"Привет от сервера TCP!{playerId}");
            RPCInvoker.InvokeServiceRPC<StartServerState>(this, methodInfoClient, NetProtocolType.Tcp, $"Привет от сервера TCP!{playerId}");
            RPCInvoker.InvokeServiceRPC<StartServerState>(this, methodInfoClient, NetProtocolType.Tcp, $"Привет от сервера TCP!{playerId}");
            RPCInvoker.InvokeServiceRPC<StartServerState>(this, methodInfoClient, NetProtocolType.Tcp, $"Привет от сервера TCP!{playerId}");
            RPCInvoker.InvokeServiceRPC<StartServerState>(this, methodInfoClient, NetProtocolType.Tcp, $"Привет от сервера TCP!{playerId}");
            RPCInvoker.InvokeServiceRPC<StartServerState>(this, methodInfoClient, NetProtocolType.Udp, $"Привет от сервера UDP!{playerId}");
            RPCInvoker.InvokeServiceRPC<StartServerState>(this, methodInfoClient, NetProtocolType.Udp, $"Привет от сервера UDP!{playerId}");
            RPCInvoker.InvokeServiceRPC<StartServerState>(this, methodInfoClient, NetProtocolType.Udp, $"Привет от сервера UDP!{playerId}");
            RPCInvoker.InvokeServiceRPC<StartServerState>(this, methodInfoClient, NetProtocolType.Udp, $"Привет от сервера UDP!{playerId}");
            RPCInvoker.InvokeServiceRPC<StartServerState>(this, methodInfoClient, NetProtocolType.Udp, $"Привет от сервера UDP!{playerId}");
            TestVar.Instance.NetworkVariable.Value = 100;
        }
        
        public void Exit()
        {
        }
        
        [ClientRPC]
        public void ClientMethod(string message)
        {
            Debug.Log($"Client received: {message}");
        }
    }
}

public class TestVar : IRPCCaller
{
    private static TestVar _instance;

    public readonly NetworkVariable<int> NetworkVariable = new("PlayerScore", 0);

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