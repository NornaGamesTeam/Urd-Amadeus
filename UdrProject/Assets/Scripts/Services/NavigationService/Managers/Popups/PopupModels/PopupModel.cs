using Urd.Services.Navigation;

namespace Urd.Popup
{
    public class PopupModel : Navigable
    {
        public override string Id => PopupTypes.ToString();

        public PopupTypes PopupTypes { get; protected set; }
        
        public PopupModel(PopupTypes popupTypes)
        {
            PopupTypes = popupTypes;
        }
    }
}