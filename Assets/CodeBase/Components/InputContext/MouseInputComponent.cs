using Entitas;
using UnityEngine;

namespace CodeBase.Components.InputContext
{
    [Input]
    public class MouseInputComponent : IComponent
    {
        public Vector3 TargetPosition;
    }
}