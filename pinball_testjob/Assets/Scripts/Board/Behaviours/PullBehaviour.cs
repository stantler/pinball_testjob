using UnityEngine;

namespace Board.Behaviours
{
    public class PullBehaviour : MonoBehaviour
    {
        private GameObject _ball;

        private float _distanceTraveled;

        public float Force;
        public float Distance;
        public float PullSpeed;
        public float PushSpeed;

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("Ball"))
            {
                return;
            }
            _ball = other.gameObject;
        }

        public void UpdateInput(bool isPressed)
        {
            if (isPressed)
            {
                if (_distanceTraveled >= Distance)
                {
                    return;
                }

                var distance = PullSpeed * Time.deltaTime;
                _distanceTraveled += distance;

                transform.Translate(0f, 0f, -distance);
                return;
            }

            if (_distanceTraveled <= 0)
            {
                return;
            }

            var fireSpeed = PushSpeed * Time.deltaTime;
            _distanceTraveled -= fireSpeed;
            transform.Translate(0, 0, fireSpeed);

            if (_ball == null)
            {
                return;
            }
            _ball.GetComponent<Rigidbody>().AddForce(0, 0, _distanceTraveled / Distance * Force);
            _ball = null;
        }
    }
}
