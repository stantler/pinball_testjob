using UnityEngine;

namespace Board.Behaviours
{
    public class PeddleBehaviour : MonoBehaviour
    {
        private HingeJoint _hingeJoint;
        private JointSpring _spring;

        public float RestPosition = 0F;
        public float PressedPosition = 60f;
        public float FlipperStrength = 10000f;
        public float FlipperDamper = 25f;
        public KeyCode Key = KeyCode.A;

        private void Awake()
        {
            _hingeJoint = GetComponent<HingeJoint>();

            _spring = new JointSpring
            {
                spring = FlipperStrength,
                damper = FlipperDamper
            };

            _hingeJoint.spring = _spring;
            _hingeJoint.useLimits = true;

            var jointLimits = _hingeJoint.limits;
            jointLimits.min = RestPosition;
            jointLimits.max = PressedPosition;
            _hingeJoint.limits = jointLimits;
        }

        public void UpdateInput(bool isPressed)
        {
            if (isPressed)
            {
                _spring.targetPosition = PressedPosition;
            }
            else
            {
                _spring.targetPosition = RestPosition;
            }
            _hingeJoint.spring = _spring;
        }
    }
}
