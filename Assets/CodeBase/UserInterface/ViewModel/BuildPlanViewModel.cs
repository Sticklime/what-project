using System.Numerics;
using CodeBase.Infrastructure.Factory;
using Unity.Properties;
using Vector3 = UnityEngine.Vector3;

namespace CodeBase.UserInterface.ViewModel
{
    public class BuildPlanViewModel
    {
        private readonly IGameFactory _gameFactory;

        [CreateProperty]
        public string NameButton { get; set; }

        public BuildPlanViewModel(IGameFactory gameFactory, string nameButton)
        {
            NameButton = nameButton;
            _gameFactory = gameFactory;
        }

        public void CreateBuildPlan()
        {
            _gameFactory.CreateBuildingPlan(Vector3.zero);
        }
    }
}