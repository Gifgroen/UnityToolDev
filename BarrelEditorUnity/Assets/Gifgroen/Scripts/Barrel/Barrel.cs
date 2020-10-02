using Gifgroen.Core;
using UnityEngine;
#if UNITY_EDITOR
using Gifgroen.Snapping;
using UnityEditor;
#endif

namespace Gifgroen.Barrel
{
    [ExecuteAlways]
    public class Barrel : MonoBehaviour
    {
        [SerializeField] public BarrelType type;

        private static readonly int ColorKey = Shader.PropertyToID("_Color");

        private MaterialPropertyBlock _mpb;
        private MaterialPropertyBlock Mpb => _mpb ?? (_mpb = new MaterialPropertyBlock());

        private void OnEnable() => BarrelManager.Add(this);
        private void OnDisable() => BarrelManager.Remove(this);

        private void OnValidate()
        {
            TryApplyColor();
        }

        private void Awake()
        {
            TryApplyColor();
        }

        public void TryApplyColor()
        {
            if (type == null)
            {
                return;
            }

            Mpb.SetColor(ColorKey, type.Color);
            GetComponent<MeshRenderer>().SetPropertyBlock(Mpb);
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            SnapSettings snapSettings = SnapSettings.GetOrCreateSettings();
            if (snapSettings == null)
            {
                return;
            }
            float size = snapSettings.CurrentSize;
            
            Gizmos.color = Color.magenta;
            Vector3 transformPosition = transform.position;
            Vector3 roundedPosition = transformPosition.Round(size);
            Gizmos.DrawWireSphere(roundedPosition, 0.2f);

            float halfLineLength = size;
            for (float i = -size; i <= size; i += size)
            {
                // X direction grid lines
                Gizmos.color = Color.red;
                Vector3 xPosition = roundedPosition;
                xPosition.z -= i;
                Gizmos.DrawLine(xPosition - Vector3.right * halfLineLength, xPosition + Vector3.right * halfLineLength);
                
                // Y -> Z direction grid lines
                Gizmos.color = Color.green;
                Vector3 yzPosition = roundedPosition;
                yzPosition.z -= i;
                Gizmos.DrawLine(yzPosition - Vector3.up * halfLineLength, yzPosition + Vector3.up * halfLineLength);
                
                // X -> Y direction grid lines
                Gizmos.color = Color.green;
                Vector3 xyPosition = roundedPosition;
                xyPosition.x -= i;
                Gizmos.DrawLine(xyPosition - Vector3.up * halfLineLength, xyPosition + Vector3.up * halfLineLength);
                
                // Z grid direction grid lines
                Gizmos.color = Color.blue;
                Vector3 zPosition = roundedPosition;
                zPosition.x -= i;
                Gizmos.DrawLine(zPosition - Vector3.forward * halfLineLength, zPosition + Vector3.forward * halfLineLength);
            }

        }

        private void OnDrawGizmos()
        {
            if (type == null)
            {
                return;
            }

            Handles.color = type.Color;
            Transform barrelTransform = transform;

            Handles.DrawWireDisc(barrelTransform.position, barrelTransform.up, type.Radius);
            Handles.color = Color.white;
        }
#endif
    }
}