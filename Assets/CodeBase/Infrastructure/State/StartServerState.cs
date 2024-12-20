using System.Threading.Tasks;
using CodeBase.Data.StaticData;
using CodeBase.Infrastructure.Services.ConfigProvider;
using Fusion;
using Fusion.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.State
{
    public class StartServerState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IConfigProvider _configProvider;
        private readonly NetworkRunner _runner;

        private ServerConnectConfig _serverConnectConfig;

        private const string NameScene = "MapScene";
        private int _sessionIndex;

        public StartServerState(IGameStateMachine stateMachine, NetworkRunner networkRunner,
            IConfigProvider configProvider)
        {
            _gameStateMachine = stateMachine;
            _runner = networkRunner;
            _configProvider = configProvider;
        }

        public async void Enter()
        {
            _serverConnectConfig = _configProvider.GetServerConnectConfig();

            Debug.Log("Starting in SERVER mode...");
            var result = await LaunchServer();

            if (result.Ok)
            {
                Debug.Log($"Server started successfully: {GetSessionName()}");
                _gameStateMachine.Enter<LoadMapState>();
            }
            else
                Debug.Log($"Server failed to start: {result.ErrorMessage}");
        }

        public void Exit()
        {
        }

        private async Task<StartGameResult> LaunchServer()
        {
            if (_runner.SessionInfo != null)
            {
                Debug.Log("Shutting down the existing session...");
                await _runner.Shutdown();
            }

            var startGameArgs = new StartGameArgs
            {
                GameMode = GameMode.Server,
                Address = NetAddress.Any(),
                SessionName = GetSessionName(),
                PlayerCount = _serverConnectConfig.MaxPlayers,
            };

            return await _runner.StartGame(startGameArgs);
        }

        private SceneRef GetSceneInfo()
        {
            Scene scene = SceneManager.GetSceneByName(NameScene);
            if (!scene.IsValid())
            {
                Debug.LogError($"Scene {NameScene} is not valid or not loaded.");
                return SceneRef.None;
            }

            return SceneRef.FromIndex(scene.buildIndex);
        }

        private string GetSessionName()
        {
            Debug.Log($"{_serverConnectConfig.SessionName}_{_sessionIndex}");
            return $"{_serverConnectConfig.SessionName}_{_sessionIndex}";
        }
    }
}