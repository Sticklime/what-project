using Entitas;
using UnityEngine;

namespace CodeBase.Components.InputContext.Camera
{
    [Input]
    public class RaycastInputComponent : IComponent
    {
        public Vector2 MousePosition;
        public Vector3 TargetPosition;
        public Vector3 StartPositionSelection;
        public Vector3 EndPositionSelection;
        public bool IsSelection;
    }
}