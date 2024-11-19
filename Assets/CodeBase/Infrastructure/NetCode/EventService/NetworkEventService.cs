using System;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

namespace CodeBase.Infrastructure.NetCode.EventService
{
    public class NetworkEventService : NetworkBehaviour
    {
        private NetworkRunner _networkRunner;
        
        private static NetworkEventService _instance;

        public static NetworkEventService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<NetworkEventService>();
                    
                    if (_instance == null)
                        Debug.LogError("NetworkEventService instance not found!");
                }
                return _instance;
            }
        }

        private void Awake()
        {
            DontDestroyOnLoad(this);
            _networkRunner = ConnectManager.Instance.NetworkRunner;
        }

        private readonly List<IEventCallback> _callbacks = new();

        public void RegisterCallback(IEventCallback callback)
        {
            if (!_callbacks.Contains(callback))
                _callbacks.Add(callback);
        }

        public void UnregisterCallback(IEventCallback callback)
        {
            _callbacks.Remove(callback);
        }

        public void SendEventFromServer(EventData eventData)
        {
            if (!_networkRunner.IsServer)
            {
                Debug.LogWarning("Only the server can send events using this method!");
                return;
            }

            RPC_SendEventToClients(eventData);
        }

        public void SendEventFromClient(EventData eventData)
        {
            if (_networkRunner == null || _networkRunner.IsServer)
            {
                Debug.LogWarning("SendEventFromClient should only be called by clients!");
                return;
            }

            RPC_SendEventToServer(eventData);
        }

        [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
        private void RPC_SendEventToServer(EventData eventData)
        {
            NotifyCallbacks(eventData); 
        }

        [Rpc(RpcSources.StateAuthority, RpcTargets.Proxies)]
        private void RPC_SendEventToClients(EventData eventData)
        {
            NotifyCallbacks(eventData); 
        }

        private void NotifyCallbacks(EventData eventData)
        {
            Debug.Log(eventData.EventID);

            foreach (var callback in new List<IEventCallback>(_callbacks))
            {
                Debug.Log(eventData.EventID);
                callback?.OnEventReceived(eventData);
            }
        }
    }

    public interface IEventCallback
    {
        void OnEventReceived(EventData eventData);
    }

    [Serializable]
    public struct EventData : INetworkStruct
    {
        [Networked] public string EventID { get; set; }

        public EventData(string eventId)
        {
            EventID = eventId;
        }
    }
}

