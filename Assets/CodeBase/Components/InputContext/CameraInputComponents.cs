using Entitas;
using UnityEngine;

namespace CodeBase.Components.InputContext
{
    [Input]
    public class CameraInputComponents : IComponent
    {
        public Vector3 MoveDirection;
    }
}
