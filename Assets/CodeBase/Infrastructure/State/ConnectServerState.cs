using System.Net;
using System.Net.Sockets;
using _Scripts.Netcore.Data.Attributes;
using _Scripts.Netcore.Data.ConnectionData;
using _Scripts.Netcore.NetworkComponents.RPCComponents;
using _Scripts.Netcore.RPCSystem;
using _Scripts.Netcore.RPCSystem.ProcessorsData;
using _Scripts.Netcore.Runner;
using CodeBase.Data.StaticData;
using CodeBase.Infrastructure.Services.ConfigProvider;
using UnityEngine;

namespace CodeBase.Infrastructure.State
{
    public class ConnectToServer : NetworkService ,IState
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
            SendData();
        }

        private void SendData()
        {
            var methodInfoClient = typeof(ConnectToServer).GetMethod(nameof(ServerMethod));

            RPCInvoker.InvokeServiceRPC<ConnectToServer>(this, methodInfoClient, NetProtocolType.Tcp, "Привет от Клиента TCP!");
            RPCInvoker.InvokeServiceRPC<ConnectToServer>(this, methodInfoClient, NetProtocolType.Tcp, "Привет от Клиента TCP!");
            RPCInvoker.InvokeServiceRPC<ConnectToServer>(this, methodInfoClient, NetProtocolType.Tcp, "Привет от Клиента TCP!");
            RPCInvoker.InvokeServiceRPC<ConnectToServer>(this, methodInfoClient, NetProtocolType.Tcp, "Привет от Клиента TCP!");
            RPCInvoker.InvokeServiceRPC<ConnectToServer>(this, methodInfoClient, NetProtocolType.Tcp, "Привет от Клиента TCP!");
            RPCInvoker.InvokeServiceRPC<ConnectToServer>(this, methodInfoClient, NetProtocolType.Tcp, "Привет от Клиента TCP!");
            RPCInvoker.InvokeServiceRPC<ConnectToServer>(this, methodInfoClient, NetProtocolType.Tcp, "Привет от Клиента TCP!");
            RPCInvoker.InvokeServiceRPC<ConnectToServer>(this, methodInfoClient, NetProtocolType.Udp, "Привет от Клиента UDP!");
            
            TestVar.Instance.NetworkVariable.OnValueChanged += newValue =>
            {
                Debug.Log($"Переменная AnotherVariable обновлена: {newValue}");
            };
        }
        
        public void Exit()
        {
        }
        
        [ServerRPC]
        public void ServerMethod(string message)
        {
            Debug.Log($"Server received: {message}");
        }
    }
}