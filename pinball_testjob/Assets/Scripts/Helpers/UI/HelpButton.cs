using System;
using Helpers.Extension;
using UnityEngine;
using UnityEngine.UI;

namespace Helpers.UI
{
    public class HelpButton : MonoBehaviour
    {
        private GameObject _countContainer;
        private Button _button;

        private int _currentCount;

        public Text Count;

        public Action OnClick;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _countContainer = Count.transform.parent.gameObject;
            _button.onClick.AddListener(() => OnClick.SafeInvoke());
            UpdateCount(0);
        }

        private void OnEnable()
        {
            UpdateCount(_.GameController.CurrentSession.HelpAdvice);
            _.GameController.CurrentSession.OnHelpAdviceCountChanged += UpdateCount;
        }

        private void OnDisable()
        {
            _.GameController.CurrentSession.OnHelpAdviceCountChanged -= UpdateCount;
        }

        private void UpdateCount(int count)
        {
            Count.AnimateCount(_currentCount, count);
            _currentCount = count;
            _countContainer.SetActive(_currentCount > 0);
        }
    }
}
