using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Data.StaticData
{
  [CreateAssetMenu(menuName = "Data/Entities/Units/SimpleUnit", fileName = "SimpleUnit")]
  public class SimpleUnitsConfigs : ScriptableObject
  {
    [field: SerializeField] public AssetReferenceGameObject Prefab { get; private set; }
  }
}