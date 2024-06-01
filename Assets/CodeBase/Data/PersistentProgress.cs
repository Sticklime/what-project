using System.Collections.Generic;

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

    public class ResourceData
    {
        public int ValueResources;

        public ResourceData(int valueResources)
        {
            ValueResources = valueResources;
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