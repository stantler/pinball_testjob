using UnityEngine;

namespace Helpers.Extension
{
    public static partial class Extensions
    {
        public static T LoadResource<T>(this string path) where T : Object
        {
            return Resources.Load<T>(path);
        }

        public static GameObject LoadGameObject(this string path)
        {
            return LoadResource<GameObject>(path);
        }

        public static GameObject LoadAndInstantiate(this string path, Transform parent = null)
        {
            var prefab = LoadGameObject(path);
            if (prefab == null)
            {
                Debug.LogError(string.Format("[Extensions] Can't load prefab {0}! Creating empty GameObject", path));
                prefab = new GameObject(path);
            }

            var instance = Object.Instantiate(prefab);
            instance.transform.SetParent(parent);

            return instance;
        }

        public static T LoadAndInstantiate<T>(this string path, Transform parent = null) where T : Component
        {
            var prefab = LoadGameObject(path);
            if (prefab == null)
            {
                Debug.LogError(string.Format("[Extensions] Can't load prefab {0}! Creating empty GameObject", path));
                prefab = new GameObject(path);
            }

            var instance = Object.Instantiate(prefab);
            instance.transform.SetParent(parent);

            var result = instance.GetComponent<T>();

            return result;
        }
    }
}