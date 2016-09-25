using System;
using System.Collections.Generic;
using System.Linq;
using GUI.Base;
using GUI.Loading;
using Helpers.Extension;
using Helpers.Modules;
using UnityEngine;

namespace GUI
{
    [Module(PrefabPath = "GUI/GUIManager")]
    public class GUIManager : MonoBehaviour, IModule
    {
        private readonly Dictionary<int, RectTransform> _rootByLevel = new Dictionary<int, RectTransform>();
        private readonly List<GUIComponentBase> _activeScreens = new List<GUIComponentBase>();

        public bool IsInitialized { get; set; }

        public LoadingScreen LoadingScreen { get; private set; }

        private GameObject LoadContainer(Type type)
        {
            var attribute = (GUIComponentAttribute)type.GetCustomAttributes(typeof(GUIComponentAttribute), false).First();

            var prefab = attribute.PrefabPath.LoadAndInstantiate();
            if (prefab == null)
            {
                Debug.LogError("Screen Prefab couldn't be loaded. Path: " + attribute.PrefabPath);
                return null;
            }

            var layer = attribute.Layer;

            RectTransform root;
            if (!_rootByLevel.TryGetValue(layer, out root))
            {
                root = new GameObject("Root_" + layer).AddComponent<RectTransform>();
                root.SetParent(transform, false);
                root.anchorMin = Vector2.zero;
                root.anchorMax = Vector2.one;
                root.sizeDelta = Vector2.zero;

                _rootByLevel.Add(layer, root);
                SortRoots();
            }

            prefab.transform.SetParent(root, false);
            return prefab;
        }

        private void SortRoots()
        {
            var l = _rootByLevel.OrderBy(p => p.Key);
            foreach (var pair in l)
            {
                pair.Value.SetAsLastSibling();
            }
        }

        public T InitializeComponent<T>() where T : GUIComponentBase
        {
            var type = typeof(T);
            var viewInstance = LoadContainer(type);
            var instance = (T)Activator.CreateInstance(type, this, viewInstance);

            _activeScreens.Add(instance);
            return instance;
        }

        public void DisposeScreen<T>(T screen) where T : GUIComponentBase
        {
            _activeScreens.Remove(screen);
            screen.Dispose();
        }

        public void Initialize()
        {
            LoadingScreen = InitializeComponent<LoadingScreen>();
            IsInitialized = true;
        }

        public void Dispose()
        {
            DisposeScreen(LoadingScreen);
            LoadingScreen.Dispose();
            LoadingScreen = null;

            IsInitialized = false;
        }
    }
}