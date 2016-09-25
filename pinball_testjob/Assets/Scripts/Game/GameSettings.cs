using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class GameSettings
    {
        public string Name = "default";
        public int BallRMId;
        public BounceParam[] Bounces;

        [Serializable]
        public struct BounceParam
        {
            public int RMId;
            public Vector3 Position;
            public Vector3 Rotation;
            public int Force;
        }
    }
}
