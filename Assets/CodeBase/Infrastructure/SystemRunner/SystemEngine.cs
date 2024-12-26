using System;
using System.Threading;
using System.Threading.Tasks;
using Entitas;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class SystemEngine : IDisposable
    {
        private readonly Systems _systems = new();
        private readonly CancellationTokenSource _cancellationTokenSource = new();
        private bool _isAtLeastOneSystemRegistered;

        public void StartSystem()
        {
            if (!_isAtLeastOneSystemRegistered)
                throw new InvalidOperationException("No system registered");

            _systems.Initialize();
            UpdateSystemAsync().Forget();
        }

        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();

            _systems.Cleanup();
            _systems.TearDown();
        }

        public void RegisterSystem(ISystem system)
        {
            if (system == null)
                throw new ArgumentNullException(nameof(system));

            _systems.Add(system);
            _isAtLeastOneSystemRegistered = true;
        }

        private async UniTaskVoid UpdateSystemAsync()
        {
            while (true)
            {
                _systems.Execute();
                await UniTask.Yield();
            }
        }
    }
}