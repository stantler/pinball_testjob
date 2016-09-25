using System;
using Helpers.Extension;
using UnityEngine;

namespace Board.Behaviours
{
    public class BorderBehaviour : MonoBehaviour
    {
        public event Action OnBallCollides;

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("Ball"))
            {
                return;
            }

            OnBallCollides.SafeInvoke();
        }
    }
}
