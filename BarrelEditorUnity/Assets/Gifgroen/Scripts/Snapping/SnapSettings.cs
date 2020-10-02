using System.Globalization;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Gifgroen.Snapping
{
    public class SnapSettings : ScriptableObject
    {
        private const string SnapSettingsBaseFolder = "Assets/Gifgroen";

        private const string SnapSettingsResourcesFolder = "Resources";

        private const string SnapSettingsFilename = "SnapSettings";

        [SerializeField] private float[] snapSizes =
        {
            0.25f,
            0.5f,
            1f,
            2f,
            4f
        };

        [SerializeField] private int selectedIndex;

        public float[] SnapSizes => snapSizes;

        public int CurrentIndex
        {
            get => selectedIndex;
            set => selectedIndex = value;
        }

        public float CurrentSize
        {
            get
            {
                if (selectedIndex < 0 || selectedIndex >= snapSizes.Length)
                {
                    return 1f;
                }

                return snapSizes[selectedIndex];
            }
        }

        public string[] SizeLabels()
        {
            string[] labels = new string[snapSizes.Length];
            for (int i = 0; i < snapSizes.Length; i++)
            {
                labels[i] = snapSizes[i].ToString(CultureInfo.InvariantCulture);
            }

            return labels;
        }

#if UNITY_EDITOR
        public static SnapSettings GetOrCreateSettings()
        {
            SnapSettings snapSettings = Resources.Load<SnapSettings>(SnapSettingsFilename);
            if (snapSettings != null)
            {
                return snapSettings;
            }

            const string filePath = SnapSettingsBaseFolder + "/" + SnapSettingsResourcesFolder;
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            snapSettings = CreateInstance<SnapSettings>();
            AssetDatabase.CreateAsset(snapSettings,
                $"{SnapSettingsBaseFolder}/{SnapSettingsResourcesFolder}/{SnapSettingsFilename}.asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            return snapSettings;
        }
#endif
    }
}