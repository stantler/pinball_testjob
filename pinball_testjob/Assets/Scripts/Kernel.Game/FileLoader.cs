using System;
using System.Collections;
using Helpers.Extension;
using Helpers.Modules;
using UnityEngine;

namespace Kernel.Game
{
    [Module(Dependecies = new[] { typeof(DataProvider) })]
    public class FileLoader : MonoBehaviour, IModule
    {
        private const string Ext = ".u3d";
        private string _path;
        private string _additionalPath;

        private string FullPath { get { return _path + _additionalPath; } }

        private IEnumerator LoadAssetCoroutine<T>(int rmid, Action<T> callback) where T : class
        {
            var files = _.DataProvider.ResourcesMediaFilePath;
            if (!files.ContainsKey(rmid))
            {
                Debug.LogError(string.Format("[FileLoader] LoadAssetCoroutine :: Can't find RMID={0}", rmid));
                callback.SafeInvoke(null);
                yield break;
            }

            if (typeof(T) == typeof(byte[]))
            {
                var url = FullPath + files[rmid] + Ext;

                var www = new WWW(url);
                yield return www;
                callback.SafeInvoke(www.bytes as T);

                Debug.Log(string.Format("[FileLoader] LoadAssetCoroutine :: Loaded {0}#{1}", rmid, url));
            }
            else
            {
                var url = FullPath + files[rmid] + Ext;
                var www = new WWW(url);
                yield return www;

                if (!string.IsNullOrEmpty(www.error))
                {
                    Debug.LogError(string.Format("[FileLoader] LoadAssetCoroutine :: WWW has Error={0}", www.error));
                    callback.SafeInvoke(null);
                    yield break;
                }

                Debug.Log(string.Format("[FileLoader] LoadAssetCoroutine :: Loaded {0}#{1}", rmid, url));

                callback.SafeInvoke(www.assetBundle.mainAsset as T);
                www.assetBundle.Unload(false);
            }
        }

        public bool IsInitialized { get; set; }
        public void Initialize()
        {
#if UNITY_EDITOR_WIN
            _path = string.Format(@"file://{0}/../../files/", Application.dataPath);
#else
            _path = @"https://raw.githubusercontent.com/stantler/pinball_testjob/master/files/";
#endif

#if UNITY_ANDROID
            _additionalPath = "P0/";
#else
            _additionalPath = "P1/";
#endif
            IsInitialized = true;
        }

        public void LoadAsset<T>(int rmid, Action<T> callback) where T : class
        {
            StartCoroutine(LoadAssetCoroutine(rmid, callback));
        }

        public void Dispose()
        {
            IsInitialized = false;
        }
    }
}
