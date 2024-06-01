using CodeBase.EntitySystems;
using CodeBase.EntitySystems.Build;
using CodeBase.EntitySystems.Camera;
using CodeBase.EntitySystems.Unit;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services.InputSystem;
using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure.State
{
    public class BootSystemState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly SystemEngine _systemEngine;
        private readonly GameContext _gameContext;
        private readonly InputContext _inputContext;
        private readonly IInputSystem _inputSystem;
        private readonly IGameFactory _gameFactory;

        public BootSystemState(IGameStateMachine stateMachine, SystemEngine systemEngine, GameContext gameContext,
            InputContext inputContext, IInputSystem inputSystem, IGameFactory gameFactory)
        {
            _stateMachine = stateMachine;
            _systemEngine = systemEngine;
            _gameContext = gameContext;
            _inputContext = inputContext;
            _inputSystem = inputSystem;
            _gameFactory = gameFactory;
        }

        public void Enter()
        {
            _systemEngine.RegisterSystem(new CameraInputSystem(_inputContext, _gameContext, _inputSystem));
            _systemEngine.RegisterSystem(new CameraMovableSystem(_gameContext, _inputContext));
            _systemEngine.RegisterSystem(new MoveAgentSystem(_gameContext, _inputContext));
            _systemEngine.RegisterSystem(new RaycastInputSystem(_inputContext, _gameContext));
            _systemEngine.RegisterSystem(new SelectionSystem(_inputContext, _gameContext));
            _systemEngine.RegisterSystem(new FollowRaycastSystem(_gameContext, _inputContext));
            _systemEngine.RegisterSystem(new BuildSystem(_inputContext, _gameContext, _gameFactory));

            _systemEngine.StartSystem();

            _stateMachine.Enter<LoadSaveState>();
        }

        public void Exit()
        {
        }
    }
}