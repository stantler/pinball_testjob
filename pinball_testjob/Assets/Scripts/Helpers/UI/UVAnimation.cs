using UnityEngine;
using UnityEngine.UI;

namespace Helpers
{
    public class UVAnimation : MonoBehaviour
    {
        public Vector2 Speed = new Vector2(0.1f, 0f);
        public RawImage Texture;

        public void Update()
        {
            if (Texture == null)
            {
                Destroy(this);
                return;
            }

            var r = Texture.uvRect;
            Texture.uvRect = new Rect(r.x + Time.deltaTime * Speed.x, r.y + Time.deltaTime * Speed.y, r.width, r.height);
        }
    }
}
