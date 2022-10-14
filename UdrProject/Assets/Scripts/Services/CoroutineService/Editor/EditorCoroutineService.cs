using System.Collections;
using System.Collections.Generic;
using Unity.EditorCoroutines.Editor;
using UnityEngine;
using Urd.Services;

public class EditorCoroutineService : BaseService, ICoroutineService
{
    public Coroutine StartCoroutine(IEnumerator coroutine)
    {
        EditorCoroutineUtility.StartCoroutineOwnerless(coroutine);
        return null;
    }

    public void StopCoroutine(Coroutine coroutine)
    {
        throw new System.NotImplementedException();
    }
    public void StopAllCoroutines()
    {
        
    }
}
