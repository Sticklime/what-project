using Entitas;
using UnityEngine;

namespace CodeBase.Components.InputContext.Camera
{
    [Input]
    public class CameraInputComponents : IComponent
    {
        public Vector3 MoveDirection;
    }
}