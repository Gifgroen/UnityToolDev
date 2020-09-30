using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gifgroen
{
    public class BarrelManager : MonoBehaviour
    {
        private static readonly List<Barrel> Barrels = new List<Barrel>();

        public static void Add(Barrel barrel)
        {
            Barrels.Add(barrel);
        }

        public static void Remove(Barrel barrel)
        {
            Barrels.Remove(barrel);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Handles.zTest = CompareFunction.LessEqual;

            Vector3 start = transform.position;
            foreach (Barrel barrel in Barrels)
            {
                if (barrel.type == null)
                {
                    continue;
                }

                Vector3 end = barrel.transform.position;
                var halfHeight = (start.y - end.y) * 0.5f;
                Vector3 offset = Vector3.up * halfHeight;
                Handles.DrawBezier(
                    start,
                    end,
                    start - offset,
                    end + offset,
                    barrel.type.Color,
                    EditorGUIUtility.whiteTexture,
                    1f
                );
            }
        }
#endif
        public static void TryApplyAllColors()
        {
            foreach (Barrel barrel in Barrels)
            {
                barrel.TryApplyColor();
            }
        }
    }
}