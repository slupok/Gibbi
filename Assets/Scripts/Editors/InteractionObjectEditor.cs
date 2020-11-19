
using UnityEngine;
using UnityEditor;
namespace Editor
{
    [CustomPropertyDrawer(typeof(WithCamera))]
    public class InteractionObjectEditor : PropertyDrawer
    {
        private Transform CameraPosition;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty isUsingCameraProperty = property.FindPropertyRelative("UsingCamera");
            SerializedProperty CameraPositionProperty = property.FindPropertyRelative("CameraPosition");
            bool isUsingCamera = (bool)isUsingCameraProperty.serializedObject.targetObject;
            CameraPosition = (Transform)CameraPositionProperty.serializedObject.targetObject;
            
            EditorGUI.BeginProperty(position, label, property);
            
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            
            var UsingCameraRect = new Rect(position.x, position.y, position.width, position.height);
            var CameraPositionRect = new Rect(position.x + 35, position.y, 120, position.height);
            
            EditorGUI.PropertyField(UsingCameraRect, isUsingCameraProperty, GUIContent.none);
            if(isUsingCamera == true)
                EditorGUI.PropertyField(CameraPositionRect, CameraPositionProperty, GUIContent.none);
            
            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
        void OnDrawGizmos()
        {
            //узнать позицию камеры
            Gizmos.DrawIcon(CameraPosition.position, "CameraGizmo.png", true); 
        }
        
    }
}
