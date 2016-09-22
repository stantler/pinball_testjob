using UnityEngine;
using UnityEngine.UI;

namespace Helpers
{
    public class VersionView:MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<Text>().text = Application.version;
        }
    }
}
