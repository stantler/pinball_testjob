using UnityEngine;

namespace Board.Behaviours
{
    public class PeddleBehaviour : MonoBehaviour
    {
        private HingeJoint _hingeJoint;
        private JointSpring _spring;

        public float restPosition = 0F;
        public float pressedPosition = 45F;
        public float flipperStrength = 10000F;
        public float flipperDamper = 1F;
        public KeyCode Key = KeyCode.A;

        private void Awaky()
        {
            _hingeJoint = GetComponent<HingeJoint>();

            _spring = new JointSpring
            {
                spring = flipperStrength,
                damper = flipperDamper
            };

            _hingeJoint.spring = _spring;
            _hingeJoint.useLimits = true;
            _hingeJoint.limits = new JointLimits()
            {
                min = restPosition,
                max = pressedPosition,
            };
        }

        private void Update()
        {
            if (Input.GetKey(Key))
            {
                _spring.targetPosition = pressedPosition;
            }
            else
            {
                _spring.targetPosition = restPosition;
            }
        }
    }
}
