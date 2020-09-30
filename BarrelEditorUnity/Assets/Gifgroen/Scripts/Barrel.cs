using UnityEngine;

namespace Gifgroen
{
    public class Barrel : MonoBehaviour
    {
#pragma warning disable 0649
        [SerializeField] private float radius = 1;

        [SerializeField] private float damage = 10;

        [SerializeField] private Color color = Color.red;
#pragma warning restore 0649
    }
}