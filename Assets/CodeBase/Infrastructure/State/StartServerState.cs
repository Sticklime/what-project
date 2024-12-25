using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using CodeBase.Data.StaticData;
using CodeBase.Infrastructure.Services.ConfigProvider;
using CodeBase.Network.Attributes;
using CodeBase.Network.Data.ConnectionData;
using CodeBase.Network.NetworkComponents.NetworkVariableComponent;
using CodeBase.Network.NetworkComponents.NetworkVariableComponent.Processor;
using CodeBase.Network.Proxy;
using CodeBase.Network.Runner;
using Cysharp.Threading.Tasks;
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
            RpcProxy.TryInvokeRPC<StartServerState>(methodInfoClient, ProtocolType.Udp, $"Привет от сервера UDP!{playerId}");
            
            var playerScore = new NetworkVariable<int>("PlayerScore", 0, NetworkVariableProcessor.Instance.SyncVariable);
            NetworkVariableProcessor.Instance.RegisterNetworkVariable("PlayerScore", playerScore);

            playerScore.Value = 100;
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