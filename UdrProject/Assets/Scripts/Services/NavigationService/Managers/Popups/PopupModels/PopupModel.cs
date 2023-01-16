using Urd.Services.Navigation;

namespace Urd.Popup
{
    public class PopupModel : Navigable
    {
        public override string Id => PopupType.ToString();

        public PopupTypes PopupType { get; protected set; }

        public PopupModel(PopupTypes popupType)
        {
            PopupType = popupType;
        }
    }
}