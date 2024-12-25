using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using CodeBase.Network.Proxy;
using Cysharp.Threading.Tasks;
using MessagePack;
using UnityEngine;

namespace CodeBase.Network.Runner
{
    public class NetworkRunner
    {
        public Socket ServerSocket { get; private set; }
        public List<Socket> ClientSockets { get; private set; }
        
        public async void StartServer(ConnectServerData connectServerData)
        {
            IPAddress.TryParse(connectServerData.ip, out IPAddress address);
            
            ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ServerSocket.Bind(new IPEndPoint(address, connectServerData.port));
            ServerSocket.Listen(10);

            Console.WriteLine("Сервер ожидает подключения...");
        
            var clientSocket = await ServerSocket.AcceptAsync();
            ClientSockets.Add(clientSocket);

            foreach (var socket in ClientSockets)
                UniTask.Run(() => RpcProxy.ListenForRpcCalls(socket));
            
            Debug.Log($"Клиент подключен: {clientSocket.RemoteEndPoint}");
        }
        
        public async void StartClient(ConnectServerData connectServerData)
        {
            ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            await ServerSocket.ConnectAsync(connectServerData.ip, connectServerData.port);
            
            UniTask.Run(() => RpcProxy.ListenForRpcCalls(ServerSocket));
            
            Debug.Log($"Клиент подключен к серверу: {ServerSocket.RemoteEndPoint}");
        }
    }

    [MessagePackObject]
    [Serializable]
    public struct ConnectServerData
    {
        [Key(0)] public string ip;
        [Key(1)] public int port;
    }
}