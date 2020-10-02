using Gifgroen.Core;
using UnityEditor;
using UnityEngine;

namespace Gifgroen.Snapping
{
#if UNITY_EDITOR 
    public static class ItemSnapping
    {
        private const string UndoStringSnap = "Snap objects";

        public static void Snap(float snapSize = 1f)
        {
            foreach (GameObject gameObject in Selection.gameObjects)
            {
                Undo.RecordObject(gameObject.transform, UndoStringSnap);
                gameObject.transform.position = gameObject.transform.position.Round(snapSize);
            }
        }

    }
    #endif
}
