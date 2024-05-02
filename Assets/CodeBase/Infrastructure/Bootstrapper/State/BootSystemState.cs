using CodeBase.EntitySystems;
using CodeBase.Infrastructure.Services.InputSystem;

namespace CodeBase.Infrastructure.Bootstrapper.State
{
    public class BootSystemState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly SystemEngine _systemEngine;
        private readonly GameContext _gameContext;
        private readonly InputContext _inputContext;
        private readonly IInputSystem _inputSystem;

        public BootSystemState(IGameStateMachine stateMachine, SystemEngine systemEngine, GameContext gameContext,
            InputContext inputContext, IInputSystem inputSystem)
        {
            _stateMachine = stateMachine;
            _systemEngine = systemEngine;
            _gameContext = gameContext;
            _inputContext = inputContext;
            _inputSystem = inputSystem;
        }

        public void Enter()
        {
            _systemEngine.RegisterSystem(new CameraInputSystem(_inputContext,_gameContext ,_inputSystem));
            _systemEngine.RegisterSystem(new CameraMovableSystem(_gameContext, _inputContext));
            _systemEngine.RegisterSystem(new MoveAgentSystem(_gameContext, _inputContext));
            _systemEngine.RegisterSystem(new RaycastInputSystem(_inputContext, _gameContext));
            _systemEngine.RegisterSystem(new SelectionSystem(_inputContext, _gameContext));

            _systemEngine.StartSystem();

            _stateMachine.Enter<LoadMapState>();
        }

        public void Exit()
        {
        }
    }
}