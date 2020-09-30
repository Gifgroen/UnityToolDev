using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gifgroen
{
    [CreateAssetMenu(fileName = "New BarrelType", menuName = "Gifgroen/BarrelType")]
    public class BarrelType : ScriptableObject
    {
#pragma warning disable 0649
        [Range(0, 10)] [SerializeField] private float radius = 1;

        [SerializeField] private float damage = 10;

        [SerializeField] private Color color = Color.red;
#pragma warning restore 0649

        public Color Color => color;
        public float Radius => radius;
    }
}