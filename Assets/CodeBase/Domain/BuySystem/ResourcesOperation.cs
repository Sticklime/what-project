using System.Collections.Generic;
using CodeBase.Data.StaticData;
using CodeBase.Data;

namespace CodeBase.Domain.BuySystem
{
    public class ResourcesOperation
    {
        private readonly IPersistentProgress _persistentProgress;

        public ResourcesOperation(IPersistentProgress persistentProgress)
        {
            _persistentProgress = persistentProgress;
        }

        public bool TryPurchaseWithResource(params ResourcesStaticData[] resourcesStaticData)
        {
            Dictionary<ResourcesType, ResourceData> resourceData = _persistentProgress.Data.ResourceData;

            foreach (var resources in resourcesStaticData)
            {
                if (CheckAvailabilityResource(resources.ResourcesType, resources.ValueResources))
                {
                    resourceData[resources.ResourcesType].ValueResources -= resources.ValueResources;

                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        private bool CheckAvailabilityResource(ResourcesType resourcesType, int cost)
        {
            Dictionary<ResourcesType, ResourceData> resourceData = _persistentProgress.Data.ResourceData;

            return resourceData.TryGetValue(resourcesType, out var resource) &&
                   CheckPurchasingPossibility(cost, resource);
        }

        private bool CheckPurchasingPossibility(int cost, ResourceData resource) =>
            resource.ValueResources >= cost;
    }
}