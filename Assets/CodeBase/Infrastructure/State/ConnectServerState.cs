using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using CodeBase.Data.StaticData;
using CodeBase.Infrastructure.Services.ConfigProvider;
using CodeBase.Network.Attributes;
using CodeBase.Network.Proxy;
using CodeBase.Network.Runner;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.State
{
    public class ConnectToServer : IState, IRPCCaller
    {
        public static bool IsServer { get; } = false;
        public static List<Socket> ServerSocket { get; private set; } = new();
        
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
            var methodInfoClient = typeof(ConnectToServer).GetMethod("ServerMethod");
            RpcProxy.TryInvokeRPC<ConnectToServer>(methodInfoClient, ProtocolType.Tcp, "Привет от Клиента TCP!");
            RpcProxy.TryInvokeRPC<ConnectToServer>(methodInfoClient, ProtocolType.Udp, "Привет от Клиента UDP!");
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