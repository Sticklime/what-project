using System.Threading.Tasks;
using CodeBase.Data.StaticData;
using CodeBase.Infrastructure.Services.ConfigProvider;
namespace CodeBase.Infrastructure.State
{
    public class StartServerState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IConfigProvider _configProvider;

        private ServerConnectConfig _serverConnectConfig;

        private const string NameScene = "MapScene";
        private int _sessionIndex;

        public StartServerState(IGameStateMachine stateMachine,
            IConfigProvider configProvider)
        {
            _gameStateMachine = stateMachine;
            _configProvider = configProvider;
        }

        public async void Enter()
        {
         
        }

        public void Exit()
        {
        }


    }
}