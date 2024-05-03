using System.Collections.Generic;
using CodeBase.Presenters;
using System;

namespace CodeBase.Infrastructure.Services.PresenterLocator
{
    public class PresenterLocator : IPresenterLocator
    {
        private readonly Dictionary<Type, IPresenter> _presentersContainer = new Dictionary<Type, IPresenter>();
        
        public void EnablePresenterOfType<TPresenter>() where TPresenter : IPresenter
        {
            if (_presentersContainer.ContainsKey(typeof(TPresenter)))
                _presentersContainer[typeof(TPresenter)].OnEnable();
            else
                throw new ArgumentNullException(nameof(TPresenter));
        }

        public void DisablePresenterOfType<TPresenter>() where TPresenter : IPresenter
        {
            if (_presentersContainer.ContainsKey(typeof(TPresenter)))
                _presentersContainer[typeof(TPresenter)].OnDisable();
            else
                throw new ArgumentException(nameof(TPresenter));
        }

        public void RegisterPresenter<TPresenter>(TPresenter presenter) where TPresenter : class, IPresenter
        {
            _presentersContainer.Add(typeof(TPresenter), presenter);

            presenter.OnEnable();
        }

        public void CleanUpRegisterPresent()
        {
            foreach (IPresenter presenter in _presentersContainer.Values)
                presenter.OnDisable();

            _presentersContainer.Clear();
        }
    }

    public interface IPresenterLocator
    {
        void EnablePresenterOfType<TPresenter>() where TPresenter : IPresenter;
        void DisablePresenterOfType<TPresenter>() where TPresenter : IPresenter;
        void RegisterPresenter<TPresenter>(TPresenter presenter) where TPresenter : class, IPresenter;
        void CleanUpRegisterPresent();
    }
}