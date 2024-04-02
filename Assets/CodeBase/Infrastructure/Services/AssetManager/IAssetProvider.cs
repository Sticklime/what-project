using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.AssetManager
{
    public interface IAssetProvider
    {
        Task<GameObject> LoadAsset(string name);
        Task<List<GameObject>> LoadGroup(string groupName);
    }
}