using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.Services.AssetManager
{
    public interface IAssetProvider
    {
        void Initialize();
        Task<T> LoadAsync<T>(string address) where T : class;
        Task<T> LoadAsync<T>(AssetReference assetReference) where T : class;
        void Cleanup();
    }
}