using Entitas;

namespace CodeBase.Components
{
    [Input]
    public class CameraInputComponents : IComponent
    {
        public float DirectionX;
        public float DirectionZ;
    }
}