using CodeBase.EntitySystems.Build;
using CodeBase.EntitySystems.Building;
using CodeBase.Infrastructure.Factory;

namespace CodeBase.Infrastructure.State
{
    public class BuildFeature : Feature
    {
        public BuildFeature(ISystemFactory system)
        {
            Add(system.CreateSystem<BuildSystem>());
            Add(system.CreateSystem<GridSystem>());
            Add(system.CreateSystem<FollowRaycastSystem>());
            Add(system.CreateSystem<RotateBuildingSystem>());
        }
    }
}