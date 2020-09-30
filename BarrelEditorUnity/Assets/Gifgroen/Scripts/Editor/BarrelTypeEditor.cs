using UnityEditor;

namespace Gifgroen.Editor
{
    [CustomEditor(typeof(BarrelType))]
    public class BarrelTypeEditor : UnityEditor.Editor
    {
        private SerializedObject _so;

        private SerializedProperty _propRadius;

        private SerializedProperty _propDamage;

        private SerializedProperty _propColor;

        private void OnEnable()
        {
            _so = serializedObject;
            _propRadius = _so.FindProperty("radius");
            _propDamage = _so.FindProperty("damage");
            _propColor = _so.FindProperty("color");
        }

        public override void OnInspectorGUI()
        {
            _so.Update();
            EditorGUILayout.PropertyField(_propRadius);
            EditorGUILayout.PropertyField(_propDamage);
            EditorGUILayout.PropertyField(_propColor);
            if (_so.ApplyModifiedProperties())
            {
                BarrelManager.TryApplyAllColors();
            }
        }
    }
}