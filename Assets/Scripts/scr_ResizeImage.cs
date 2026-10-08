using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.RuleTile.TilingRuleOutput;

public enum ResizeImageType
{
    None,
    Classic,
    AnchorScale,
}
public class ResizeImage : MonoBehaviour
{
    public bool useWorldScaling = false;
    public ResizeImageType scalingType = ResizeImageType.Classic;
    public Image image; // Can be assigned in inspector, or picked up automatically
    public Draggable draggableParent; // Must be assigned in inspector
    
    [Header("Classic Scaling")]
    [Tooltip("Modify the scale of the image by this amount.")]
    public Vector3 scale = new Vector3(1f, 1f, 1f);
    [Tooltip("Whether to also copy the z scale from the original image.")]
    public bool inheritZScale = false;

    [SerializeField] private Vector3 currentClassicScale = new Vector3(1f, 1f, 1f);
    public Vector3 minClassicScale = new Vector3(1f, 1f, 1f);
    public Vector3 maxClassicScale = new Vector3(1f, 1f, 1f);
    [Header("Anchor Scaling")]
    public List<GameObject> imageAnchorPoints = new List<GameObject>(4);
    [Header("Debug")]
    [DisplayWithoutEdit, SerializeField] private Vector3 baseScale = new Vector3(1f,1f,1f);
    [DisplayWithoutEdit, SerializeField] private Vector3[] v_current = new Vector3[4];
    [DisplayWithoutEdit, SerializeField] private Vector3[] v_new = new Vector3[4];

    private void OnValidate()
    {
        if (Application.isPlaying) return;

        Image _image = UpdateImageSize();
        AnchorScalingPoints(_image);

        if (draggableParent) draggableParent.gameObject.name = _image.sprite.name;
    }

    private void Start()
    {
        UpdateImageSize();
    }

    #region Setup
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

    public Image GetImage()
    {
        if (image) return image;
        else return GetComponent<Image>();
    }

    public Image UpdateImageSize(Image _image = null)
    {
        // Get image
        if (!_image)
        {
            _image = GetImage();
        }

        //
        var _size = _image.sprite.bounds.size;

        var _scale = new Vector3(scale.x * _size.x, scale.y * _size.y, scale.z * (inheritZScale ? _size.z : 1));

        transform.localScale = new Vector3(scale.x * _size.x, scale.y * _size.y, scale.z * (inheritZScale ? _size.z : 1));
        baseScale = transform.localScale;
        // Return image
        return _image;
    }
    #endregion

    private void Update()
    {
        if (scalingType == ResizeImageType.AnchorScale) UpdateAnchorScale();
        else if (scalingType == ResizeImageType.Classic) UpdateClassicScale();
    }

    #region In-game Scaling
    public void UpdateClassicScale()
    {
        Debug.Log(draggableParent.transform.parent.name + "/" + draggableParent.resetToParent.name + "\n" +
            draggableParent.transform.parent.childCount + "/" + (draggableParent.transform.GetSiblingIndex()+1));
        if (draggableParent.transform.parent == draggableParent.resetToParent
            && draggableParent.transform.parent.childCount == draggableParent.transform.GetSiblingIndex()+1)
        {
            var _scroll = Input.GetAxis("Mouse ScrollWheel");

            if (_scroll != 0)
            {
                Debug.Log("Scroll = " + _scroll);
                currentClassicScale = new Vector3(
                    Mathf.Clamp(currentClassicScale.x + _scroll, minClassicScale.x, maxClassicScale.x),
                    Mathf.Clamp(currentClassicScale.y + _scroll, minClassicScale.y, maxClassicScale.y),
                    Mathf.Clamp(currentClassicScale.z + _scroll, minClassicScale.z, maxClassicScale.z)
                    );
            }

            Image _image = GetImage();
            if (_image)
            {
                _image.rectTransform.localScale = new Vector3(
                    (currentClassicScale.x * baseScale.x),
                    (currentClassicScale.y * baseScale.y),
                    (currentClassicScale.z * baseScale.z)
                    );
            } else
            {
                Debug.LogError("No image found");
            }
        }
    }
    private void UpdateAnchorScale()
    {
        Image _image = GetImage();
        v_current = new Vector3[4];
        v_new = new Vector3[4];
        _image.rectTransform.GetWorldCorners(v_current);

        if (imageAnchorPoints.Count == 4)
        {
            for (int i = 0; i < 4; i++)
            {
                if (imageAnchorPoints[i]) v_new[i] = imageAnchorPoints[i].transform.localToWorldMatrix.GetPosition();
                else v_new[i] = v_current[i];
            }

            var _canvas = GetComponentInParent<Canvas>();

            // Check for changes in anchor position
            for (int i = 0; i < 4; i++)
            {
                int j = (i + 2) % 4;
                if (v_new[i] != v_current[i])
                {
                    Debug.Log("poop1 | " + i + " " + v_current[i] + "->" + v_new[i] + ": " + (v_current[i] - v_new[i]));
                    //Vector3 _newSize = new Vector3(v_new[i].x - v_current[i].x)
                    Debug.Log("poop2 | " + i + "/" + j + " " + v_new[i] + "/" + v_new[j]);
                    _image.rectTransform.localScale = new Vector3(
                        baseScale.x * Mathf.Abs(v_new[j].x - v_new[i].x),
                        baseScale.y * Mathf.Abs(v_new[j].y - v_new[i].y),
                        1
                        );
                }
            }
        }
    }

    #endregion
}
