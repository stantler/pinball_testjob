using UnityEngine;

namespace Board.Behaviours
{
    public class BounceBehaviour : MonoBehaviour
    {
        public float Force = 1500f;

        private void OnCollisionEnter(Collision other)
        {
            other.rigidbody.AddExplosionForce(Force, transform.position, 5);
        }
    }
}
