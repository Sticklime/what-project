using Entitas;
using UnityEditor.Build.Pipeline;

namespace CodeBase.EntitySystems.Build
{
    public class GridSystem : IExecuteSystem
    {
        

        public GridSystem(GameContext gameContext, InputContext inputContext)
        {
            /*_gameContext = gameContext.GetGroup(GameMatcher.AllOf(GameMatcher.BuildingPlan, GameMatcher.Model));
            _inputContext = gameContext.GetGroup(InputMatcher.RaycastInput);*/
        }

        public void Execute()
        {
            
        }
    }
}