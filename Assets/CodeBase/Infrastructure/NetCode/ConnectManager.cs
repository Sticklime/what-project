using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Fusion;
using Fusion.Sockets;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.Infrastructure.NetCode
{
    public class ConnectManager : MonoBehaviour, INetworkRunnerCallbacks
    {
        public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
        {
        }

        public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
        {
        }

        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
            OnPlayerJoin?.Invoke(player);
        }

        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
        {
            OnPlayerLeave?.Invoke(player);
        }

        public void OnInput(NetworkRunner runner, NetworkInput input)
        {
        }

        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
        {
        }

        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
        {
        }

        public void OnConnectedToServer(NetworkRunner runner)
        {
        }

        public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
        {
        }

        public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request,
            byte[] token)
        {
        }

        public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
        {
        }

        public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
        {
        }

        public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
        {
        }

        public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
        {
        }

        public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
        {
        }

        public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key,
            ArraySegment<byte> data)
        {
        }

        public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
        {
        }

        public void OnSceneLoadDone(NetworkRunner runner)
        {
        }

        public void OnSceneLoadStart(NetworkRunner runner)
        {
        }

        [field: SerializeField] public NetworkRunner NetworkRunner { get; private set; }
        [SerializeField] private NetworkObject networkEventSystemPrefab;

        public event Action<PlayerRef> OnPlayerJoin;
        public event Action<PlayerRef> OnPlayerLeave;

        private static ConnectManager _instance;

        public static ConnectManager Instance
        {
            get
            {
                _instance = FindObjectOfType<ConnectManager>();
                return _instance;
            }
        }

        private void Awake() =>
            DontDestroyOnLoad(this);

        public async UniTask Join()
        {
            var startGameArgs = new StartGameArgs
            {
                GameMode = GameMode.AutoHostOrClient,
                SessionName = "TestSession"
            };
            
            NetworkRunner.ProvideInput = true;

            await NetworkRunner.StartGame(startGameArgs);

            if (NetworkRunner.IsServer) 
                NetworkRunner.Spawn(networkEventSystemPrefab);
        }
    }
}