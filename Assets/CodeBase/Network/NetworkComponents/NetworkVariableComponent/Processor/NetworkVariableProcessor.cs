using System.Collections.Generic;
using System.Net.Sockets;
using System.Reflection;
using CodeBase.Network.Attributes;
using CodeBase.Network.NetworkComponents.NetworkVariableComponent.Data;
using CodeBase.Network.Proxy;
using CodeBase.Network.Runner;
using MessagePack;
using UnityEngine;

namespace CodeBase.Network.NetworkComponents.NetworkVariableComponent.Processor
{
    public class NetworkVariableProcessor : IRPCCaller
    {
        private readonly Dictionary<string, object> _networkVariables = new();
        private INetworkRunner _networkRunner;

        private static NetworkVariableProcessor _instance;

        public static NetworkVariableProcessor Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;

                _instance = new NetworkVariableProcessor();
                return _instance;
            }
        }

        public void Initialize(INetworkRunner networkRunner)
        {
            _networkRunner = networkRunner;
            RpcProxy.RegisterRPCInstance<NetworkVariableProcessor>(this);
        }

        public void RegisterNetworkVariable<T>(string name, NetworkVariable<T> networkVariable)
        {
            if (_networkVariables.TryAdd(name, networkVariable))
                return;
            
            Debug.LogWarning($"Variable {name} is already registered.");
        }

        public NetworkVariable<T> GetNetworkVariable<T>(string name)
        {
            if (_networkVariables.TryGetValue(name, out var variable))
                return variable as NetworkVariable<T>;

            return null;
        }

        public void SyncVariable<T>(string name, T newValue)
        {
            if (!_networkRunner.IsServer)
            {
                Debug.LogWarning("Only the server can modify network variables.");
                return;
            }

            if (_networkVariables.TryGetValue(name, out var variable))
                if (variable is NetworkVariable<T> networkVariable)
                    networkVariable.Value = newValue;

            var message = new NetworkVariableMessage
            {
                VariableName = name,
                SerializedValue = MessagePackSerializer.Serialize(newValue)
            };

            MethodInfo methodInfo = typeof(NetworkVariableProcessor).GetMethod(nameof(SyncVariableOnClients));
            
            RpcProxy.TryInvokeRPC<NetworkVariableProcessor>(
                methodInfo,
                ProtocolType.Tcp,
                message);
        }

        [RPCAttributes.ClientRPC]
        public void SyncVariableOnClients(NetworkVariableMessage message)
        {
            if (!_networkVariables.TryGetValue(message.VariableName, out var variable))
                return;
            
            var variableType = variable.GetType().GetGenericArguments()[0];
            var deserializedValue = MessagePackSerializer.Deserialize(variableType, message.SerializedValue);
            
            var property = variable.GetType().GetProperty(nameof(NetworkVariable<object>.Value));
            property?.SetValue(variable, deserializedValue);
        }
    }
}
