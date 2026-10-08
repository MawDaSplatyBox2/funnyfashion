using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class ResizeImage : MonoBehaviour
{
    public bool useWorldScaling = false;
    public bool useAnchorScaling = false;
    public Image image;
    
    [Header("Classic Scaling")]
    [Tooltip("Modify the scale of the image by this amount.")]
    public Vector3 scale = new Vector3(1f, 1f, 1f);
    [Tooltip("Whether to also copy the z scale from the original image.")]
    public bool inheritZScale = false;
    [Header("Anchor Scaling")]
    public List<GameObject> imageAnchorPoints = new List<GameObject>(4);

    private void OnValidate()
    {
        Image _image = UpdateImageSize();
        AnchorScalingPoints(_image);
    }

    private void Start()
    {
        UpdateImageSize();
    }

    private void AnchorScalingPoints(Image _image)
    {
        Vector3[] v = new Vector3[4];
        _image.rectTransform.GetWorldCorners(v);

        if (imageAnchorPoints.Count == 4)
        {
            for(int i = 0; i < 4; i++)
            {
                if (imageAnchorPoints[i]) imageAnchorPoints[i].transform.position = v[i];
            }
            return;
        }
        
        var _newList = new List<GameObject>();
        for(int i = 0; i < 4; i++)
        {
            if (i < imageAnchorPoints.Count)
            {
                _newList.Add(imageAnchorPoints[i]);
            } else
            {
                _newList.Add(null);
            }
        }

        imageAnchorPoints = _newList;
    }

    public Image UpdateImageSize(Image _image = null)
    {
        // Get image
        if (!_image)
        {
            if (image) _image = image;
            else _image = GetComponent<Image>();
        }

        //
        var _size = _image.sprite.bounds.size;

        var _scale = new Vector3(scale.x * _size.x, scale.y * _size.y, scale.z * (inheritZScale ? _size.z : 1));

        transform.localScale = new Vector3(scale.x * _size.x, scale.y * _size.y, scale.z * (inheritZScale ? _size.z : 1));
        
        // Return image
        return _image;
    }
}
