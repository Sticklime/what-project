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
        public IReadOnlyDictionary<int, Socket> ConnectedClients;

        public List<Socket> TcpClientSockets { get; private set; } = new();
        public List<Socket> UdpClientSockets { get; private set; } = new();

        public Socket TcpServerSocket { get; private set; }
        public Socket UdpServerSocket { get; private set; }

        public bool IsServer { get; private set; }

        public async UniTask StartServer(ConnectServerData connectServerData)
        {
            TcpServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            TcpServerSocket.Bind(new IPEndPoint(IPAddress.Any, connectServerData.TcpPort));
            TcpServerSocket.Listen(connectServerData.MaxClients);

            UdpServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            UdpServerSocket.Bind(new IPEndPoint(IPAddress.Any, connectServerData.UdpPort));

            IsServer = true;

            Console.WriteLine("Сервер ожидает подключения...");

            var clientSocketTCP = await TcpServerSocket.AcceptAsync();
            TcpClientSockets.Add(clientSocketTCP);

            UniTask.Run(() => RpcProxy.ListenForRpcCalls(clientSocketTCP));

            Debug.Log($"TCP клиент подключен: {clientSocketTCP.RemoteEndPoint}");

            Debug.Log("UDP сервер готов к приему данных.");
        }

        public async UniTask StartClient(ConnectClientData connectClientData)
        {
            var tcpClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            await tcpClientSocket.ConnectAsync(connectClientData.Ip.ToString(), connectClientData.TcpPort);
            TcpServerSocket = tcpClientSocket;

            var udpClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            udpClientSocket.Connect(connectClientData.Ip.ToString(), connectClientData.UdpPort);
            UdpServerSocket = udpClientSocket;

            Debug.Log($"Клиент подключен к серверу: {tcpClientSocket.RemoteEndPoint}");

            UniTask.Run(() => RpcProxy.ListenForRpcCalls(tcpClientSocket));
            UniTask.Run(() => RpcProxy.ListenForRpcCalls(udpClientSocket));
        }
    }

    public interface INetworkRunner
    {
        UniTask StartServer(ConnectServerData connectServerData);
        UniTask StartClient(ConnectClientData connectClientData);
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