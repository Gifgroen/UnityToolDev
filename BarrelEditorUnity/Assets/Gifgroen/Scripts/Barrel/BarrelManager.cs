using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Rendering;


namespace Gifgroen.Barrel
{
    public class BarrelManager : MonoBehaviour
    {
        private static readonly List<Gifgroen.Barrel.Barrel> Barrels = new List<Gifgroen.Barrel.Barrel>();

        public static void Add(Gifgroen.Barrel.Barrel barrel)
        {
            Barrels.Add(barrel);
        }

        public static void Remove(Gifgroen.Barrel.Barrel barrel)
        {
            Barrels.Remove(barrel);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Handles.zTest = CompareFunction.LessEqual;

            Vector3 start = transform.position;
            foreach (Gifgroen.Barrel.Barrel barrel in Barrels)
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
            foreach (Gifgroen.Barrel.Barrel barrel in Barrels)
            {
                barrel.TryApplyColor();
            }
        }
    }
}