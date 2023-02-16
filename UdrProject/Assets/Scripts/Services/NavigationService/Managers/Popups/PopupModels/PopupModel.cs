using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Urd.LiveOps;
using Urd.Services.Navigation;

namespace Urd.Popup
{
    public class PopupModel : Navigable, IDeserializable
    {
        public override string Id => PopupType.ToString();

        [JsonProperty][JsonConverter(typeof(StringEnumConverter))]
        public PopupTypes PopupType { get; protected set; }

        public PopupModel(PopupTypes popupType)
        {
            PopupType = popupType;
        }

        public virtual bool WasDeserializableSuccess => PopupType != PopupTypes.None && PopupType != PopupTypes.Size;
    }
}