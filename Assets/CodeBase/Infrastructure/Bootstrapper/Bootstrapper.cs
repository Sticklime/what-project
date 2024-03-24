using System;
using CodeBase.Infrastructure.Services.SceneLoader;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Bootstrapper
{
    public class Bootstrapper : MonoBehaviour
    {
        private GameStateMachine _gameStateMachine;
        private Contexts _contexts;
        private ISceneLoader _sceneLoader;
        private IGameFactory _entityFactory;

        [Inject]
        private void Construct(ISceneLoader sceneLoader, IGameFactory entityFactory)
        {
            _sceneLoader = sceneLoader;
            _entityFactory = entityFactory;
        }

        private async void Awake()
        {
            DontDestroyOnLoad(gameObject);

            _gameStateMachine = new GameStateMachine();
            _contexts = new Contexts();

            await _sceneLoader.Load("MapScene");

            Camera cameraMain = Camera.main;

            _entityFactory.CreateEntityCamera(cameraMain);
        }

        private void Update()
        {
            _contexts.
        }
    }
}