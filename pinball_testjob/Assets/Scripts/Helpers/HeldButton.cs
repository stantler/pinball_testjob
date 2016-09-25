using System;
using Helpers.Extension;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Helpers
{
    public class HeldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private bool _isPress;

        public event Action<bool> OnPointerUpdate;

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_isPress)
            {
                return;
            }

            _isPress = true;
            OnPointerUpdate.SafeInvoke(_isPress);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!_isPress)
            {
                return;
            }

            _isPress = false;
            OnPointerUpdate.SafeInvoke(_isPress);
        }
    }
}
