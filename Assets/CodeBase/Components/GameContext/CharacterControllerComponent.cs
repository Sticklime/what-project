using Entitas;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Components
{
    [Game]
    public class CharacterControllerComponent : IComponent
    {
        public NavMeshAgent CharacterController;
        public bool CanMove;
    }
}