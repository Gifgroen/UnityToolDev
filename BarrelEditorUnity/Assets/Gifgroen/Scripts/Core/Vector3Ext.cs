using UnityEngine;

namespace Gifgroen.Core
{
    public static class Vector3Ext
    {
        public static Vector3 Round(this Vector3 position, float size = 1f)
        {
            position.x = Mathf.Round(position.x / size) * size;
            position.y = Mathf.Round(position.y / size) * size;
            position.z = Mathf.Round(position.z / size) * size;
            return position;
        }
    }
}
