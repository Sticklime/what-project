﻿using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using _Scripts.Netcore.RPCSystem;
using _Scripts.Netcore.RPCSystem.ProcessorsData;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Scripts.Netcore.Spawner.ObjectsSyncer
{
  public class NetworkObjectsSyncer : INetworkObjectSyncer
  {
    private readonly List<(AssetReferenceGameObject, int, GameObject)> _networkObjects = new();
    private MethodInfo _spawnMethodInfo;

    public void AddNetworkObject(AssetReferenceGameObject objectId, int uniqueId, GameObject gameObject) => 
      _networkObjects.Add((objectId, uniqueId, gameObject));

    public bool CheckSyncObject(AssetReferenceGameObject prefabId, int uniqueId) =>
      _networkObjects.Any(networkObject => 
        networkObject.Item1 == prefabId && networkObject.Item2 == uniqueId);

    public void Sync(NetworkSpawner networkSpawner)
    {
      _spawnMethodInfo = typeof(NetworkSpawner).GetMethod("SpawnClientRpc");
            
      foreach (var networkObject in _networkObjects)
        RPCInvoker.InvokeServiceRPC<NetworkSpawner>(networkSpawner, _spawnMethodInfo,
          NetProtocolType.Tcp, networkObject.Item1, networkObject.Item2, networkObject.Item3.transform.position,
          networkObject.Item3.transform. rotation, networkObject.Item3.transform.localScale);
    }
  }

  public interface INetworkObjectSyncer
  {
    void Sync(NetworkSpawner networkSpawner);
    void AddNetworkObject(AssetReferenceGameObject objectId, int uniqueId, GameObject gameObject);
    bool CheckSyncObject(AssetReferenceGameObject prefabId, int uniqueId);
  }
}