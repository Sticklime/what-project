using System;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace CodeBase.Infrastructure.Services.SceneLoader
{
    public class SceneLoader : ISceneLoader
    {
        public async Task Load(string levelName, Action onLoaded = null)
        {
            AsyncOperationHandle<SceneInstance> waitNextScene = Addressables.LoadSceneAsync(levelName);

            await waitNextScene.Task;

            onLoaded?.Invoke();
        }
    }
}