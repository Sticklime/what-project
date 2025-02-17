using System;
using System.Threading;
using System.Threading.Tasks;
using Entitas;
using Cysharp.Threading.Tasks;

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
            UpdateSystemAsync(_cancellationTokenSource.Token).Forget();
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

        private async UniTaskVoid UpdateSystemAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken);
                _systems.Execute();
            }
        }
    }
}