using CodeBase.Infrastructure.Bootstrapper.Factory;
using CodeBase.Infrastructure.Bootstrapper.State;
using CodeBase.Infrastructure.State;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Bootstrapper
{
    public class Bootstrapper : MonoBehaviour
    {
        private IGameStateMachine _stateMachine;
        private IStateFactory _stateFactory;

        [Inject]
        private void Construct(IGameStateMachine stateMachine, IStateFactory stateFactory)
        {
            _stateMachine = stateMachine;
            _stateFactory = stateFactory;
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            RegisterState();
            _stateMachine.Enter<BootstrapState>();
        }

        private void RegisterState()
        {
            _stateMachine.RegisterState<BootstrapState>(_stateFactory.CreateState<BootstrapState>());
            _stateMachine.RegisterState<BootSystemState>(_stateFactory.CreateState<BootSystemState>());
            _stateMachine.RegisterState<LoadMapState>(_stateFactory.CreateState<LoadMapState>());
        }
    }
}