using CodeBase.EntitySystems.Camera;
using CodeBase.Infrastructure.Factory;

public class CameraFeature : Feature
{
    public CameraFeature(ISystemFactory system)
    {
        Add(system.CreateSystem<CameraMovableSystem>());
    }
}