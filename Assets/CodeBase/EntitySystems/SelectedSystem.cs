using System;
using System.Linq;
using Entitas;
using UnityEngine;
using Gizmos = Popcron.Gizmos;

namespace CodeBase.EntitySystems
{
    public class SelectedSystem : IExecuteSystem
    {
        private readonly IGroup<InputEntity> _inputFilter;
        private readonly IGroup<GameEntity> _selectableFilter;

        private readonly BoxCollider[] _selectBuffer = new BoxCollider[99];
        private readonly LayerMask _layerMask = LayerMask.GetMask("SelectReceiver");

        public SelectedSystem(InputContext inputContext, GameContext gameContext)
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
                    Vector3 centralPoint = CentralPoint(startPosition, endPosition);
                    Vector3 sizeBox = SizeBox(startPosition, endPosition);

                    int countValue = CreatePhysicsBox(centralPoint, sizeBox);
                    DrawBox(centralPoint, sizeBox, Color.red);

                    selectReceiver.IsSelect = false;

                    for (var index = 0; index < countValue; index++)
                    {
                        BoxCollider collider = _selectBuffer[index];

                        if (selectReceiver.BoxCollider == collider)
                        {
                            DrawBox(collider.gameObject.transform.position, collider.size, Color.green);
                            selectReceiver.IsSelect = true;
                        }
                    }
                }
            }
        }

        private int CreatePhysicsBox(Vector3 centralPoint, Vector3 sizeBox) =>
            Physics.OverlapBoxNonAlloc(centralPoint, sizeBox, _selectBuffer, Quaternion.identity, _layerMask);

        private Vector3 CentralPoint(Vector3 startPosition, Vector3 endPosition) =>
            Vector3.Lerp(startPosition, endPosition, 0.5f);

        private Vector3 SizeBox(Vector3 startPosition, Vector3 endPosition) =>
            new(Mathf.Abs(endPosition.x - startPosition.x), 1, Mathf.Abs(endPosition.z - startPosition.z));

        private void DrawBox(Vector3 centralPoint, Vector3 sizeBox, Color color) =>
            Gizmos.Cube(centralPoint, Quaternion.identity, sizeBox, color);
    }
}