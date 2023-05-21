using UnityEngine;

namespace Urd.Game.SkillTrees
{
    public enum HitAreaShape
    {
        Cone,
        Box
    }
    
    public interface IHitArea
    {
        HitAreaShape Shape { get; }
        
        float BeginTime { get; }
        float EndTime { get; }
        
        float DamagePercentage { get; }
        float RotationDegreesClockWise { get; }
    }
}