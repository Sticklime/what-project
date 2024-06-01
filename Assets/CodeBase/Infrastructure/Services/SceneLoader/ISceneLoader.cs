using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure.Services.SceneLoader
{
    public interface ISceneLoader
    {
        UniTask Load(string name);
    }
}