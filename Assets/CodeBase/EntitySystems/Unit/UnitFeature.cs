using CodeBase.EntitySystems;
using CodeBase.EntitySystems.Unit;
using CodeBase.Infrastructure.Factory;

public class UnitFeature : Feature
{
    public UnitFeature(ISystemFactory system)
    {
        Add(system.CreateSystem<MoveAgentSystem>());
        Add(system.CreateSystem<SelectionSystem>());
    }
}