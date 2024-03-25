using System;
using CodeBase.Infrastructure.Services.SceneLoader;
using CodeBase.Systems;
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

        private PlayerInputSystem _playerInputSystem;
        private MovableSystem _movableSystem;

        private bool _isInitialize;

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

            _playerInputSystem = new PlayerInputSystem(_contexts);
            _movableSystem = new MovableSystem(_contexts);

            _isInitialize = true;

            _entityFactory.CreateEntityCamera(Camera.main);
        }

        private void Update()
        {
            if (!_isInitialize)
                return;
            
            _playerInputSystem.Execute();
            _movableSystem.Execute();
        }
    }
}