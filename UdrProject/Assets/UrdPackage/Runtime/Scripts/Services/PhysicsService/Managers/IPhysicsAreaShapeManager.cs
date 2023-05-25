using UnityEngine;
using Urd.Game.SkillTrees;

namespace Urd.Services.Physics
{
    public interface IPhysicsAreaShapeManager
    {
        AreaShapeType AreaShape { get; }
        bool TryHit(Vector2 originPoint, Vector2 direction, IHitModel hitModel);
    }
}
