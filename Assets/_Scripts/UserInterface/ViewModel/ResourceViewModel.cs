using System.ComponentModel;
using System.Runtime.CompilerServices;
using CodeBase.Data;
using Unity.Properties;

namespace CodeBase.UserInterface.ViewModel
{
    public class ResourceViewModel : ModelView
    {
        private ResourceData _resourceData;

        [CreateProperty]
        public string CountResource => _resourceData.ValueResources.ToString();

        public ResourceViewModel(ResourceData resourceData)
        {
            _resourceData = resourceData;
            _resourceData.PropertyChanged += OnResourceDataChanged;
        }

        ~ResourceViewModel()
        {
            _resourceData.PropertyChanged -= OnResourceDataChanged;
        }

        private void OnResourceDataChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ResourceData.ValueResources))
            {
                OnPropertyChanged(nameof(CountResource));
            }
        }
    }

    public abstract class ModelView : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}