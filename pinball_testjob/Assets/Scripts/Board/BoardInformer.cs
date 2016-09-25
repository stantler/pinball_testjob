using Board.Behaviours;
using UnityEngine;

namespace Board
{
    public class BoardInformer : MonoBehaviour
    {
        public Transform BallPoint;
        public Transform BounceParent;

        public PeddleBehaviour PaddleLeft;
        public PeddleBehaviour PaddleRight;

        public PullBehaviour Pull;

        public BorderBehaviour Border;
    }
}
