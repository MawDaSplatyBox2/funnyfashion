using UnityEngine;
using UnityEngine.UI;

public class Util : MonoBehaviour
{
    public static void SetBoxCollider(GameObject _gameObject)
    {
        RectTransform itemRectTransform = _gameObject.GetComponent<RectTransform>();
        // strech box collider
        BoxCollider2D itemBoxCollider2D = itemRectTransform.gameObject.GetComponent<BoxCollider2D>();
        if (itemBoxCollider2D != null)
        {
            Image image = itemRectTransform.gameObject.GetComponent<Image>();

            if (image.preserveAspect)
            {

                var originalW = (int)(image.sprite.rect.width);
                var originalH = (int)(image.sprite.rect.height);

                var currentW = image.rectTransform.rect.width;
                var currentH = image.rectTransform.rect.height;

                var ratio = Mathf.Min(currentW / originalW, currentH / originalH);

                var newW = image.sprite.rect.width * ratio;
                var newH = image.sprite.rect.height * ratio;

                itemBoxCollider2D.size = new Vector2(newW, newH);
            }
            else
            {

                itemBoxCollider2D.size = image.rectTransform.rect.size;

            }
        }
    }
}
