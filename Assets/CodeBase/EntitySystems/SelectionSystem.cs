using Entitas;
using UnityEngine;
using Gizmos = Popcron.Gizmos;

namespace CodeBase.EntitySystems
{
    public class SelectionSystem : IExecuteSystem
    {
        private readonly IGroup<InputEntity> _inputFilter;
        private readonly IGroup<GameEntity> _selectableFilter;

        public SelectionSystem(InputContext inputContext, GameContext gameContext)
        {
            _inputFilter = inputContext.GetGroup(InputMatcher.RaycastInput);
            _selectableFilter = gameContext.GetGroup(GameMatcher.SelectReceiver);
        }

        public void Execute()
        {
            foreach (GameEntity entity in _selectableFilter)
            {
                var raycastInput = _inputFilter.GetSingleEntity().raycastInput;
                var selectReceiver = entity.selectReceiver;

                Vector3 startPosition = raycastInput.StartPositionSelection;
                Vector3 endPosition = raycastInput.EndPositionSelection;

                if (raycastInput.IsSelection)
                {
                    DrawBox(CentralPoint(startPosition, endPosition), SizeBox(startPosition, endPosition), Color.red);

                    selectReceiver.IsSelect = IsWithinSelectionBox(entity, startPosition, endPosition);

                    if (selectReceiver.IsSelect)
                    {
                        DrawBox(entity.selectReceiver.BoxCollider.transform.position,
                            entity.selectReceiver.BoxCollider.size,
                            Color.green);
                    }
                }
            }
        }

        private bool IsWithinSelectionBox(GameEntity entity, Vector3 startPosition, Vector3 endPosition)
        {
            float minX = Mathf.Min(startPosition.x, endPosition.x);
            float maxX = Mathf.Max(startPosition.x, endPosition.x);
            float minZ = Mathf.Min(startPosition.z, endPosition.z);
            float maxZ = Mathf.Max(startPosition.z, endPosition.z);

            Vector3 position = entity.selectReceiver.BoxCollider.transform.position;

            return position.x >= minX && position.x <= maxX &&
                   position.z >= minZ && position.z <= maxZ;
        }

        private Vector3 CentralPoint(Vector3 startPosition, Vector3 endPosition) =>
            Vector3.Lerp(startPosition, endPosition, 0.5f);

        private Vector3 SizeBox(Vector3 startPosition, Vector3 endPosition) =>
            new(Mathf.Abs(endPosition.x - startPosition.x), 1, Mathf.Abs(endPosition.z - startPosition.z));

        private void DrawBox(Vector3 centralPoint, Vector3 sizeBox, Color color) =>
            Gizmos.Cube(centralPoint, Quaternion.identity, sizeBox, color);
    }
}