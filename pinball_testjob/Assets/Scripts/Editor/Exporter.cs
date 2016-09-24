using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Scripts.Editor
{
    public class Exporter : EditorWindow
    {
        private Dictionary<BuildTarget, string> _buildTargets = new Dictionary<BuildTarget, string>()
        {
            { BuildTarget.Android, "P0/" },
            { BuildTarget.StandaloneWindows64, "P1/" }
        };
        private TextAsset _xml;

        [MenuItem("ProjectEditor/Exporter Window")]
        public static void ShowWindow()
        {
            GetWindow<Exporter>();
        }

        private void OnGUI()
        {
            _xml = EditorGUILayout.ObjectField("XML:", _xml, typeof(TextAsset), false) as TextAsset;
            if (_xml == null)
            {
                return;
            }

            if (GUILayout.Button("Create assets"))
            {
                if (_xml == null || string.IsNullOrEmpty(_xml.text))
                {
                    return;
                }

                var serializer = new XmlSerializer(typeof(ExportConfig));
                ExportConfig config;
                using (var textReader = new StringReader(_xml.text))
                {
                    config = (ExportConfig)serializer.Deserialize(textReader);
                }

                if (string.IsNullOrEmpty(config.FolderRoot))
                {
                    config.FolderRoot = EditorUtility.SaveFolderPanel("Save here", "/../../../", "");
                }

                foreach (var buildPair in _buildTargets)
                {
                    foreach (var file in config.Paths)
                    {
                        var assets = AssetDatabase.FindAssets(file.SourceFile);
                        var paths = assets.Select(o => AssetDatabase.GUIDToAssetPath(o)).ToArray();

                        Selection.objects = paths.SelectMany(p => AssetDatabase.LoadAllAssetsAtPath(p)) /*.Where(o => o is GameObject)*/.ToArray();
                        var selection = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);

                        if (selection.Length == 1)
                        {
                            var o = selection[0];
                            var pathName = config.FolderRoot + "/" + buildPair.Value + file.DestinationFile + ".u3d";

                            var dir = System.IO.Path.GetDirectoryName(pathName);
                            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                            {
                                Directory.CreateDirectory(dir);
                            }

                            var result = BuildPipeline.BuildAssetBundle(o, selection, pathName,
                                BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets | BuildAssetBundleOptions.DeterministicAssetBundle,
                                buildPair.Key);
                            Debug.Log(string.Format("[Exporter] {0} -> {1} :: Result = {2}", o.name, pathName, result));
                        }
                    }
                }
            }
        }

        [XmlRoot("config")]
        public class ExportConfig
        {
            [XmlAttribute("root")]
            public string FolderRoot;

            [XmlArray("paths")]
            [XmlArrayItem("path")]
            public Path[] Paths;
        }

        public class Path
        {
            [XmlAttribute("in")]
            public string SourceFile;

            [XmlAttribute("out")]
            public string DestinationFile;
        }
    }
}
