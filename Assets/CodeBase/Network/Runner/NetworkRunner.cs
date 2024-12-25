using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using CodeBase.Network.Proxy;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Network.Runner
{
    public class NetworkRunner : INetworkRunner
    {
        public event Action<int> OnPlayerConnected;

        public Dictionary<int, Socket> ConnectedClients { get; } = new();

        public List<Socket> TcpClientSockets { get; } = new();
        public List<Socket> UdpClientSockets { get; } = new();

        public Socket TcpServerSocket { get; private set; }
        public Socket UdpServerSocket { get; private set; }

        public int TcpPort { get; private set; }
        public int UdpPort { get; private set; }
        public int MaxClients { get; private set; }

        public bool IsServer { get; private set; }

        public async UniTask StartServer(ConnectServerData connectServerData)
        {
            SetServerParameters(connectServerData);

            TcpServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            TcpServerSocket.Bind(new IPEndPoint(IPAddress.Any, TcpPort));
            TcpServerSocket.Listen(MaxClients);

            UdpServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            UdpServerSocket.Bind(new IPEndPoint(IPAddress.Any, UdpPort));

            IsServer = true;
            RpcProxy.Initialize(this);

            WaitConnectClients();
        }

        public async UniTask StartClient(ConnectClientData connectClientData)
        {
            TcpServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            await TcpServerSocket.ConnectAsync(connectClientData.Ip.ToString(), connectClientData.TcpPort);

            UdpServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            UdpServerSocket.ConnectAsync(connectClientData.Ip.ToString(), connectClientData.UdpPort);

            Debug.Log($"Клиент подключен к серверу: {TcpServerSocket.RemoteEndPoint}");

            RpcProxy.Initialize(this);

            UniTask.Run(() => RpcProxy.ListenForRpcCalls(TcpServerSocket));
            UniTask.Run(() => RpcProxy.ListenForRpcCalls(UdpServerSocket));
        }

        private async void WaitConnectClients()
        {
            while (TcpClientSockets.Count < MaxClients
                   && UdpClientSockets.Count < MaxClients)
            {
                Console.WriteLine("Сервер ожидает подключения...");

                var clientSocketTCP = await TcpServerSocket.AcceptAsync();

                int playerIndex = TcpClientSockets.IndexOf(clientSocketTCP);
                
                TcpClientSockets.Add(clientSocketTCP);
                ConnectedClients.Add(playerIndex, clientSocketTCP);

                OnPlayerConnected?.Invoke(playerIndex);
                UniTask.Run(() => RpcProxy.ListenForRpcCalls(clientSocketTCP));

                Debug.Log($"TCP клиент подключен: {clientSocketTCP.RemoteEndPoint}");

                Debug.Log("UDP сервер готов к приему данных.");
            }
        }

        private void SetServerParameters(ConnectServerData data)
        {
            TcpPort = data.TcpPort;
            UdpPort = data.UdpPort;
            MaxClients = data.MaxClients;
        }
    }

    public interface INetworkRunner
    {
        UniTask StartServer(ConnectServerData connectServerData);
        UniTask StartClient(ConnectClientData connectClientData);

        event Action<int> OnPlayerConnected;

        Dictionary<int, Socket> ConnectedClients { get; }

        List<Socket> TcpClientSockets { get; }
        List<Socket> UdpClientSockets { get; }

        Socket TcpServerSocket { get; }
        Socket UdpServerSocket { get; }

        int TcpPort { get; }
        int UdpPort { get; }
        int MaxClients { get; }

        bool IsServer { get; }
    }

    public struct ConnectServerData
    {
        public int TcpPort;
        public int UdpPort;
        public int MaxClients;
    }

    public struct ConnectClientData
    {
        public IPAddress Ip;
        public int TcpPort;
        public int UdpPort;
    }
}