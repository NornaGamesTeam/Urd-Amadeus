using System;
using System.Collections;
using UnityEngine;

namespace Urd.Services
{
    [Serializable]
    public class CoroutineService : BaseService, ICoroutineService
    {
        private static string COROUTINE_GAMEOBJECT_NAME = "CoroutineView";

        private CoroutineServiceView _coroutineView;

        public override void Init()
        {
            base.Init();

            CreateCoroutineView();
            SetAsLoaded();
        }

        private void CreateCoroutineView()
        {
            _coroutineView = new GameObject(COROUTINE_GAMEOBJECT_NAME).AddComponent<CoroutineServiceView>();
        }

        public Coroutine StartCoroutine(IEnumerator coroutine)
        {
            return _coroutineView.StartCoroutine(coroutine);
        }

        public void StopCoroutine(Coroutine coroutine)
        {
            _coroutineView.StopCoroutine(coroutine);
        }

        public void StopAllCoroutines()
        {
            _coroutineView.StopAllCoroutines();
        }
    }
}