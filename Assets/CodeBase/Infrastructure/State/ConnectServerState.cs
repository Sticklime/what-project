using System.Threading.Tasks;
using Fusion;
using Fusion.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.State
{
    public class ConnectServerState : IState
    {
        private const string NameScene = "MapScene";
        
        private const string _sessionName = "Session";
        private int _sessionIndex;

        private const string _serverAddress = "45.12.108.146";
        private const ushort _serverPort = 5055;
        private const int _maxPlayers = 2;
        
        private const GameMode _clientMode = GameMode.Client;
        private const GameMode _serverMode = GameMode.Server; 

        private readonly NetworkRunner _runner;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly bool _isServerMode;

        public ConnectServerState(IGameStateMachine stateMachine,
            NetworkRunner runner)
        {
            _runner = runner;
            _gameStateMachine = stateMachine;

#if SERVER
            _isServerMode = true;
#else
            _isServerMode = false;
#endif
        }

        public void Exit()
        {
        }

        public async void Enter()
        {
            if (_isServerMode)
            {
                Debug.Log("Starting in SERVER mode...");
                var res = await StartServer();

                Debug.Log(res.Ok
                    ? $"Server started successfully: {_runner.SessionInfo?.Name}"
                    : $"Server failed to start: {res.ErrorMessage}");
            }
            else
            {
                Debug.Log("Starting in CLIENT mode...");
                var res = await StartGame();

                if (res.Ok)
                {
                    Debug.Log($"Connected to session: {_runner.SessionInfo?.Name}");

                    if (_runner.SessionInfo?.PlayerCount > _maxPlayers)
                    {
                        Debug.Log("Session is full. Creating a new session...");
                        await StartNewSession();
                    }
                    else
                        _gameStateMachine.Enter<LoadMapState>();
                }
                else
                    Debug.Log($"Connection error: {res.ErrorMessage}");
            }
        }

        private Task<StartGameResult> StartGame()
        {
            var startGameArgs = new StartGameArgs
            {
                GameMode = _clientMode,
                Address = NetAddress.CreateFromIpPort(_serverAddress, _serverPort),
                SessionName = GetSessionName(),
                Scene = null,
                PlayerCount = _maxPlayers,
            };

            return _runner.StartGame(startGameArgs);
        }

        private Task<StartGameResult> StartServer()
        {
            var startGameArgs = new StartGameArgs
            {
                GameMode = _serverMode,
                Address = NetAddress.Any(_serverPort),
                SessionName = GetSessionName(),
                Scene = GetSceneInfo(),
                PlayerCount = _maxPlayers,
            };

            return _runner.StartGame(startGameArgs);
        }

        private async Task StartNewSession()
        {
            _sessionIndex++;

            var startGameArgs = new StartGameArgs
            {
                GameMode = _clientMode,
                Address = NetAddress.CreateFromIpPort(_serverAddress, _serverPort),
                SessionName = GetSessionName(),
                Scene = null,
                PlayerCount = _maxPlayers,
            };

            var res = await _runner.StartGame(startGameArgs);

            if (res.Ok)
            {
                Debug.Log($"New session created: {_runner.SessionInfo?.Name}");
                _gameStateMachine.Enter<LoadMapState>();
            }
            else
                Debug.Log($"Failed to create a new session: {res.ErrorMessage}");
        }
        
        private NetworkSceneInfo? GetSceneInfo()
        {
            Debug.Log(SceneManager.GetSceneByName(NameScene).buildIndex);
            var scene = SceneRef.FromIndex(SceneManager.GetSceneByName(NameScene).buildIndex * -1);
            
            var networkSceneInfo = new NetworkSceneInfo();
            
            networkSceneInfo.AddSceneRef(scene);
            
            return networkSceneInfo;
        }


        private string GetSessionName() => 
            $"{_sessionName}_{_sessionIndex}";
    }
}
