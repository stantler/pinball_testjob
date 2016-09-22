using UnityEngine;
using UnityEngine.UI;

namespace Helpers
{
    [RequireComponent(typeof(Image), typeof(Button))]
    public class ClickHelper : MonoBehaviour, ICanvasRaycastFilter
    {
        private Image.Type _imageType;
        private Sprite _sprite;

        private void Awake()
        {
            var image = GetComponent<Image>();
            if (image == null)
            {
                Debug.LogError("There is no Image at thios gameObject", gameObject);
                Destroy(this);
                return;
            }

            _imageType = image.type;
            _sprite = image.sprite;
        }

        public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
        {
            var rectTransform = (RectTransform)transform;
            Vector2 localPositionPivotRelative;
            RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)transform, sp, eventCamera, out localPositionPivotRelative);

            // convert to bottom-left origin coordinates
            var localPosition = new Vector2(localPositionPivotRelative.x + rectTransform.pivot.x * rectTransform.rect.width,
                localPositionPivotRelative.y + rectTransform.pivot.y * rectTransform.rect.height);

            var spriteRect = _sprite.textureRect;
            var maskRect = rectTransform.rect;

            int x;
            int y;
            // convert to texture space
            switch (_imageType)
            {
                case Image.Type.Sliced:
                    {
                        var border = _sprite.border;
                        // x slicing
                        if (localPosition.x < border.x)
                        {
                            x = Mathf.FloorToInt(spriteRect.x + localPosition.x);
                        }
                        else if (localPosition.x > maskRect.width - border.z)
                        {
                            x = Mathf.FloorToInt(spriteRect.x + spriteRect.width - (maskRect.width - localPosition.x));
                        }
                        else
                        {
                            x = Mathf.FloorToInt(spriteRect.x + border.x +
                                                 ((localPosition.x - border.x) /
                                                 (maskRect.width - border.x - border.z)) *
                                                 (spriteRect.width - border.x - border.z));
                        }
                        // y slicing
                        if (localPosition.y < border.y)
                        {
                            y = Mathf.FloorToInt(spriteRect.y + localPosition.y);
                        }
                        else if (localPosition.y > maskRect.height - border.w)
                        {
                            y = Mathf.FloorToInt(spriteRect.y + spriteRect.height - (maskRect.height - localPosition.y));
                        }
                        else
                        {
                            y = Mathf.FloorToInt(spriteRect.y + border.y +
                                                 ((localPosition.y - border.y) /
                                                 (maskRect.height - border.y - border.w)) *
                                                 (spriteRect.height - border.y - border.w));
                        }
                    }
                    break;
                default:
                    {
                        // conversion to uniform UV space
                        x = Mathf.FloorToInt(spriteRect.x + spriteRect.width * localPosition.x / maskRect.width);
                        y = Mathf.FloorToInt(spriteRect.y + spriteRect.height * localPosition.y / maskRect.height);
                    }
                    break;
            }

            // destroy component if texture import settings are wrong
            try
            {
                return _sprite.texture.GetPixel(x, y).a > 0;
            }
            catch (UnityException e)
            {
                Debug.LogError("Mask texture not readable, set your sprite to Texture Type 'Advanced' and check 'Read/Write Enabled'\nException: " + e.Message);
                Destroy(this);
                return false;
            }
        }
    }
}
