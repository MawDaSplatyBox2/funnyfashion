using UnityEngine;
using UnityEngine.UI;

public class ResizeImage : MonoBehaviour
{
    public float scale = 1f;
    public bool thisDoesNothing = false;

    private void OnValidate()
    {
        UpdateImageSize();
    }

    private void Start()
    {
        UpdateImageSize();
    }

    public void UpdateImageSize(Image _image = null)
    {
        if (!_image) _image = GetComponent<Image>();

        Debug.Log(_image.sprite.pixelsPerUnit);
        Debug.Log(_image.sprite.bounds.size);
        var _size = _image.sprite.bounds.size;
        transform.localScale = new Vector3(scale*_size.x, scale*_size.y, 1);
    }
}
