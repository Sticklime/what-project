using System;

namespace CodeBase.Network.NetworkComponents.NetworkVariableComponent
{
    public class NetworkVariable<T>
    {
        private T _value;
        private readonly Action<string, T> _syncCallback;
        private readonly string _variableName;

        public T Value
        {
            get => _value;
            set
            {
                if (Equals(_value, value))
                    return;

                _value = value;
                OnValueChanged?.Invoke(value);

                _syncCallback?.Invoke(_variableName, value);
            }
        }

        public event Action<T> OnValueChanged;

        public NetworkVariable(string variableName, T initialValue, Action<string, T> syncCallback)
        {
            _variableName = variableName;
            _value = initialValue;
            _syncCallback = syncCallback;
        }
    }
}