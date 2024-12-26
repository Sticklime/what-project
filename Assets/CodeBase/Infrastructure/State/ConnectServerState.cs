using System.Threading.Tasks;
using CodeBase.Data.StaticData;
using CodeBase.Infrastructure.Services.ConfigProvider;
using Fusion;
using Fusion.Sockets;
using UnityEngine;

namespace CodeBase.Infrastructure.State
{
    public class ConnectToServer : IState
    {
        private ServerConnectConfig _serverConnectConfig;
        private readonly IConfigProvider _configProvider;
        private readonly NetworkRunner _runner;

        private int _sessionIndex;

        public ConnectToServer(NetworkRunner runner, IConfigProvider configProvider)
        {
            _runner = runner;
            _configProvider = configProvider;
        }

        public async void Enter()
        {
            _serverConnectConfig = _configProvider.GetServerConnectConfig();
            StartGameResult result = await StartGame();

            if (!result.Ok)
            {
                Debug.Log("Failed to join the game session. Trying to start a new session...");
                await StartNewSession();
            }
        }

        public void Exit()
        {
        }

        private async Task<StartGameResult> StartGame()
        {
            if (_runner.SessionInfo != null)
            {
                Debug.Log("Shutting down the existing session...");
                await _runner.Shutdown();
            }

            var startGameArgs = new StartGameArgs
            {
                GameMode = GameMode.Client,
                Address = NetAddress.CreateFromIpPort(_serverConnectConfig.ServerAddress, _serverConnectConfig.ServerPort),
                SessionName = GetSessionName(),
                Scene = null,
                PlayerCount = _serverConnectConfig.MaxPlayers,
            };

            return await _runner.StartGame(startGameArgs);
        }

        private async Task StartNewSession()
        {
            if (_runner.SessionInfo != null)
            {
                Debug.Log("Shutting down the existing session...");
                await _runner.Shutdown();
            }

            _sessionIndex++;

            var startGameArgs = new StartGameArgs
            {
                GameMode = GameMode.Client,
                Address = NetAddress.CreateFromIpPort(_serverConnectConfig.ServerAddress,
                    _serverConnectConfig.ServerPort),
                SessionName = GetSessionName(),
                Scene = null,
                PlayerCount = _serverConnectConfig.MaxPlayers,
            };

            StartGameResult res = await _runner.StartGame(startGameArgs);

            Debug.Log(res.Ok
                ? $"New session created: {_runner.SessionInfo?.Name}"
                : $"Failed to create a new session: {res.ErrorMessage}");
        }

        private string GetSessionName() => $"{_serverConnectConfig.SessionName}_{_sessionIndex}";
    }
}