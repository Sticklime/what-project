using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace CodeBase.Infrastructure.Services.SceneLoader
{
    public class SceneLoader : ISceneLoader
    {
        public async UniTask Load(string levelName)
        {
            AsyncOperationHandle<SceneInstance> waitNextScene = Addressables.LoadSceneAsync(levelName);

            await waitNextScene.Task;
        }
    }
}