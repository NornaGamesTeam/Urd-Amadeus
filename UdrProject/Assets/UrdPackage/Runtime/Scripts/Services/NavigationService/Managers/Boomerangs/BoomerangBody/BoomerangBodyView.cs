using UnityEngine;
using Urd.Boomerang;
using Urd.Services;
using Urd.Utils;

namespace Urd.View.Boomerang
{
    public abstract class BoomerangBodyView : MonoBehaviour
    {
        public IBoomerangView BoomerangView { get; private set; }
        public BoomerangModel BoomerangModel => BoomerangView?.BoomerangModel;
        
        [field: SerializeField]
        public Transform Container { get; private set; }

        public void SetBoomerangView(IBoomerangView boomerangView)
        {
            BoomerangView = boomerangView;
        }

        public void Open()
        {
            OnBeginOpen();
        }

        protected virtual void OnBeginOpen()
        {
            // TODO open Animation
        }
        public virtual void OnIdle() { }

        public void CloseFromUI()
        {
            StaticServiceLocator.Get<INavigationService>().Close(BoomerangModel);
        }

        public void Close()
        {
            OnBeginClose();
        }

        public virtual void OnBeginClose()
        {
            // TODO  close Animation
        }
    }
}