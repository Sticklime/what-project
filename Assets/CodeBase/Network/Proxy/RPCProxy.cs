using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using CodeBase.Network.Attributes;
using CodeBase.Network.Data;
using CodeBase.Network.Runner;
using Cysharp.Threading.Tasks;
using MessagePack;
using UnityEngine;

namespace CodeBase.Network.Proxy
{
    public static class RpcProxy
    {
        private static Dictionary<Type, IRPCCaller> _callers = new();
        private static NetworkRunner _runner;

        public static void Initialize(INetworkRunner runner) =>
            _runner = (NetworkRunner)runner;

        public static void RegisterRPCInstance<T>(IRPCCaller caller) where T : IRPCCaller =>
            _callers[typeof(T)] = caller;

        public static bool TryInvokeRPC<TObject>(MethodInfo methodInfo, ProtocolType protocolType,
            params object[] parameters) where TObject : class
        {
            if (methodInfo.GetCustomAttribute<RPCAttributes.ClientRPC>() == null &&
                methodInfo.GetCustomAttribute<RPCAttributes.ServerRPC>() == null)
            {
                Console.WriteLine($"Method: {methodInfo.Name} must have RPC attributes.");
                return false;
            }

            if (!_callers.ContainsKey(typeof(TObject)))
            {
                Console.WriteLine($"{typeof(TObject)} must be registered.");
                return false;
            }

            var serializedParameters = parameters.Select(param =>
                MessagePackSerializer.Serialize(param)).ToArray();

            var serializedParamTypes = parameters.Select(param => param.GetType()).ToArray();
            var serializedParamTypesBytes = MessagePackSerializer.Serialize(serializedParamTypes);

            var message = new RpcMessage
            {
                MethodName = methodInfo.Name,
                Parameters = serializedParameters,
                ClassType = typeof(TObject).ToString(),
                MethodParam = serializedParamTypesBytes
            };

            byte[] data = SerializeMessage(message);

            try
            {
                if (methodInfo.GetCustomAttribute<RPCAttributes.ClientRPC>() != null)
                {
                    switch (protocolType)
                    {
                        case ProtocolType.Tcp:
                        {
                            foreach (var socket in _runner.TcpClientSockets)
                                socket.Send(data);
                
                            break;
                        }
                        case ProtocolType.Udp:
                        {
                            foreach (var socket in _runner.UdpClientSockets)
                            {
                                var endpoint = new IPEndPoint(IPAddress.Broadcast, ((IPEndPoint)socket.LocalEndPoint).Port);
                                socket.SendTo(data, endpoint);
                            }
                            break;
                        }
                    }
                }
                else if (methodInfo.GetCustomAttribute<RPCAttributes.ServerRPC>() != null)
                {
                    switch (protocolType)
                    {
                        case ProtocolType.Tcp:
                            _runner.TcpServerSocket.Send(data);
                            break;
                        case ProtocolType.Udp:
                            foreach (var socket in _runner.UdpClientSockets)
                            {
                                var endpoint = new IPEndPoint(IPAddress.Broadcast, ((IPEndPoint)socket.LocalEndPoint).Port);
                                socket.SendTo(data, endpoint);
                            }
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                return false;
            }

            return true;
        }

        private static byte[] SerializeMessage(RpcMessage message) =>
            MessagePackSerializer.Serialize(message);

        public static async UniTask ListenForRpcCalls(Socket socket)
        {
            byte[] buffer = new byte[1024 * 64];

            while (true)
            {
                Debug.Log("Waiting for RPC calls...");

                int bytesRead = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), SocketFlags.None);

                if (bytesRead <= 0)
                    continue;

                RpcMessage message = DeserializeMessage(buffer.Take(bytesRead).ToArray());
                
                if (message != null)
                    ProcessRpcMessage(Type.GetType(message.ClassType), message);
            }
        }


        private static RpcMessage DeserializeMessage(byte[] data)
        {
            try
            {
                return MessagePackSerializer.Deserialize<RpcMessage>(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Deserialization failed: {ex.Message}");
                return null;
            }
        }

        private static void ProcessRpcMessage(Type callerType, RpcMessage message)
        {
            if (callerType == null) return;
            
            var method = GetRpcMethod(callerType, message);

            if (method == null) return;

            var parameters = ConvertParameters(message.Parameters, method.GetParameters());

            if (!_callers.TryGetValue(callerType, out IRPCCaller rpcCaller)) return;
            
            method.Invoke(rpcCaller, parameters);
        }

        private static MethodInfo GetRpcMethod(Type callerType, RpcMessage message)
        {
            var paramTypes = MessagePackSerializer.Deserialize<Type[]>(message.MethodParam);
            return callerType.GetMethods().FirstOrDefault(m =>
                m.Name == message.MethodName && ParametersMatch(m.GetParameters(), paramTypes));
        }

        private static bool ParametersMatch(ParameterInfo[] parameterInfos, Type[] paramTypes)
        {
            if (parameterInfos.Length != paramTypes.Length)
                return false;

            return !parameterInfos.Where((t, i) =>
                t.ParameterType != paramTypes[i]).Any();
        }

        private static object[] ConvertParameters(byte[][] serializedParameters, ParameterInfo[] parameterInfos)
        {
            var parameters = new object[serializedParameters.Length];
            for (int i = 0; i < serializedParameters.Length; i++)
                parameters[i] = MessagePackSerializer.Deserialize
                    (parameterInfos[i].ParameterType, serializedParameters[i]);

            return parameters;
        }
    }

    public interface IRPCCaller
    {
    }
}
