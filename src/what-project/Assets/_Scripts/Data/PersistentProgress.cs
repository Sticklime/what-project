using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using NUnit.Framework.Constraints;

namespace CodeBase.Data
{
    public class PersistentProgress : IPersistentProgress
    {
        public PlayerData Data { get; set; }
    }

    public class PlayerData
    {
        public Dictionary<ResourcesType, ResourceData> ResourceData;

        public PlayerData(Dictionary<ResourcesType, ResourceData> resourceData)
        {
            ResourceData = resourceData;
        }
    }

    public class ResourceData : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int _valueResources;

        public int ValueResources
        {
            get => _valueResources;
            set
            {
                if (_valueResources != value)
                {
                    _valueResources = value;
                    OnPropertyChanged();
                }
            }
        }

        public ResourceData(int valueResources)
        {
            _valueResources = valueResources;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public interface IPersistentProgress
    {
        public PlayerData Data { get; set; }
    }

    public enum ResourcesType
    {
        Default = 0,
        Cristal = 1,
    }
}