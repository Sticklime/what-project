using UnityEngine;

namespace CodeBase.Data.StaticData
{
    [CreateAssetMenu(menuName = "StaticData/ServerConnect", fileName = "NewServerConnect")]
    public class ServerConnectConfig : ScriptableObject
    {
        [field: SerializeField] public string ServerAddress { get; private set; }
        [field: SerializeField] public ushort ServerPort { get; private set; }
        [field: SerializeField] public int MaxPlayers { get; private set; }
        [field: SerializeField] public string SessionName { get; private set; }
    }
}