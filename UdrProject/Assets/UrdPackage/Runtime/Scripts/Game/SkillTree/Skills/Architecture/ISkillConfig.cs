using UnityEngine;

namespace Urd.Game.SkillTrees
{
    public interface ISkillConfig
    {
        SkillType Type { get; }
        Sprite Image { get; }
    }
}
