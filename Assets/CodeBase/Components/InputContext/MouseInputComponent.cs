using Entitas;
using UnityEngine;

namespace CodeBase.Components.InputContext
{
    [Input]
    public class MouseInputComponent : IComponent
    {
        public Vector3 TargetPosition;
        public Vector3 StartPositionSelection;
        public Vector3 EndPositionSelection;
    }
}