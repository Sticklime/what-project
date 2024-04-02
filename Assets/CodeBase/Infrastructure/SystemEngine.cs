using System;
using Entitas;
using UniRx;

namespace CodeBase.Infrastructure
{
    public class SystemEngine : IDisposable
    {
        private readonly Systems _systems = new();

        private bool _isAtLeastOneSystemRegistered;

        public void Start()
        {
            if (!_isAtLeastOneSystemRegistered)
                throw new InvalidOperationException("No system register");

            _systems.Initialize();

            Observable.EveryUpdate().Subscribe(x => UpdateSystem());
        }

        public void Dispose()
        {
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

        private void UpdateSystem() =>
            _systems.Execute();
    }
}