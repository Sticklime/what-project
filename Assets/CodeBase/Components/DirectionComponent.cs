using Entitas;
using UnityEngine;

namespace Components
{
    [Game]
    public class DirectionComponent : IComponent
    {
        public Vector3 Direction;
        public float Speed;
    }
}