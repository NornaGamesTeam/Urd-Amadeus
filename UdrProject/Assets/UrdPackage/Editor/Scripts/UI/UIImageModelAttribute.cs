using System;
using UnityEditor;
using UnityEngine;
using Urd.UI;

namespace Urd.UrdEditor
{
    //[CustomPropertyDrawer(typeof(UIImageModel))]
    public class UIImageModelAttribute : PropertyDrawer
    {
        string PropertiesVarNameFormat = "<{0}>k__BackingField";
        
        private float _positionY;
        private float _sizeY;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            _sizeY = position.height;
            _positionY = position.y;
            DrawAddressable(property, position);
            _positionY += position.height;
            _sizeY += position.height;
            DrawSpriteAndColor(property, position);
            _positionY += position.height;
            _sizeY += position.height;
            
            EditorGUI.EndProperty();
            
            property.serializedObject.ApplyModifiedProperties();
        }
        
        private void DrawAddressable(SerializedProperty property, Rect position)
        {
            float acumHorizontalX = position.x;
            float acumSizeX = 200;
            Rect rectAddressableToogleLabel = new Rect(acumHorizontalX, _positionY, acumSizeX, position.height);
            acumHorizontalX += 120;
            acumSizeX += 50;
            Rect rectAddressableToogle = new Rect(acumHorizontalX, _positionY, acumSizeX, position.height);
            string propertyAddressableCustomVarName = "_customAddressable";
            
            acumHorizontalX += 20;
            Rect rectAddressable = new Rect(acumHorizontalX, _positionY, position.width - acumHorizontalX+position.x, position.height);
            
            var propertyAddressableCustom = property.FindPropertyRelative(propertyAddressableCustomVarName);
            EditorGUI.LabelField(rectAddressableToogleLabel, propertyAddressableCustom.displayName);
            propertyAddressableCustom.boolValue = EditorGUI.Toggle(rectAddressableToogle, propertyAddressableCustom.boolValue);

            if (propertyAddressableCustom.boolValue)
            {
                string propertyAddressableVarName = "_addressable";
                var propertyAddressable = property.FindPropertyRelative(propertyAddressableVarName);
                propertyAddressable.stringValue = EditorGUI.TextField(rectAddressable, propertyAddressable.stringValue);
            }
        }
        
        private void DrawSpriteAndColor(SerializedProperty property, Rect position)
        {
            float acumHorizontalX = position.x;
            float acumSizeX = 120;
            
            Rect rectAddressableSprite = new Rect(acumHorizontalX, _positionY, acumSizeX, position.height);
            string propertySpriteCustomVarName = string.Format(PropertiesVarNameFormat, "Sprite");

            var temp = property.serializedObject.FindProperty(propertySpriteCustomVarName);
            var propertySprite = property.FindPropertyRelative(propertySpriteCustomVarName);
            EditorGUI.PropertyField(rectAddressableSprite, propertySprite);
            /*
            EditorGUI.LabelField(rectAddressableSprite, propertySprite.displayName);
            EditorGUI.LabelField(rectAddressableSprite, propertySprite.displayName);
            */
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label) + 16f;
        }
    }
}