using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Data.StaticData
{
    [CreateAssetMenu(menuName = "GameModeData", fileName = "NewGameModeData", order = 66)]
    public class GameModeData : ScriptableObject
    {
        [field: SerializeField] List<ResourcesStaticData> Resources = new List<ResourcesStaticData>();
    }
}