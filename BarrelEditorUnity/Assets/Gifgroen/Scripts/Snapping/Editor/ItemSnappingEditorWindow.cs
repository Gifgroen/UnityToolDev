using System.IO;
using UnityEditor;
using UnityEngine;

namespace Gifgroen.Snapping.Editor
{
    public class ItemSnappingEditorWindow : EditorWindow
    {
        [MenuItem("Tools/Gifgroen/Snap Tool &%s")]
        public static void Open() => GetWindow<ItemSnappingEditorWindow>("Snap Tool");

        private SnapSettings _snapSettings;

        private string[] _sizeLabels;

        private void OnEnable()
        {
            _snapSettings = SnapSettings.GetOrCreateSettings();
            _sizeLabels = _snapSettings.SizeLabels();
            Selection.selectionChanged += Repaint;
        }

        // ReSharper disable once DelegateSubtraction
        private void OnDisable() => Selection.selectionChanged -= Repaint;

        private void OnGUI()
        {
            GUILayout.Label("Settings", EditorStyles.boldLabel);
            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                using (new EditorGUILayout.HorizontalScope())
                {
                    GUILayout.Label("Grid size");
                    _snapSettings.CurrentIndex = GUILayout.SelectionGrid(
                        _snapSettings.CurrentIndex,
                        _sizeLabels,
                        _snapSettings.SnapSizes.Length);
                    EditorUtility.SetDirty(_snapSettings);
                }
            }

            using (new EditorGUI.DisabledScope(Selection.gameObjects.Length == 0))
            {
                if (GUILayout.Button("Snap selection"))
                {
                    ItemSnapping.Snap(_snapSettings.CurrentSize);
                }
            }
        }

        
    }
}