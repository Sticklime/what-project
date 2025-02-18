using CodeBase.EntitySystems.Camera;
using CodeBase.Infrastructure.Factory;

public class InputFeature : Feature
{
    public InputFeature(ISystemFactory system)
    {
        Add(system.CreateSystem<CameraInputSystem>());
        Add(system.CreateSystem<RaycastInputSystem>());
    }
}