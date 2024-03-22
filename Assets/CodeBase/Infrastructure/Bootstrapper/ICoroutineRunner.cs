using System.Collections;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.SceneLoader
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerable coroutine);
    }
}