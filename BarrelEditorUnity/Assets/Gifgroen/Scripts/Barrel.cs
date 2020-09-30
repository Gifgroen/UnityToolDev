using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gifgroen
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
        private void OnDrawGizmos()
        {
            if (type == null)
            {
                return;
            }
            Handles.color = type.Color;
            Transform barrelTransform = transform;
            Handles.DrawWireDisc(
                barrelTransform.position, 
                barrelTransform.up, 
                type.Radius);
            Handles.color = Color.white;
        }
#endif
    }
}