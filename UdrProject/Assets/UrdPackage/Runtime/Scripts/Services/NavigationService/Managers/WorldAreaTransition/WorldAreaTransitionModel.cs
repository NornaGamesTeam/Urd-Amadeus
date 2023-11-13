using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Urd.LiveOps;
using Urd.Services.Navigation;

namespace Urd.WorldAreaTransition
{
    public class WorldAreaTransitionModel : Navigable, IDeserializable
    {
        public override string Id => WorldAreaTransitionType.ToString();

        [JsonProperty]
        [JsonConverter(typeof(StringEnumConverter))]
        public WorldAreaTransitionTypes WorldAreaTransitionType { get; protected set; }

        public WorldAreaTransitionModel(WorldAreaTransitionTypes worldAreaTransitionType)
        {
            WorldAreaTransitionType = worldAreaTransitionType;
        }

        public virtual bool WasDeserializableSuccess
            => WorldAreaTransitionType != WorldAreaTransitionTypes.None &&
               WorldAreaTransitionType != WorldAreaTransitionTypes.Size;
    }
}