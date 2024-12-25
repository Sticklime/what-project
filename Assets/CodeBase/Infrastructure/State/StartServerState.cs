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
    public class StartServerState : IState, IRPCCaller
    {
        public static bool IsServer { get; } = true;
        public static Socket ServerSocket { get; private set; }
        public static List<Socket> ClientsSockets { get; } = new();
        
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
        }

        public async void Enter()
        {
            ConnectServerData serverData = new()
            {
                MaxClients = 2,
                TcpPort = 5055,
                UdpPort = 5056
            };
            
            await _networkRunner.StartServer(serverData);
            SendData();
        }
        
        private void SendData()
        {
            var methodInfoClient = typeof(StartServerState).GetMethod("ClientMethod");
            RpcProxy.TryInvokeRPC<StartServerState>(methodInfoClient, ProtocolType.Udp, "Привет от сервера UDP!");
            RpcProxy.TryInvokeRPC<StartServerState>(methodInfoClient, ProtocolType.Tcp, "Привет от сервера TCP!");
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