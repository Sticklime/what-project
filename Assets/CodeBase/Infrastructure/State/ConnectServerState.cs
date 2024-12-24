using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;
using CodeBase.Data.StaticData;
using CodeBase.Infrastructure.Services.ConfigProvider;
using CodeBase.Network.Attributes;
using CodeBase.Network.Proxy;
using Cysharp.Threading.Tasks;
using Fusion;
using UnityEngine;

namespace CodeBase.Infrastructure.State
{
    public class ConnectToServer : IState, IRPCCaller
    {
        public static bool IsServer { get; } = false;
        public static List<Socket> ServerSocket { get; private set; } = new();
        
        private ServerConnectConfig _serverConnectConfig;
        private readonly IConfigProvider _configProvider;
        private readonly NetworkRunner _runner;

        private int _sessionIndex;
        
        private static ConnectToServer _instance;

        public ConnectToServer(IConfigProvider configProvider)
        {
            _configProvider = configProvider;

            _instance = this;
            RpcProxy.RegisterRPCInstance<ConnectToServer>(_instance);
        }

        public async void Enter()
        {
            StartClient();
        }

        private static async void StartClient()
        {
            var serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            await serverSocket.ConnectAsync("127.0.0.1", 5055);
            
            UniTask.Run(() => RpcProxy.ListenForRpcCalls(serverSocket));
            
            ServerSocket.Add(serverSocket);
        
            Debug.Log($"Клиент подключен к серверу: {serverSocket.RemoteEndPoint}");

            var methodInfoClient = typeof(ConnectToServer).GetMethod("ServerMethod");
            RpcProxy.TryInvokeRPC<ConnectToServer>(methodInfoClient, ServerSocket, "Привет от Клиента!");
        }
        
        public void Exit()
        {
        }
        
        [RPCAttributes.ServerRPC]
        public static void ServerMethod(string message)
        {
            Debug.Log($"Server received: {message}");
        }
    }
}