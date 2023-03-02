using Urd.Services.Navigation;

namespace Urd.Boomerang
{
    public class BoomerangModel : Navigable
    {
        public override string Id => BoomerangType.ToString();

        public BoomerangTypes BoomerangType { get; protected set; }
        
        public float Duration { get; protected set; }

        public BoomerangModel(BoomerangTypes boomerangType, float duration)
        {
            BoomerangType = boomerangType;
            SetDuration(duration);
        }

        public void SetDuration(float duration)
        {
            Duration = duration;
        }
    }
}