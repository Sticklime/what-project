using System;
using CodeBase.Network.NetworkComponents.NetworkVariableComponent.Processor;
using UnityEngine;

namespace CodeBase.Network.NetworkComponents.NetworkVariableComponent
{
    public class NetworkVariable<T>
    {
        public event Action<T> OnValueChanged;
        
        private T _value;
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
       
                NetworkVariableProcessor.Instance.SyncVariable(_variableName, value);
            }
        }

        public T NonSyncValue
        {
            get => _value;
            set
            {
                if (Equals(_value, value))
                    return;

                _value = value;
                OnValueChanged?.Invoke(value);
            }
        }

        public NetworkVariable(string variableName, T initialValue)
        {
            _variableName = variableName;
            _value = initialValue;
            
            NetworkVariableProcessor.Instance.RegisterNetworkVariable(variableName, this);
        }
    }
}