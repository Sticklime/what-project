using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using CodeBase.Data.StaticData;
using CodeBase.Infrastructure.Services.ConfigProvider;
using CodeBase.Network.Attributes;
using CodeBase.Network.Proxy;
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

        private const string NameScene = "MapScene";
        private int _sessionIndex;

        private static StartServerState _instance;

        public StartServerState(IGameStateMachine stateMachine,
            IConfigProvider configProvider)
        {
            _gameStateMachine = stateMachine;
            _configProvider = configProvider;

            _instance = this;    
            RpcProxy.RegisterRPCInstance<StartServerState>(_instance);
        }

        public async void Enter()
        {
            Debug.Log("Starting in SERVER mode...");
            StartServer();
        }
        
        private static async void StartServer()
        {
            ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ServerSocket.Bind(new IPEndPoint(IPAddress.Any, 5055));
            ServerSocket.Listen(10);

            
            Console.WriteLine("Сервер ожидает подключения...");
        
            var clientSocket = await ServerSocket.AcceptAsync();
            ClientsSockets.Add(clientSocket);

            foreach (var socket in ClientsSockets)
                UniTask.Run(() => RpcProxy.ListenForRpcCalls(socket));
            
            Debug.Log($"Клиент подключен: {clientSocket.RemoteEndPoint}");

            var methodInfoClient = typeof(StartServerState).GetMethod("ClientMethod");
            RpcProxy.TryInvokeRPC<StartServerState>(methodInfoClient, ClientsSockets, "Привет от Сервера!");
        }


        public void Exit()
        {
        }
        
        [RPCAttributes.ClientRPC]
        public static void ClientMethod(string message)
        {
            Debug.Log($"Client received: {message}");
        }
    }
}