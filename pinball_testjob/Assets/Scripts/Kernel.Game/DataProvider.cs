using System.Collections.Generic;
using System.Linq;
using Game;
using Helpers.Modules;
using UnityEngine;

namespace Kernel.Game
{
    public class DataProvider : IModule
    {
        private const int ShiftBall = 0;
        private const int ShiftBumper = 10;

        public bool IsInitialized { get; set; }

        public Dictionary<int, string> ResourcesMediaFilePath { get; private set; }
        public GameSettings[] GameSettings { get; private set; }

        public void Initialize()
        {
            ResourcesMediaFilePath = new Dictionary<int, string>()
            {
                {ShiftBall + 0, "ball/ball0"},
                {ShiftBall + 1, "ball/ball1"},
                {ShiftBumper + 0, "bounce/bounce0"},
                {ShiftBumper + 1, "bounce/bounce1"},
            };
            GameSettings = Resources.LoadAll<TextAsset>("settings").Select(t => JsonUtility.FromJson<GameSettings>(t.text)).ToArray();

            IsInitialized = true;
        }

        public void Dispose()
        {
            IsInitialized = false;
        }
    }
}
