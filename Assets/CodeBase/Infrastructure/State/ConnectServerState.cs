using System.Net;
using System.Net.Sockets;
using CodeBase.Data.StaticData;
using CodeBase.Infrastructure.Services.ConfigProvider;
using CodeBase.Network.Attributes;
using CodeBase.Network.Data.ConnectionData;
using CodeBase.Network.Proxy;
using CodeBase.Network.Runner;
using UnityEngine;

namespace CodeBase.Infrastructure.State
{
    public class ConnectToServer : IState, IRPCCaller
    {
        private ServerConnectConfig _serverConnectConfig;
        private readonly IConfigProvider _configProvider;
        private readonly INetworkRunner _runner;

        private int _sessionIndex;
        
        public ConnectToServer(IConfigProvider configProvider,
            INetworkRunner runner)
        {
            _configProvider = configProvider;
            _runner = runner;

            RpcProxy.RegisterRPCInstance<ConnectToServer>(this);
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
            SendData();
        }

        private void SendData()
        {
            var methodInfoClient = typeof(ConnectToServer).GetMethod(nameof(ServerMethod));
            RpcProxy.TryInvokeRPC<ConnectToServer>(methodInfoClient, ProtocolType.Tcp, "Привет от Клиента TCP!");
            RpcProxy.TryInvokeRPC<ConnectToServer>(methodInfoClient, ProtocolType.Udp, "Привет от Клиента UDP!");
            
            TestVar.Instance.NetworkVariable.OnValueChanged += newValue =>
            {
                Debug.Log($"Переменная AnotherVariable обновлена: {newValue}");
            };
        }
        
        public void Exit()
        {
        }
        
        [RPCAttributes.ServerRPC]
        public void ServerMethod(string message)
        {
            Debug.Log($"Server received: {message}");
        }
    }
}