using Entitas;

namespace CodeBase.Components.InputContext
{
    [Input]
    public class CameraInputComponents : IComponent
    {
        public float DirectionX;
        public float DirectionZ;
    }
}
