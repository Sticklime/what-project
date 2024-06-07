using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Data.StaticData
{
    [CreateAssetMenu(menuName = "StaticData/GameModeData", fileName = "NewGameModeData")]
    public class GameModeData : ScriptableObject
    {
        [field: SerializeField] public List<ResourcesStaticData> Resources { get; private set; }
        [field: SerializeField] public GameModeType GameModeType { get; private set; }
    }

    public enum GameModeType
    {
        Default = 0,
        
    }
}