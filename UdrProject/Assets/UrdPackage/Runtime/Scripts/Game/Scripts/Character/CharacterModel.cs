using System;
using UnityEngine;
using Urd.Models;

namespace Urd.Character
{
    [System.Serializable]
    public class CharacterModel
    {
        public float Speed => _characterConfig.Speed;

        public CharacterMovementModel CharacterMovement { get; private set; } = new ();
        
        private CharacterConfig _characterConfig;

        public CharacterModel(CharacterConfig characterConfig)
        {
            _characterConfig = characterConfig;
        }
    }
}