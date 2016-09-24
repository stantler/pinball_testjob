using Board;
using UnityEngine;

namespace Game
{
    public class GameInstance
    {
        private BoardInformer _boardInformer;

        public GameInstance()
        {
            _boardInformer = Object.FindObjectOfType<BoardInformer>();
        }
    }
}
