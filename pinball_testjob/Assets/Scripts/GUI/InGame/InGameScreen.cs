using GUI.Base;
using UnityEngine;

namespace GUI.InGame
{
    [GUIComponent(0, "GUI/InGameScreen")]
    public class InGameScreen : GUIComponent<InGameScreenInformer>
    {
        public bool IsLeftPressed { get; private set; }
        public bool IsRightPressed { get; private set; }
        public bool IsPullPressed { get; private set; }

        private void OnLeftUpdate(bool isPressed)
        {
            IsLeftPressed = isPressed;
        }

        private void OnRightUpdate(bool isPressed)
        {
            IsRightPressed = isPressed;
        }

        private void OnSpringUpdate(bool isPressed)
        {
            IsPullPressed = isPressed;
        }

        public InGameScreen(GUIManager guiManager, GameObject view) : base(guiManager, view)
        {
            Informer.ButtonLeft.OnPointerUpdate += OnLeftUpdate;
            Informer.ButtonRight.OnPointerUpdate += OnRightUpdate;
            Informer.ButtonSprting.OnPointerUpdate += OnSpringUpdate;
        }

        public override void Dispose()
        {
            Informer.ButtonLeft.OnPointerUpdate -= OnLeftUpdate;
            Informer.ButtonRight.OnPointerUpdate -= OnRightUpdate;
            Informer.ButtonSprting.OnPointerUpdate -= OnSpringUpdate;
        }
    }
}
