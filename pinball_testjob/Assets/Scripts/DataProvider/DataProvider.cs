using System;
using System.Collections.Generic;
using System.Linq;
//using DataProvider.Entries;
using Helpers.Extension;
using Helpers.Modules;
using UnityEngine;

namespace DataProvider
{
    public class DataProvider : IModule
    {
        public bool IsInitialized { get; set; }

        //public Dictionary<int, ScenarioEntry> ScenarioEntries;
        //public Dictionary<int, BranchEntry> BranchEntries;
        //public Dictionary<int, BuildingEntry> BuildingEntries;
        //public Dictionary<int, QuestionEntry> QuestionEntries;
        //public Dictionary<int, TaskEntry> TaskEntries;
        //public Dictionary<int, GameEventEntry> GameEventEntries;
        //public Dictionary<int, NationEntry> NationEntries;

        public DataRaw DataRaw { get; private set; }

        public static event Action<float> OnDataLoading;

        public void Initialize()
        {
            DataRaw = Resources.Load<DataRaw>(@"Data/DataRaw");
            DataRaw.LoadFromJson(p =>
            {
                OnDataLoading.SafeInvoke(Mathf.Max(0, p - 0.1f));
            }, () =>
            {
                var commands = new List<Action>()
                {
                    //() => { ScenarioEntries = DataRaw.ScenarioEntries.ToDictionary(e => e.Id, e => e); },
                    //() => { BranchEntries = DataRaw.BranchEntries.ToDictionary(e => e.Id, e => e); },
                    //() => { BuildingEntries = DataRaw.BuildingEntries.ToDictionary(e => e.Id, e => e); },
                    //() => { QuestionEntries = DataRaw.QuestionEntries.ToDictionary(e => e.Id, e => e); },
                    //() => { TaskEntries = DataRaw.TaskEntries.ToDictionary(e => e.Id, e => e); },
                    //() => { GameEventEntries = DataRaw.GameEventEntries.ToDictionary(e => e.Id, e => e); },
                    //() => { NationEntries = DataRaw.NationEntries.ToDictionary(e => e.Id, e => e); },
                };

                for (var i = 0; i < commands.Count; i++)
                {
                    commands[i]();
                    OnDataLoading.SafeInvoke(0.9f + (float)i/ commands.Count);
                }
                OnDataLoading.SafeInvoke(1f);
                IsInitialized = true;
            });
        }

        public void Dispose()
        {
            IsInitialized = false;
        }
    }
}
