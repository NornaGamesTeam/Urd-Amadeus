using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Urd.Character
{
    public class CharacterView : MonoBehaviour
    {
        private CharacterModel _characterModel;


        private void Start()
        {
            Init();
        }

        private void Init()
        {
            // remove this from the start
            SetModel(GetComponent<CharacterController>().CharacterModel);

            _characterModel.OnPositionChanged += OnCharacterMove;
        }

        private void OnCharacterMove(Vector2 characterPosition)
        {
            transform.position = characterPosition;
        }

        private void SetModel(CharacterModel characterModel)
        {
            _characterModel = characterModel;
        }
    }
}