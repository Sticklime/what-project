using UnityEngine;
using Entitas;

namespace CodeBase.EntitySystems
{
    public class MouseInputSystem : IExecuteSystem
    {
        private readonly IGroup<InputEntity> _inputFilter;
        private readonly IGroup<GameEntity> _moveFilter;

        public MouseInputSystem(InputContext inputContext, GameContext gameContext)
        {
            _inputFilter = inputContext.GetGroup(InputMatcher.RaycastInput);
            _moveFilter = gameContext.GetGroup(GameMatcher.CharacterController);
        }

        public void Execute()
        {
            foreach (GameEntity entity in _moveFilter)
            {
                var raycastInput = _inputFilter.GetSingleEntity().raycastInput;
                var characterController = entity.characterController;

                if (Input.GetMouseButtonDown(1))
                {
                    characterController.CanMove = true;
                }
                else if (Input.GetMouseButtonUp(1))
                {
                    characterController.CanMove = false;
                }
                else if (Input.GetMouseButton(0))
                {
                    if (raycastInput.IsSelection == false)
                    {
                        raycastInput.StartPositionSelection = raycastInput.TargetPosition;
                        raycastInput.EndPositionSelection = raycastInput.TargetPosition;
                    }

                    raycastInput.IsSelection = true;

                    raycastInput.EndPositionSelection = raycastInput.TargetPosition;
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    raycastInput.IsSelection = false;
                }
            }
        }
    }
}