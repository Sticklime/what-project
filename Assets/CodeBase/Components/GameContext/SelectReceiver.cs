using Entitas;
using UnityEngine;

namespace CodeBase.Components
{
    [Game]
    public class SelectReceiver : IComponent
    {
        public BoxCollider BoxCollider;
        public bool IsSelect;
    }
}