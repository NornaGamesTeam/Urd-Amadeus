using System;
using UnityEngine;
using Urd.Boomerang;

namespace Urd.View.Boomerang
{
    public interface IBoomerangView
    {
        BoomerangModel BoomerangModel { get; }
        void SetBoomerangModel(BoomerangModel boomerangModel);
        GameObject GameObject { get; }
        event Action OnClickOnClose;
        void Close();
    }
}