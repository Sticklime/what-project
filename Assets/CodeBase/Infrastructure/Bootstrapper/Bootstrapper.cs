using CodeBase.Infrastructure.Services.SceneLoader;
using CodeBase.Infrastructure.States;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Bootstrapper
{
    public class Bootstrapper : MonoBehaviour
    {
        private GameStateMachine _gameStateMachine;
        private ISceneLoader _sceneLoader;

        [Inject]
        private void Construct(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }
        
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            _gameStateMachine = new GameStateMachine(_sceneLoader);
        }
    }
}


    public interface IPayloadedState<TPayload> : IExitableState
    {
        void Enter(TPayload payload);
    }
