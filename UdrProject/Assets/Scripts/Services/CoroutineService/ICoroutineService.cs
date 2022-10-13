using System.Collections;
using UnityEngine;

namespace Urd.Services
{
    public interface ICoroutineService : IBaseService
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
        void StopCoroutine(Coroutine coroutine);
        void StopAllCoroutines();
    }
}