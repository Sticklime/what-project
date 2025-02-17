using Entitas;
using UnityEngine;

namespace CodeBase.Components
{
    [Game]
    public class DirectionComponent : IComponent
    {
        public Vector3 Direction;
        public float Speed;
    }
}