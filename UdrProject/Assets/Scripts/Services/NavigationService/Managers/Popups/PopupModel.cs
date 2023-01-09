namespace Urd.Services.Navigation
{
    public class PopupModel : Navigable
    {
        public override string Id => PopupType.ToString();

        public PopupType PopupType { get; protected set; }
        
        public PopupModel(PopupType popupType)
        {
            PopupType = popupType;
        }
    }
}