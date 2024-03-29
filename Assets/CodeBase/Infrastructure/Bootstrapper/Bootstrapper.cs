using CodeBase.Infrastructure.Bootstrapper.State;
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

            _stateMachine.RegisterState<LoadMapState>(_stateFactory.CreateState<LoadMapState>());

            _stateMachine.Enter<LoadMapState>();
        }
    }
}