using System;
using UnityEngine;
using Urd.WorldAreaTransition;

namespace Urd.View.WorldAreaTransition
{
    public interface IWorldAreaTransitionView
    {
        WorldAreaTransitionModel WorldAreaTransitionModel { get; }
        void SetWorldAreaTransitionModel(WorldAreaTransitionModel worldAreaTransitionModel);
        GameObject GameObject { get; }
        event Action OnClickOnClose;
        void Close();
    }
}