using System;
using UnityEngine;

namespace CodeBase.Data.StaticData
{
    [Serializable]
    public class ResourcesStaticData
    {
        [field: SerializeField] public ResourcesType ResourcesType { get; private set; }
        [field: SerializeField] public int StartCountResources { get; private set; }
    }
}