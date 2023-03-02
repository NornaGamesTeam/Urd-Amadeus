using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Urd.Services.Localization;
using Urd.View.Localization;

namespace Urd.UrdEditor
{
    [CustomEditor(typeof(LocalizationView))]
    public class EditorLocalizationView : Editor
    {
        public const string NO_KEY = "--NO KEY--";

        private bool _foldout;
        private string _filterText;
        private int _selectionIndex;

        private SerializedProperty _keyProperty;
        private LocalizationView _target;

        public override void OnInspectorGUI()
        {
            _target = target as LocalizationView;

            _keyProperty = serializedObject.FindProperty("_key");
            EditorGUILayout.PropertyField(_keyProperty);

            GUI.enabled = false;
            var valueProperty = serializedObject.FindProperty("_value");
            EditorGUILayout.PropertyField(valueProperty);
            GUI.enabled = true;

            _foldout = EditorGUILayout.Foldout(_foldout, "Search Key");

            if (_foldout)
            {
                DrawSearchFields();
            }

            serializedObject.ApplyModifiedProperties();

            SetSetValueButtons();
        }

        private void DrawSearchFields()
        {
            _filterText = EditorGUILayout.TextField("Filter", _filterText);

            var keysValues = EditorLocalizationService.GetKeysValues();

            List<KeyValuePair<string, string>> keys = new List<KeyValuePair<string, string>>();
            keys.Insert(0, new KeyValuePair<string, string>(string.Empty, NO_KEY));

            bool noFilter = string.IsNullOrEmpty(_filterText);
            foreach (var item in keysValues)
            {
                if (noFilter || item.Key.ToLower().Contains(_filterText.ToLower()))
                {
                    keys.Add(item);
                }
            }

            _selectionIndex = 0;
            for (int i = 0; i < keys.Count; i++)
            {
                if (keys[i].Key == _keyProperty.stringValue)
                {
                    _selectionIndex = i;
                    break;
                }
            }

            var newSelectionIndex = EditorGUILayout.Popup("KeyValue", _selectionIndex, GetPopupItems(keys));

            if (newSelectionIndex != _selectionIndex)
            {
                _selectionIndex = newSelectionIndex;
                _target.SetKey(keys[_selectionIndex].Key);
                _target.SetValueFromKey();
                var value = EditorLocalizationService.Locate(keys[_selectionIndex].Key);
                _target.SetTextMeshProValue(value);
            }
        }

        private string[] GetPopupItems(List<KeyValuePair<string, string>> keys)
        {
            var result = new List<string>(keys.Count);
            for (int i = 0; i < keys.Count; i++)
            {
                result.Add(GeneratePopupName(keys[i]));
            }
            return result.ToArray();
        }

        private string GeneratePopupName(KeyValuePair<string, string> item)
        {
            if (!string.IsNullOrEmpty(item.Key))
            {
                return $"{item.Key} : {item.Value}";
            }
            else
            {
                return NO_KEY;
            }
        }

        private void SetSetValueButtons()
        {
            EditorGUILayout.LabelField("Test Languages", GUI.skin.horizontalSlider);

            EditorGUILayout.BeginHorizontal();
            for (LocalizationLanguages language = LocalizationLanguages.None+1; language < LocalizationLanguages.Size; language++)
            {
                if (GUILayout.Button(language.ToString()))
                {
                    var value = EditorLocalizationService.Locate(_target.Key, language);
                    _target.SetTextMeshProValue(value);
                }
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}