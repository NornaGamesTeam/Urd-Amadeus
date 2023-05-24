using System;

namespace Urd.Services.Physics
{
    public interface IAreaShapeModel : IEquatable<IAreaShapeModel>
    {
        AreaShapeType AreaShape { get; }
    }
}