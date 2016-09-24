using System.Collections.Generic;
using Helpers.Modules;

namespace Kernel.Game
{
    public class DataProvider : IModule
    {
        private const int ShiftBall = 0;
        private const int ShiftBumper = 10;

        public bool IsInitialized { get; set; }

        public Dictionary<int, string> ResourcesMediaFilePath { get; set; }

        public void Initialize()
        {
            ResourcesMediaFilePath = new Dictionary<int, string>()
            {
                {ShiftBall + 0, "ball/ball0"},
                {ShiftBall + 1, "ball/ball1"},
                {ShiftBumper + 0, "bumper/bumper0"},
                {ShiftBumper + 1, "bumper/bumper1"},
                {ShiftBumper + 2, "bumper/bumper2"},
            };

            IsInitialized = true;
        }

        public void Dispose()
        {
            IsInitialized = false;
        }
    }
}
