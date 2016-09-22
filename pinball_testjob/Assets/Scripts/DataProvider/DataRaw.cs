using System;
using System.Collections;
using System.Collections.Generic;
using DataProvider.Entries;
using GameDevWare.Serialization;
using Helpers.Extension;
using UnityEngine;

namespace DataProvider
{
    [Serializable]
    public class DataRaw : ScriptableObject
    {
        //[NonSerialized]
        //public List<ScenarioEntry> ScenarioEntries;
        //[NonSerialized]
        //public List<BranchEntry> BranchEntries;
        //[NonSerialized]
        //public List<BuildingEntry> BuildingEntries;
        //[NonSerialized]
        //public List<QuestionEntry> QuestionEntries;
        //[NonSerialized]
        //public List<GameEventEntry> GameEventEntries;
        //[NonSerialized]
        //public List<TaskEntry> TaskEntries;
        //[NonSerialized]
        //public List<NationEntry> NationEntries;

        [NonSerialized]
        public string[] Donaters;

        [HideInInspector]
        public List<Sprite> Sprites;
        public TextAsset DonatersFile;

        public Sprite MoneyIcon;

#if UNITY_EDITOR
        public void SaveToJson()
        {
            //Save("ScenarioEntries", ScenarioEntries);
            //Save("BranchEntries", BranchEntries);
            //Save("BuildingEntries", BuildingEntries);
            //Save("QuestionEntries", QuestionEntries);
            //Save("TaskEntries", TaskEntries);
            //Save("GameEventEntries", GameEventEntries);
            //Save("NationEntries", NationEntries);
        }

        public static void Save<T>(string file, T data)
        {
            //var json = Json.SerializeToString(data, SerializationOptions.PrettyPrint);
            //Debug.Log(string.Format("[Save] {0}: {1}", file, json));
            //System.IO.File.WriteAllText(string.Format("{0}/Data/{1}.json", Application.streamingAssetsPath, file), json);
        }
#endif
        public void LoadFromJson(Action<float> progressCallback, Action callback)
        {
            ApplicationManagerComponent.Instance.StartCoroutine(LoadAll(progressCallback, callback));
        }

        public IEnumerator LoadAll(Action<float> progressCallback, Action callback)
        {
            var commands = new List<IEnumerator>()
            {
                CoroutineWrapper(()=>Donaters = DonatersFile.text.Split(new [] {'\n'}, StringSplitOptions.RemoveEmptyEntries)),
                //Load<List<ScenarioEntry>>("ScenarioEntries", data => { ScenarioEntries = data; }),
                //Load<List<BranchEntry>>("BranchEntries", data => { BranchEntries = data; }),
                //Load<List<BuildingEntry>>("BuildingEntries", data => { BuildingEntries = data; }),
                //Load<List<QuestionEntry>>("QuestionEntries", data => { QuestionEntries = data; }),
                //Load<List<TaskEntry>>("TaskEntries", data => { TaskEntries = data; }),
                //Load<List<GameEventEntry>>("GameEventEntries", data => { GameEventEntries = data; }),
                //Load<List<NationEntry>>("NationEntries", data => { NationEntries = data; })
            };

            progressCallback.SafeInvoke(0f);

            var count = commands.Count;
            for (var i = 0; i < count; i++)
            {
                yield return ApplicationManagerComponent.Instance.StartCoroutine(commands[i]);
                progressCallback.SafeInvoke(i / (float)count);
            }
            progressCallback.SafeInvoke(1f);

            callback();
        }

        public IEnumerator CoroutineWrapper(Action action)
        {
            action();
            yield return new WaitForEndOfFrame();
        }

        public static IEnumerator Load<T>(string file, Action<T> callback)
        {
#if UNITY_EDITOR
            var www = new WWW(string.Format("file://{0}/Data/{1}.json", Application.streamingAssetsPath, file));
#else
            var www = new WWW(string.Format("{0}/Data/{1}.json", Application.streamingAssetsPath, file));
#endif
            yield return www;
            Debug.Log(string.Format("[Load] {0}: {1}", file, www.text));
            //callback(Json.Deserialize<T>(www.text));
        }
    }
}
