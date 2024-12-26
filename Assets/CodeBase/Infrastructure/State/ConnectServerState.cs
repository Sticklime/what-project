using CodeBase.Data.StaticData;
using CodeBase.Infrastructure.Services.ConfigProvider;

namespace CodeBase.Infrastructure.State
{
    public class ConnectToServer : IState
    {
        private ServerConnectConfig _serverConnectConfig;
        private readonly IConfigProvider _configProvider;

        private int _sessionIndex;

        public ConnectToServer( IConfigProvider configProvider)
        {
            _configProvider = configProvider;
        }

        public async void Enter()
        {
            _serverConnectConfig = _configProvider.GetServerConnectConfig();
        }

        public void Exit()
        {
        }
    }
}