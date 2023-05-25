using System;

namespace Urd.Utils
{
    [Flags]
    public enum LayerMaskTypes
    {
        Wall = 1 << 10,
        Item = 1 << 11,
        Enemy = 1 << 12,
        NPC = 1 << 13,
        Character = 1 << 14,
    }
}