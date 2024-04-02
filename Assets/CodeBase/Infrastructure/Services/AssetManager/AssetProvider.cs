using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace CodeBase.Infrastructure.Services.AssetManager
{
    public class AssetProvider : IAssetProvider
    {
        public async Task<GameObject> LoadAsset(string name)
        {
            AsyncOperationHandle<GameObject> loadAssetAsync = Addressables.LoadAssetAsync<GameObject>(name);

            await loadAssetAsync.Task;

            return loadAssetAsync.Result;
        }
        
        public async Task<List<GameObject>> LoadGroup(string groupName)
        {
            var loadAssetsAsync = Addressables.LoadAssetsAsync<GameObject>(groupName, null, true);

            await loadAssetsAsync.Task;

            return loadAssetsAsync.Result.ToList();
        }
    }
}