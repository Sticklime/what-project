using Entitas;
using UnityEngine.AI;

namespace CodeBase.Components.Unit
{
    [Game]
    public class CharacterControllerComponent : IComponent
    {
        public NavMeshAgent CharacterController;
        public bool CanMove;
    }
}