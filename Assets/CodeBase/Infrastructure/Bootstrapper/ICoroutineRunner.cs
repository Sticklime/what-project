using System.Collections;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.SceneLoader
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
        void StopCoroutine(Coroutine coroutine);
    }
}