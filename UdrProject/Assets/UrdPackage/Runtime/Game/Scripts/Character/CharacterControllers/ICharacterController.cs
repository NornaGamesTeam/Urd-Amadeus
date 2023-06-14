using System;
using UnityEngine;
using Urd.Services.Physics;

namespace Urd.Character
{
    // TODO create from the level manager
    public interface ICharacterController : IDisposable
    {
        public ICharacterModel CharacterModel { get; }
    }
}
