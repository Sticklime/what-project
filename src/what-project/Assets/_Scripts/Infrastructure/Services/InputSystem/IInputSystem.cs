using UnityEngine;

namespace CodeBase.Infrastructure.Services.InputSystem
{
    public interface IInputSystem
    {
        public Vector3 DirectionAxis { get; }
        public Vector2 MousePosition { get; }
        public bool LeftMouseButton { get; }
        public bool RightMouseButton { get; }
    }

    public interface IInitializationInput : IInputSystem
    {
        public void EnableSystem();
    }
}